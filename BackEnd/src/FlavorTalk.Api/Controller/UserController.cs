using FlavorTalk.Api.Extensions;
using FlavorTalk.Core.Features.Users.Commands;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace FlavorTalk.Api.Controller;

public class UserController(IMessageBus bus) : BaseController(bus)
{
    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] CreateUser.Command command)
    {
        var result = await Bus.SendAsync<Result<CreateUser.Response>>(command);

        if (result.IsFailed)
            return BadRequest(result);

        return Ok(result.Value);
    }
}
