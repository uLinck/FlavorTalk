using FlavorTalk.Domain.Entities;
using FlavorTalk.Domain.Resources;
using FlavorTalk.Infrastructure.Data;
using FlavorTalk.Shared.Attributes;
using FlavorTalk.Shared.Models;
using FluentResults;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FlavorTalk.Core.Features.CategoryService.Commands;
[Endpoint(EndpointMethod.PUT, "categories")]
public static class UpdateCategory
{
    public record Command(Guid CategoryId, string Name)
    {
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.CategoryId)
                    .NotEmpty();

                RuleFor(x => x.Name)
                    .NotEmpty();

            }
        }
    }

    public record Response(Guid CategoryId);

    public class Handler
    {
        public static async Task<Result<Response>> Handle(Command command, FlavorTalkContext context)
        {
            var category = await context.Categories.FindAsync(command.CategoryId);

            if (category is null)
                return Result.Fail(Errors.CategoryNotFound);

            category.Update(command.Name);

            await context.SaveChangesAsync();

            return Result.Ok(new Response(category.Id));
        }
    }
}
