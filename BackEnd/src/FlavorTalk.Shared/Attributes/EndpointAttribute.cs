using FlavorTalk.Shared.Models;

namespace FlavorTalk.Shared.Attributes;
[AttributeUsage(AttributeTargets.Class)]
public class EndpointAttribute : Attribute
{
    public EndpointMethod Method { get; }
    public string? Route { get; }
    public bool RequiresAuth { get; }
    public bool UseAutoPath { get; }

    public EndpointAttribute(EndpointMethod method, string? route = null, bool requiresAuth = true, bool useAutoPath = true)
    {
        Method = method;
        Route = route;
        RequiresAuth = requiresAuth;
        UseAutoPath = useAutoPath && string.IsNullOrEmpty(route);
    }
}
