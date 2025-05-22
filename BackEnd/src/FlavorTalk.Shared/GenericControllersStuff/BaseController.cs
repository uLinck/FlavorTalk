using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace FlavorTalk.Shared.GenericControllersStuff;

[ApiController]
[Route("api/v1/[controller]")]
public class BaseController(IMessageBus bus) : ControllerBase
{
    protected IMessageBus Bus => bus;
}
