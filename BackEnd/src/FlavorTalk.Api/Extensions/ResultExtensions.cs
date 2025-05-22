using FlavorTalk.Shared.GenericControllersStuff;
using FluentResults;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace FlavorTalk.Api.Extensions;

public static class ResultExtensions
{
    public static Exception ToException(this ResultBase result) => new(string.Join(", ", result.Errors.Select(e => e.Message)));

    public static void ConfigureSwaggerForGenericControllers(this SwaggerGenOptions options)
    {
        options.CustomOperationIds(apiDesc =>
        {
            if (apiDesc.ActionDescriptor is Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor controllerActionDescriptor)
            {
                // For generic controllers, use a custom operation ID format
                if (controllerActionDescriptor.ControllerTypeInfo.IsGenericType &&
                    controllerActionDescriptor.ControllerTypeInfo.GetGenericTypeDefinition() == typeof(EndpointController<,>))
                {
                    var commandType = controllerActionDescriptor.ControllerTypeInfo.GenericTypeArguments[0];
                    var parentType = commandType.DeclaringType;
                    return parentType?.Name ?? controllerActionDescriptor.ActionName;
                }
            }
            return null; // Fall back to default behavior
        });

        // Add a document filter to add documentation for generic controllers
        options.DocumentFilter<GenericControllerDocumentFilter>();

        // Add an operation filter to customize the operation of generic controllers
        options.OperationFilter<GenericControllerOperationFilter>();

        // Include XML comments if available
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
        {
            options.IncludeXmlComments(xmlPath);
        }
    }
}

