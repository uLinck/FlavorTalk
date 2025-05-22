using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlavorTalk.Shared.GenericControllersStuff;
public class GenericControllerOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var descriptor = context.ApiDescription.ActionDescriptor;

        if (descriptor is not ControllerActionDescriptor cad)
            return;

        if (!cad.ControllerTypeInfo.IsGenericType)
            return;

        var declaringType = cad.ControllerTypeInfo.GenericTypeArguments.FirstOrDefault()?.DeclaringType;
        var endpointAttr = declaringType?.GetCustomAttribute<EndpointAttribute>();

        if (endpointAttr is null)
            return;

        operation.Summary = $"Endpoint: {endpointAttr.Route}";
    }
}
