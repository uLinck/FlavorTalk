using FlavorTalk.Shared.Attributes;
using FlavorTalk.Shared.Models;
using FluentResults;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.Text.Json;
using Wolverine;

namespace FlavorTalk.Shared.Extensions;

public static class MinimalApiExtensions
{
    private const string _baseApiV1Url = "/api/v1/";

    public static WebApplication MapCommandEndpoints(this WebApplication app, Assembly assembly)
    {
        var requestTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsNested)
            .Where(t => (t.Name == "Command" || t.Name == "Query") && t.DeclaringType != null)
            .ToList();

        foreach (var requestType in requestTypes)
        {
            var parentType = requestType.DeclaringType!;
            var endpointAttr = parentType.GetCustomAttribute<EndpointAttribute>();

            if (endpointAttr == null) continue;

            var responseType = parentType.GetNestedType("Response");
            if (responseType == null) continue;

            // Generate route based on attribute configuration
            var route = GenerateRoute(parentType, endpointAttr);

            RegisterEndpoint(app, parentType, requestType, responseType, endpointAttr, route);
        }

        return app;
    }

    private static string GenerateRoute(Type parentType, EndpointAttribute endpointAttr)
    {
        // If a specific route is provided in the attribute, use it
        if (!string.IsNullOrEmpty(endpointAttr.Route))
        {
            // Ensure the route starts with the base API path
            var route = endpointAttr.Route.StartsWith('/') ? endpointAttr.Route : $"/{endpointAttr.Route}";
            return route.StartsWith(_baseApiV1Url) ? route : $"{_baseApiV1Url.TrimEnd('/')}{route}";
        }

        // If UseAutoPath is true, generate route automatically
        if (endpointAttr.UseAutoPath)
        {
            return GenerateAutoRoute(parentType);
        }

        // Fallback to class name
        return $"{_baseApiV1Url}{parentType.Name.ToLowerInvariant()}";
    }

    private static string GenerateAutoRoute(Type parentType)
    {
        // Extract controller name from namespace
        // Example: FlavorTalk.Core.Features.Plates.Commands.CreatePlate -> plates/CreatePlate
        var namespaceParts = parentType.Namespace?.Split('.') ?? [];

        string controllerName = ExtractControllerName(namespaceParts, parentType);
        string actionName = ExtractActionName(parentType.Name);

        return $"{_baseApiV1Url}{controllerName.ToLowerInvariant()}/{actionName}";
    }

    private static string ExtractControllerName(string[] namespaceParts, Type parentType)
    {
        // Look for Features pattern first
        var featureIndex = Array.FindIndex(namespaceParts, part =>
            part.Equals("Features", StringComparison.OrdinalIgnoreCase));

        if (featureIndex >= 0 && featureIndex + 1 < namespaceParts.Length)
        {
            // Pattern: ...Features.{ControllerName}.Commands/Queries
            return namespaceParts[featureIndex + 1];
        }

        // Look for Controllers, Commands, Queries folders
        var controllerIndex = Array.FindLastIndex(namespaceParts, part =>
            part.Equals("Controllers", StringComparison.OrdinalIgnoreCase) ||
            part.Equals("Commands", StringComparison.OrdinalIgnoreCase) ||
            part.Equals("Queries", StringComparison.OrdinalIgnoreCase));

        if (controllerIndex > 0)
        {
            // Use the namespace part before Controllers/Commands/Queries
            return namespaceParts[controllerIndex - 1];
        }

        // Fallback: use the second-to-last namespace part
        if (namespaceParts.Length >= 2)
        {
            return namespaceParts[namespaceParts.Length - 2];
        }

        // Final fallback: use class name without common suffixes
        return RemoveCommonSuffixes(parentType.Name);
    }

    private static string ExtractActionName(string className)
    {
        // Remove common suffixes and convert to appropriate casing
        var cleanName = RemoveCommonSuffixes(className);
        return cleanName;
    }

    private static string RemoveCommonSuffixes(string name)
    {
        string[] suffixesToRemove = { "Command", "Query", "Handler", "Endpoint", "Controller" };

        foreach (var suffix in suffixesToRemove)
        {
            if (name.EndsWith(suffix, StringComparison.OrdinalIgnoreCase) && name.Length > suffix.Length)
            {
                return name.Substring(0, name.Length - suffix.Length);
            }
        }

        return name;
    }

    private static void RegisterEndpoint(WebApplication app, Type parentType, Type requestType,
        Type responseType, EndpointAttribute attr, string route)
    {
        var method = attr.Method;

        var endpoint = method switch
        {
            EndpointMethod.POST => app.MapPost(route, CreateHandler(requestType, responseType)),
            EndpointMethod.PUT => app.MapPut(route, CreateHandler(requestType, responseType)),
            EndpointMethod.PATCH => app.MapPatch(route, CreateHandler(requestType, responseType)),
            EndpointMethod.DELETE => app.MapDelete(route, CreateHandler(requestType, responseType)),
            EndpointMethod.GET => app.MapGet(route, CreateHandler(requestType, responseType)),
            _ => null
        };

        if (endpoint != null)
        {
            // Create a unique endpoint name using controller + action + method
            var controllerName = ExtractControllerName(parentType.Namespace?.Split('.') ?? [], parentType);
            var actionName = ExtractActionName(parentType.Name);
            var endpointName = $"{controllerName}{actionName}{method}";

            endpoint.WithName(endpointName);

            if (attr.RequiresAuth)
            {
                endpoint.RequireAuthorization();
            }
        }
    }

    private static Delegate CreateHandler(Type requestType, Type responseType)
    {
        return async (HttpContext context, IMessageBus messageBus) =>
        {
            try
            {
                object? request = null;

                // Handle GET requests (typically queries) - get data from route and query string
                if (context.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
                {
                    request = CreateRequestFromRouteAndQuery(requestType, context);
                }
                else
                {
                    // Handle POST, PUT, PATCH, DELETE - combine JSON body with route parameters
                    request = await CreateRequestFromBodyAndRoute(requestType, context);
                }

                if (request == null)
                {
                    return CreateErrorResponse(typeof(object), ["Invalid request data"], [], 400);
                }

                var trySendMethod = typeof(WolverineExtensions)
                    .GetMethod("TrySendAsync", BindingFlags.Public | BindingFlags.Static)!
                    .MakeGenericMethod(responseType);

                var taskResult = trySendMethod.Invoke(null, new[] { messageBus, request })!;
                await (Task) taskResult;

                var resultProperty = taskResult.GetType().GetProperty("Result");
                var actualResult = resultProperty?.GetValue(taskResult);

                return HandleResultWithWrapper(actualResult, responseType);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse(typeof(object), ["An unexpected error occurred"], [ex.Message], 500);
            }
        };
    }

    private static object? CreateRequestFromRouteAndQuery(Type requestType, HttpContext context)
    {
        // Get constructor parameters for records
        var constructors = requestType.GetConstructors();
        var primaryConstructor = constructors.OrderByDescending(c => c.GetParameters().Length).First();
        var constructorParams = primaryConstructor.GetParameters();

        var parameterValues = new object?[constructorParams.Length];

        // Fill constructor parameters from route and query values
        for (int i = 0; i < constructorParams.Length; i++)
        {
            var param = constructorParams[i];
            var paramName = param.Name!;

            // Try route values first
            var routeValue = context.Request.RouteValues
                .FirstOrDefault(rv => string.Equals(rv.Key, paramName, StringComparison.OrdinalIgnoreCase));

            if (routeValue.Key != null && routeValue.Value != null)
            {
                parameterValues[i] = ConvertValue(routeValue.Value.ToString()!, param.ParameterType);
            }
            else
            {
                // Try query string values
                var queryValue = context.Request.Query
                    .FirstOrDefault(q => string.Equals(q.Key, paramName, StringComparison.OrdinalIgnoreCase));

                if (queryValue.Key != null && queryValue.Value.Count > 0)
                {
                    var value = queryValue.Value.First();
                    if (!string.IsNullOrEmpty(value))
                    {
                        parameterValues[i] = ConvertValue(value, param.ParameterType);
                    }
                }
                else
                {
                    // Use default value if parameter has one, otherwise null/default
                    if (param.HasDefaultValue)
                    {
                        parameterValues[i] = param.DefaultValue;
                    }
                    else if (param.ParameterType.IsValueType && Nullable.GetUnderlyingType(param.ParameterType) == null)
                    {
                        parameterValues[i] = Activator.CreateInstance(param.ParameterType);
                    }
                    else
                    {
                        parameterValues[i] = null;
                    }
                }
            }
        }

        return Activator.CreateInstance(requestType, parameterValues);
    }

    private static async Task<object?> CreateRequestFromBodyAndRoute(Type requestType, HttpContext context)
    {
        object? request = null;

        // Try to read from JSON body first
        if (context.Request.ContentLength > 0 &&
            context.Request.ContentType?.Contains("application/json") == true)
        {
            request = await context.Request.ReadFromJsonAsync(requestType);
        }

        // If we got a request from JSON and have route parameters, we need to create a new instance
        // with the route parameters overriding the JSON values
        if (context.Request.RouteValues.Any())
        {
            request = CreateRequestWithRouteOverrides(requestType, request, context);
        }
        // If no JSON body, create from route parameters only
        else if (request == null)
        {
            request = CreateRequestFromRouteParameters(requestType, context);
        }

        return request;
    }

    private static object? CreateRequestWithRouteOverrides(Type requestType, object? existingRequest, HttpContext context)
    {
        var constructors = requestType.GetConstructors();
        var primaryConstructor = constructors.OrderByDescending(c => c.GetParameters().Length).First();
        var constructorParams = primaryConstructor.GetParameters();

        var parameterValues = new object?[constructorParams.Length];

        // Fill constructor parameters
        for (int i = 0; i < constructorParams.Length; i++)
        {
            var param = constructorParams[i];
            var paramName = param.Name!;

            // Check if this parameter should be overridden by route value
            var routeValue = context.Request.RouteValues
                .FirstOrDefault(rv => string.Equals(rv.Key, paramName, StringComparison.OrdinalIgnoreCase));

            if (routeValue.Key != null && routeValue.Value != null)
            {
                // Override with route value
                parameterValues[i] = ConvertValue(routeValue.Value.ToString()!, param.ParameterType);
            }
            else if (existingRequest != null)
            {
                // Use value from existing request (JSON body)
                var property = requestType.GetProperty(param.Name!, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                parameterValues[i] = property?.GetValue(existingRequest);
            }
            else
            {
                // Use default value
                if (param.HasDefaultValue)
                {
                    parameterValues[i] = param.DefaultValue;
                }
                else if (param.ParameterType.IsValueType && Nullable.GetUnderlyingType(param.ParameterType) == null)
                {
                    parameterValues[i] = Activator.CreateInstance(param.ParameterType);
                }
                else
                {
                    parameterValues[i] = null;
                }
            }
        }

        return Activator.CreateInstance(requestType, parameterValues);
    }

    private static object? CreateRequestFromRouteParameters(Type requestType, HttpContext context)
    {
        var constructors = requestType.GetConstructors();
        var primaryConstructor = constructors.OrderByDescending(c => c.GetParameters().Length).First();
        var constructorParams = primaryConstructor.GetParameters();

        var parameterValues = new object?[constructorParams.Length];

        for (int i = 0; i < constructorParams.Length; i++)
        {
            var param = constructorParams[i];
            var paramName = param.Name!;

            var routeValue = context.Request.RouteValues
                .FirstOrDefault(rv => string.Equals(rv.Key, paramName, StringComparison.OrdinalIgnoreCase));

            if (routeValue.Key != null && routeValue.Value != null)
            {
                parameterValues[i] = ConvertValue(routeValue.Value.ToString()!, param.ParameterType);
            }
            else
            {
                if (param.HasDefaultValue)
                {
                    parameterValues[i] = param.DefaultValue;
                }
                else if (param.ParameterType.IsValueType && Nullable.GetUnderlyingType(param.ParameterType) == null)
                {
                    parameterValues[i] = Activator.CreateInstance(param.ParameterType);
                }
                else
                {
                    parameterValues[i] = null;
                }
            }
        }

        return Activator.CreateInstance(requestType, parameterValues);
    }

    private static object? ConvertValue(string value, Type targetType)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        // Handle nullable types
        var actualType = Nullable.GetUnderlyingType(targetType) ?? targetType;

        try
        {
            if (actualType == typeof(Guid))
            {
                return Guid.Parse(value);
            }
            else if (actualType == typeof(int))
            {
                return int.Parse(value);
            }
            else if (actualType == typeof(long))
            {
                return long.Parse(value);
            }
            else if (actualType == typeof(decimal))
            {
                return decimal.Parse(value);
            }
            else if (actualType == typeof(double))
            {
                return double.Parse(value);
            }
            else if (actualType == typeof(float))
            {
                return float.Parse(value);
            }
            else if (actualType == typeof(bool))
            {
                return bool.Parse(value);
            }
            else if (actualType == typeof(DateTime))
            {
                return DateTime.Parse(value);
            }
            else if (actualType == typeof(DateTimeOffset))
            {
                return DateTimeOffset.Parse(value);
            }
            else if (actualType.IsEnum)
            {
                return Enum.Parse(actualType, value, true);
            }
            else
            {
                return Convert.ChangeType(value, actualType);
            }
        }
        catch
        {
            // If conversion fails, return null for nullable types or default for value types
            return targetType.IsValueType && Nullable.GetUnderlyingType(targetType) == null
                ? Activator.CreateInstance(targetType)
                : null;
        }
    }

    private static IResult HandleResultWithWrapper(object? result, Type responseType)
    {
        if (result == null)
            return CreateSuccessResponse(null, responseType, 200);

        var isFailedProperty = result.GetType().GetProperty("IsFailed");
        if (isFailedProperty != null && (bool) isFailedProperty.GetValue(result)!)
        {
            var errorsProperty = result.GetType().GetProperty("Errors");
            var errors = new List<string>();
            var reasons = new List<string>();

            if (errorsProperty?.GetValue(result) is IEnumerable<IError> errorList)
            {
                errors = errorList.Select(e => e.Message).ToList();
                reasons = errorList.SelectMany(e => e.Reasons?.Select(r => r.Message) ?? []).ToList();
            }

            return CreateErrorResponse(responseType, errors, reasons, 400);
        }

        var valueProperty = result.GetType().GetProperty("Value");
        var actualValue = valueProperty?.GetValue(result) ?? result;

        return CreateSuccessResponse(actualValue, responseType, 200);
    }

    private static IResult CreateSuccessResponse(object? data, Type responseType, int statusCode)
    {
        var httpResponseType = typeof(HttpResponse<>).MakeGenericType(responseType);
        var response = Activator.CreateInstance(httpResponseType)!;

        httpResponseType.GetProperty("Data")?.SetValue(response, data);
        httpResponseType.GetProperty("Errors")?.SetValue(response, new List<string>());
        httpResponseType.GetProperty("Reasons")?.SetValue(response, new List<string>());

        return Results.Json(response, statusCode: statusCode);
    }

    private static IResult CreateErrorResponse(Type responseType, List<string> errors, List<string> reasons, int statusCode)
    {
        var httpResponseType = typeof(HttpResponse<>).MakeGenericType(responseType);
        var response = Activator.CreateInstance(httpResponseType)!;

        httpResponseType.GetProperty("Data")?.SetValue(response, null);
        httpResponseType.GetProperty("Errors")?.SetValue(response, errors);
        httpResponseType.GetProperty("Reasons")?.SetValue(response, reasons);

        return Results.Json(response, statusCode: statusCode);
    }
}