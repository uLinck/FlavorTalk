using FlavorTalk.Domain.Entities;
using FlavorTalk.Infrastructure.Data;
using FlavorTalk.Shared.Extensions;
using FluentResults;
using FluentValidation;

namespace FlavorTalk.Core.Features.Catalogs.Commands;
public static class CreatePlate
{
    public record Command(string Name, string Description, Guid? CategoryId)
    {
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Name)
                    .NotNull()
                    .NotEmpty();

                RuleFor(x => x.Description)
                    .NotNull()
                    .NotEmpty();
            }
        }
    }


    public record Response(Guid PlateId);

    public class Handler
    {
        public static async Task<Result<Response>> Handle(Command command, FlavorTalkContext context)
        {
            Category? category = null;

            if (command.CategoryId is not null)
                category = await context.Categories.FindAsync(command.CategoryId);

            var plate = new Plate(command.Name, command.Description);

            if (category is null)
                context.Categories.Add(new Category(plate.Name, [plate]));
            else
                context.Categories.Update(category.With(c => c.AddPlate(plate)));

            await context.SaveChangesAsync();

            return Result.Ok(new Response(plate.Id));
        }
    } 
}
