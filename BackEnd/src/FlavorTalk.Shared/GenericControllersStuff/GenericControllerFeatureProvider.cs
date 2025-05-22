using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;
using System.Runtime.CompilerServices;
using Wolverine;

namespace FlavorTalk.Shared.GenericControllersStuff;

public abstract class GenericController(IMessageBus bus) : BaseController(bus) { }

public class GenericControllerFeatureProvider(Assembly assembly) : IApplicationFeatureProvider<ControllerFeature>
{
    public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
    {
        var candidates = assembly.GetTypes()
            .Where(t => t.GetCustomAttributes<EndpointAttribute>(false).Any())
            .ToList();

        foreach (var c in candidates)
        {
            Console.WriteLine($"[GENERIC-CONTROLLER] Encontrado: {c.FullName}");
        }

        foreach (var candidate in candidates)
        {
            var attribute = candidate.GetCustomAttribute<EndpointAttribute>();
            var controllerType = typeof(EndpointController<,>).MakeGenericType(
                GetCommandType(candidate),
                GetResponseType(candidate));

            var typeInfo = controllerType.GetTypeInfo();

            Console.WriteLine($"[GENERIC-CONTROLLER] Registrando controller: {controllerType.FullName}");

            if (!feature.Controllers.Contains(typeInfo))
                feature.Controllers.Add(typeInfo);
        }
    }

    private static Type GetCommandType(Type type) => 
        type.GetNestedType("Command") ?? throw new InvalidOperationException($"Command type not found in {type.Name}");
    private static Type GetResponseType(Type type) => 
        type.GetNestedType("Response") ?? throw new InvalidOperationException($"Response type not found in {type.Name}");
}
