using FlavorTalk.Domain.Entities;
using FlavorTalk.Domain.Resources;
using FlavorTalk.Infrastructure.Data;
using FlavorTalk.Shared.Attributes;
using FlavorTalk.Shared.Extensions;
using FlavorTalk.Shared.Models;
using FluentResults;
using FluentValidation;

namespace FlavorTalk.Core.Features.Plates.Commands;

[Endpoint(EndpointMethod.POST, "plates")]
public static class CreatePlate
{
    public record Command(string Name, string Description, Guid? CategoryId, Guid MerchantId)
    {
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Name)
                    .NotEmpty();

                RuleFor(x => x.Description)
                    .NotEmpty();

                RuleFor(x => x.MerchantId)
                    .NotEmpty();
            }
        }
    }

    public record Response(Guid PlateId);

    public class Handler
    {
        public static async Task<Result<Response>> Handle(Command command, FlavorTalkContext context)
        {
            var merchant = await context.Merchants.FindAsync(command.MerchantId);

            if (merchant is null)
                return Result.Fail(Errors.MerchantNotFound);

            Category? category = null;

            if (command.CategoryId is not null)
                category = await context.Categories.FindAsync(command.CategoryId);

            var plate = new Plate(command.Name, command.Description);

            if (category is null)
                merchant.Catalog.AddCategory(new(plate.Name, [plate]));
            else
                context.Categories.Update(category.With(c => c.AddPlate(plate)));

            await context.SaveChangesAsync();

            return Result.Ok(new Response(plate.Id));
        }
    } 
}
