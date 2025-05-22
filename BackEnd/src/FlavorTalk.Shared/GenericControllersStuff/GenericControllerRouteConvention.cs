using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Reflection;

namespace FlavorTalk.Shared.GenericControllersStuff;

public class GenericControllerRouteConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        if (!controller.ControllerType.IsGenericType)
            return;

        var genericType = controller.ControllerType.GetGenericTypeDefinition();
        if (genericType != typeof(EndpointController<,>))
            return;

        var commandType = controller.ControllerType.GenericTypeArguments[0];
        var parentType = commandType.DeclaringType;

        if (parentType == null)
            return;

        var attribute = parentType.GetCustomAttribute<EndpointAttribute>();
        if (attribute == null)
            return;

        controller.ControllerName = parentType.Name;

        if (attribute.RequiresAuthorization)
        {
            var authFilter = new AuthorizeFilter();
            controller.Filters.Add(authFilter);
        }

        // Configure action and route
        var action = controller.Actions.FirstOrDefault();
        if (action == null)
            return;

        action.ActionName = "Execute";

        var selector = action.Selectors.FirstOrDefault();
        if (selector == null)
        {
            selector = new SelectorModel();
            action.Selectors.Add(selector);
        }

        selector.AttributeRouteModel = new AttributeRouteModel
        {
            Template = attribute.Route
        };

        // Set HTTP method using the enum
        var methodConstraint = new HttpMethodActionConstraint(
            [attribute.HttpMethod.ToString()]
        );

        selector.ActionConstraints.Add(methodConstraint);
    }
}
