using FlavorTalk.Shared;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace FlavorTalk.Api.Configs.Filters;

public class HttpResponseFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.Result is ObjectResult objectResult)
        {
            object? data = null;
            List<string> errors = [];
            List<string> reasons = [];

            if (objectResult.StatusCode == 400)
            {
                errors.Add("One or more validation errors occurred.");

                try
                {
                    var json = JsonSerializer.Serialize(objectResult.Value);
                    var errorDoc = JsonDocument.Parse(json);
                    var root = errorDoc.RootElement;

                    if (root.TryGetProperty("errors", out var errorsElement))
                    {
                        foreach (var errorProperty in errorsElement.EnumerateObject())
                        {
                            if (errorProperty.Value.ValueKind == JsonValueKind.Array)
                            {
                                foreach (var message in errorProperty.Value.EnumerateArray())
                                {
                                    if (message.ValueKind == JsonValueKind.String)
                                    {
                                        reasons.Add(message.GetString() ?? string.Empty);
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                    if (objectResult.Value is ValidationProblemDetails validationProblem)
                    {
                        foreach (var error in validationProblem.Errors)
                        {
                            foreach (var message in error.Value)
                            {
                                reasons.Add(message);
                            }
                        }
                    }
                }
            }
            else if (objectResult.Value is IResultBase resultBase)
            {
                if (resultBase.IsSuccess)
                {
                    var valueProp = objectResult.Value.GetType().GetProperty("Value");
                    data = valueProp?.GetValue(objectResult.Value);
                }
                else
                {
                    errors = resultBase.Errors.Select(e => e.Message).ToList();
                    reasons = resultBase.Errors.SelectMany(e => e.Reasons.Select(r => r.Message)).ToList();
                }
            }
            else if (objectResult.StatusCode >= 400)
            {
                errors.Add("An error occurred");
            }
            else
            {
                data = objectResult.Value;
            }

            var responseType = typeof(HttpResponse<>).MakeGenericType(data?.GetType() ?? typeof(object));
            var response = Activator.CreateInstance(responseType)!;
            responseType.GetProperty(nameof(HttpResponse<object>.Data))?.SetValue(response, data);
            responseType.GetProperty(nameof(HttpResponse<object>.Errors))?.SetValue(response, errors);
            responseType.GetProperty(nameof(HttpResponse<object>.Reasons))?.SetValue(response, reasons);

            context.Result = new ObjectResult(response)
            {
                StatusCode = objectResult.StatusCode
            };
        }

        await next();
    }
}