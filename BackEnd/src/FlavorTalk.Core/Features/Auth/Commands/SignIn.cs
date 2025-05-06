using FlavorTalk.Domain;
using FlavorTalk.Domain.Resources;
using FlavorTalk.Shared;
using FluentResults;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace FlavorTalk.Core.Features.Auth.Commands;
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

    public class Handler
    {
        public static async Task<Result> Handle(Command command,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IOptions<AppSettings> options)
        {
            var user = await userManager.FindByEmailAsync(command.Email);
            if (user is null) return Result.Fail(AuthError.UserNotFound);

            var result = await signInManager.PasswordSignInAsync(user, command.Password, command.RememberMe, false);
            if (!result.Succeeded) return Result.Fail(AuthError.CouldNotSignIn);

            return Result.Ok();
        }
    }
}
