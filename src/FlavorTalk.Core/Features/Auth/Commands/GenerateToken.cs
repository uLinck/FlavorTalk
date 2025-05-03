using FlavorTalk.Domain;
using FlavorTalk.Shared;
using FluentResults;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorTalk.Core.Features.Auth.Commands;
public static class GenerateToken
{
    public record Command(string Email, string Password)
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
    public record Response(string Token, DateTime ExpiresAtUtc);

    public class Handler
    {
        public static DateTime ExpiresAt => DateTime.UtcNow.AddDays(7);

        public static async Task<Result<Response>> Handle(Command command,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IOptions<AppSettings> options)
        {
            var user = await userManager.FindByEmailAsync(command.Email);
            if (user is null) return Result.Fail("User not found");

            var result = await signInManager.PasswordSignInAsync(user, command.Password, false, false);
            if (!result.Succeeded) return Result.Fail("Could not SignIn. Email or Password is incorrect.");

            var token = GenerateJwtToken(options.Value);

            return Result.Ok(new Response(token, ExpiresAt));
        }

        private static string GenerateJwtToken(AppSettings appSettings)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = ExpiresAt,
                Issuer = appSettings.Jwt.Issuer,
                Audience = appSettings.Jwt.Audience,
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}

