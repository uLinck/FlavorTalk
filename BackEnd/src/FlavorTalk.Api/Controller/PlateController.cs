using FlavorTalk.Api.Extensions;
using FlavorTalk.Core.Features.Catalogs.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace FlavorTalk.Api.Controller;

[Authorize]
public class PlateController : BaseController
{
    public PlateController(IMessageBus bus) : base(bus) { }

    [HttpPost]
    public async Task<ActionResult<CreatePlate.Response>> CreatePlateAsync(CreatePlate.Command command)
    {
        var result = await Bus.TrySendAsync<CreatePlate.Response>(command);

        if (result.IsFailed)
            return BadRequest(result);

        return Ok(result);
    }
}
