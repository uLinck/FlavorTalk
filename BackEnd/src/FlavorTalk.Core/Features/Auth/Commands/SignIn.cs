using FlavorTalk.Domain.Entities;
using FlavorTalk.Domain.Resources;
using FlavorTalk.Shared.Attributes;
using FlavorTalk.Shared.Models;
using FluentResults;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace FlavorTalk.Core.Features.Auth.Commands;

[Endpoint(EndpointMethod.POST, requiresAuth: false)]
public static class SignIn
{
    public record Command(string Email, string Password, bool RememberMe) 
    {
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).NotEmpty();
            }
        }
    }

    public record Response(Guid Id, string Name, string? Email);

    public class Handler
    {
        public static async Task<Result<Response>> Handle(Command command,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            var user = await userManager.FindByEmailAsync(command.Email);
            if (user is null) return Result.Fail(Errors.UserNotFound);

            var result = await signInManager.PasswordSignInAsync(user, command.Password, command.RememberMe, false);
            if (!result.Succeeded) return Result.Fail(Errors.CouldNotSignIn);

            return Result.Ok(new Response(user.Id, user.Name, user.Email));
        }
    }
}
