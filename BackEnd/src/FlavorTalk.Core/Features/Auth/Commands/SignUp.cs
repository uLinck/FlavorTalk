using FlavorTalk.Domain.Entities;
using FlavorTalk.Shared.Attributes;
using FlavorTalk.Shared.Models;
using FluentResults;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace FlavorTalk.Core.Features.Auth.Commands;

[Endpoint(EndpointMethod.POST, requiresAuth: false)]
public static class SignUp
{
    public record Command(string Name, string Email, string Password)
    {
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
            }
        }
    }

    public record Response(Guid UserId, string Name, string Email);

    public class Handler
    {
        public static async Task<Result<Response>> Handle(Command command, UserManager<User> userManager)
        {
            var user = new User(command.Name, command.Email);
            var result = await userManager.CreateAsync(user);

            if (!result.Succeeded)
                return Fail(result);

            result = await userManager.AddPasswordAsync(user, command.Password);

            if (!result.Succeeded)
                return Fail(result);

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmResult = await userManager.ConfirmEmailAsync(user, token);

            if (!confirmResult.Succeeded)
                return Fail(confirmResult);

            return Result.Ok(new Response(user.Id, command.Name, command.Email));
        }

        private static Result<Response> Fail(IdentityResult result) =>
            Result.Fail(result.Errors.Select(x => x.Description));
    }
}
