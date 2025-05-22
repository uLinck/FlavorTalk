using FlavorTalk.Shared.Extensions;
using FluentResults;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using Wolverine;

namespace FlavorTalk.Shared;

// HTTP methods enum
public enum HttpMethod
{
    GET,
    POST,
    PUT,
    PATCH,
    DELETE
}

// Attributes to define HTTP behavior
[AttributeUsage(AttributeTargets.Class)]
public class EndpointAttribute : Attribute
{
    public HttpMethod Method { get; }
    public string? Route { get; }
    public bool RequiresAuth { get; }

    public EndpointAttribute(HttpMethod method, string? route = null, bool requiresAuth = true)
    {
        Method = method;
        Route = route;
        RequiresAuth = requiresAuth;
    }
}

// Extension method to auto-register endpoints
public static class MinimalApiExtensions
{
    public static WebApplication MapCommandEndpoints(this WebApplication app, Assembly assembly)
    {
        // Look for both Command and Query nested classes
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

            RegisterEndpoint(app, parentType, requestType, responseType, endpointAttr);
        }

        return app;
    }

    private static void RegisterEndpoint(WebApplication app, Type parentType, Type requestType,
        Type responseType, EndpointAttribute attr)
    {
        var route = attr.Route ?? $"/{parentType.Name}";
        var method = attr.Method;

        // We no longer need to get the handler method since we're using Wolverine
        var endpoint = method switch
        {
            HttpMethod.POST => app.MapPost(route, CreateHandler(requestType, responseType)),
            HttpMethod.PUT => app.MapPut(route, CreateHandler(requestType, responseType)),
            HttpMethod.PATCH => app.MapPatch(route, CreateHandler(requestType, responseType)),
            HttpMethod.DELETE => app.MapDelete(route, CreateHandler(requestType, responseType)),
            HttpMethod.GET => app.MapGet(route, CreateHandler(requestType, responseType)),
            _ => null
        };

        if (endpoint != null && attr.RequiresAuth)
        {
            endpoint.RequireAuthorization();
        }
    }

    private static Delegate CreateHandler(Type requestType, Type responseType)
    {
        // Create a handler that uses Wolverine's message bus
        return async (HttpContext context, IMessageBus messageBus) =>
        {
            try
            {
                // Deserialize the request body to the correct type
                var request = await context.Request.ReadFromJsonAsync(requestType);

                if (request == null)
                {
                    return CreateErrorResponse(typeof(object), ["Invalid request body"], [], 400);
                }

                var trySendMethod = typeof(WolverineExtensions)
                    .GetMethod("TrySendAsync", BindingFlags.Public | BindingFlags.Static)!
                    .MakeGenericMethod(responseType);

                var taskResult = trySendMethod.Invoke(null, new[] { messageBus, request })!;

                // Await the Task<Result<TResponse>>
                await (Task) taskResult;

                // Get the actual result from the completed task
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

    private static IResult HandleResultWithWrapper(object? result, Type responseType)
    {
        if (result == null)
            return CreateSuccessResponse(null, responseType, 200);

        // Check if it's a FluentResults Result<T>
        var isFailedProperty = result.GetType().GetProperty("IsFailed");
        if (isFailedProperty != null && (bool) isFailedProperty.GetValue(result)!)
        {
            // Extract errors from FluentResults
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

        // Extract the actual value if it's a successful Result<T>
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
