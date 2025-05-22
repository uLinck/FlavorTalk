namespace FlavorTalk.Shared.GenericControllersStuff;


/// <summary>
/// Represents HTTP methods according to RFC 7231 and common extensions.
/// </summary>
public enum HttpMethod
{
    /// <summary>
    /// Represents an HTTP GET method.
    /// The GET method requests a representation of the specified resource.
    /// Requests using GET should only retrieve data.
    /// </summary>
    GET,

    /// <summary>
    /// Represents an HTTP POST method.
    /// The POST method is used to submit an entity to the specified resource,
    /// often causing a change in state or side effects on the server.
    /// </summary>
    POST,

    /// <summary>
    /// Represents an HTTP PUT method.
    /// The PUT method replaces all current representations of the target resource with the request payload.
    /// </summary>
    PUT,

    /// <summary>
    /// Represents an HTTP DELETE method.
    /// The DELETE method deletes the specified resource.
    /// </summary>
    DELETE,

    /// <summary>
    /// Represents an HTTP PATCH method.
    /// The PATCH method is used to apply partial modifications to a resource.
    /// </summary>
    PATCH,

    /// <summary>
    /// Represents an HTTP HEAD method.
    /// The HEAD method asks for a response identical to that of a GET request,
    /// but without the response body.
    /// </summary>
    HEAD,

    /// <summary>
    /// Represents an HTTP OPTIONS method.
    /// The OPTIONS method is used to describe the communication options for the target resource.
    /// </summary>
    OPTIONS,

    /// <summary>
    /// Represents an HTTP TRACE method.
    /// The TRACE method performs a message loop-back test along the path to the target resource.
    /// </summary>
    TRACE,

    /// <summary>
    /// Represents an HTTP CONNECT method.
    /// The CONNECT method establishes a tunnel to the server identified by the target resource.
    /// </summary>
    CONNECT
}
