using Microsoft.AspNetCore.Mvc;
using Wolverine;
using FlavorTalk.Shared.Extensions;

namespace FlavorTalk.Shared.GenericControllersStuff;

[ApiController]
[Route("api/v1")]
public class EndpointController<TCommand, TResponse>(IMessageBus bus) : GenericController(bus)
    where TCommand : class
    where TResponse : class
{
    [HttpPost]
    public async Task<ActionResult<TResponse>> Execute([FromBody] TCommand command)
    {
        var result = await Bus.SendAsync<TResponse>(command);
        if (result.IsFailed)
            return BadRequest(result);
        return Ok(result);
    }
}
