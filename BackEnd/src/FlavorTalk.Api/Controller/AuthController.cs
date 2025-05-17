using FlavorTalk.Api.Extensions;
using FlavorTalk.Core.Features.Auth.Commands;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace FlavorTalk.Api.Controller;

public class AuthController : BaseController
{
    public AuthController(IMessageBus bus) : base(bus) { }

    [HttpPost("SignIn")]
    public async Task<ActionResult<SignIn.Response>> SignInAsync(SignIn.Command command)
    {
        var result = await Bus.SendAsync<SignIn.Response>(command);

        if (result.IsFailed)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("SignUp")]
    public async Task<ActionResult<SignUp.Response>> SignUpAsync(SignUp.Command command)
    {
        var result = await Bus.SendAsync<SignUp.Response>(command);

        if (result.IsFailed)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("GenerateToken")]
    public async Task<ActionResult<GenerateToken.Response>> GenerateTokenAsync(GenerateToken.Command command)
    {
        var result = await Bus.SendAsync<GenerateToken.Response>(command);

        if (result.IsFailed)
            return BadRequest(result);

        return Ok(result);
    }
}
