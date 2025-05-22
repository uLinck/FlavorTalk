using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace FlavorTalk.Api.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class BaseController(IMessageBus bus) : ControllerBase
{
    protected IMessageBus Bus => bus;
}
