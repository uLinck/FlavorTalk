﻿using FlavorTalk.Domain.Resources;
using FlavorTalk.Infrastructure.Data;
using FlavorTalk.Shared.Attributes;
using FlavorTalk.Shared.Models;
using FluentResults;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorTalk.Core.Features.Plates.Commands;

[Endpoint(EndpointMethod.PUT, "plates/{plateId}")]
public static class UpdatePlate
{
    public record Command(Guid PlateId, string Name, string? Description)
    {
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.PlateId)
                    .NotEmpty();

                RuleFor(x => x.Name)
                    .NotEmpty();
            }
        }
    }

    public record Response(Guid PlateId);

    public class Handler
    {
        public static async Task<Result<Response>> Handle(Command command, FlavorTalkContext context)
        {
            var plate = await context.Plates.FindAsync(command.PlateId);

            if (plate is null) return Result.Fail(Errors.PlateNotFound);

            plate.Update(command.Name, command.Description);

            await context.SaveChangesAsync();

            return Result.Ok(new Response(plate.Id));
        }
    }
}
