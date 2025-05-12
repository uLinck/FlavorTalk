using FlavorTalk.Core.Features.Users.Commands;
using FlavorTalk.Core.Features.Users.Queries;
using FluentResults;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace FlavorTalk.Api.Controller;

public class UserController(IMessageBus bus) : BaseController(bus)
{
    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] CreateUser.Command command)
    {
        try
        {
            var result = await Bus.InvokeAsync<Result<CreateUser.Response>>(command);
            if (result.IsFailed)
                return BadRequest(result);
            return Ok(result.Value);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors.Select(e => e.ErrorMessage));
        }
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<GetAllUsers.Response>>> GetAllUsersAsync()
    {
        var result = await Bus.InvokeAsync<List<GetAllUsers.Response>>(new GetAllUsers.Query());
        return Ok(result);
    }
}
