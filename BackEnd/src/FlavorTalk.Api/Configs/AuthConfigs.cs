using FlavorTalk.Domain.Entities;
using FlavorTalk.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace FlavorTalk.Api.Configs;

public static class AuthConfigs
{
    public static WebApplicationBuilder AddAuth(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication();

        builder.Services.AddIdentity<User, Role>(options => options.SignIn.RequireConfirmedAccount = true)
                        .AddEntityFrameworkStores<FlavorTalkContext>().AddDefaultTokenProviders();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            options.User.RequireUniqueEmail = true;
        });

        return builder;
    }
}
