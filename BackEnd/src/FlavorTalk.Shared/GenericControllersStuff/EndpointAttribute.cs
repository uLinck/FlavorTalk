namespace FlavorTalk.Shared.GenericControllersStuff;

/// <summary>
/// Initializes a new instance of the <see cref="EndpointAttribute"/> class.
/// </summary>
/// <param name="route">The route template for the endpoint.</param>
/// <param name="httpMethod">The HTTP method for the endpoint.</param>
/// <param name="requiresAuthorization">Whether the endpoint requires authorization.</param>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class EndpointAttribute(string route, HttpMethod httpMethod = HttpMethod.POST, bool requiresAuthorization = true) : Attribute
{
    /// <summary>
    /// Gets the route template for the endpoint.
    /// </summary>
    public string Route { get; } = route;

    /// <summary>
    /// Gets the HTTP method for the endpoint.
    /// </summary>
    public HttpMethod HttpMethod { get; } = httpMethod;

    /// <summary>
    /// Gets whether the endpoint requires authorization.
    /// </summary>
    public bool RequiresAuthorization { get; } = requiresAuthorization;
}

