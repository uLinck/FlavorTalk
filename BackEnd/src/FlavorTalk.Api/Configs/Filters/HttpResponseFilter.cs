using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FlavorTalk.Api.Configs.Filters;

public class HttpResponse<T> where T : class
{
    public T? Data { get; set; }
    public List<string> Errors { get; set; } = [];
    public List<string> Reasons { get; set; } = [];
}

public class HttpResponseFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.Result is not ObjectResult objectResult)
            await next();
        else
        {
            object? data = null;
            List<string> errors = [];
            List<string> reasons = [];

            if (objectResult.Value is IResultBase resultBase)
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