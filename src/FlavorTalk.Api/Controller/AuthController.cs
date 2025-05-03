using FlavorTalk.Core.Features.Auth.Commands;
using FluentResults;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace FlavorTalk.Api.Controller;

public class AuthController : BaseController
{
    public AuthController(IMessageBus bus) : base(bus) {}

    [HttpPost("SignIn")]
    public async Task<ActionResult<Result>> SignInAsync(SignIn.Command command)
    {
        try
        {
            var result = await Bus.InvokeAsync<Result>(command);
            if (result.IsFailed)
                return BadRequest(result);
            return Ok();
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors.Select(e => e.ErrorMessage));
        }
    }

    [HttpPost("GenerateToken")]
    public async Task<ActionResult<Result<GenerateToken.Response>>> GenerateTokenAsync(GenerateToken.Command command)
    {
        try
        {
            var result = await Bus.InvokeAsync<Result<GenerateToken.Response>>(command);
            if (result.IsFailed)
                return BadRequest(result);
            return Ok(result.Value);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors.Select(e => e.ErrorMessage));
        }
    }
}
