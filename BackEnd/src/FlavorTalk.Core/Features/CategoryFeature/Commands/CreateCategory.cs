using FlavorTalk.Domain.Entities;
using FlavorTalk.Domain.Resources;
using FlavorTalk.Infrastructure.Data;
using FlavorTalk.Shared.Attributes;
using FlavorTalk.Shared.Models;
using FluentResults;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FlavorTalk.Core.Features.CategoryService.Commands;
[Endpoint(EndpointMethod.POST, "categories")]
public static class CreateCategory
{
    public record Command(string Name, List<Guid>? PlateIds, Guid MerchantId)
    {
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Name)
                    .NotEmpty();

                RuleFor(x => x.MerchantId)
                    .NotEmpty();
            }
        }
    }

    public record Response(Guid CategoryId);

    public class Handler
    {
        public static async Task<Result<Response>> Handle(Command command, FlavorTalkContext context)
        {
            var merchant = await context.Merchants.FindAsync(command.MerchantId);

            if (merchant is null)
                return Result.Fail(Errors.MerchantNotFound);

            var plates = new List<Plate>();
            
            if (command.PlateIds?.Any() == true)
            {
                plates = await context.Plates
                    .Where(p => command.PlateIds.Contains(p.Id))
                    .ToListAsync();

                if (plates.Count != command.PlateIds.Count)
                    return Result.Fail(Errors.PlateNotFound);
            }

            var category = new Category(command.Name, plates);

            merchant.Catalog.AddCategory(category);
            await context.SaveChangesAsync();

            return Result.Ok(new Response(category.Id));
        }
    }
}
