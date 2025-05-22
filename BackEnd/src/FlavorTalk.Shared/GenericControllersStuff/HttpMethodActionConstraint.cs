using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace FlavorTalk.Shared.GenericControllersStuff;

public class HttpMethodActionConstraint : IActionConstraint
{
    private readonly string[] _httpMethods;

    public HttpMethodActionConstraint(IEnumerable<string> httpMethods)
    {
        _httpMethods = httpMethods?.ToArray() ?? Array.Empty<string>();
    }

    public int Order => 0;

    public bool Accept(ActionConstraintContext context)
    {
        if (context == null)
            throw new ArgumentNullException(nameof(context));

        var method = context.RouteContext.HttpContext.Request.Method;
        return _httpMethods.Contains(method, StringComparer.OrdinalIgnoreCase);
    }
}
