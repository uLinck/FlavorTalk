using FlavorTalk.Core;
using FlavorTalk.Core.Features.Users.Commands;
using FlavorTalk.Domain;
using FlavorTalk.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Wolverine;
using Wolverine.EntityFrameworkCore;
using Wolverine.FluentValidation;
using Wolverine.SqlServer;

namespace FlavorTalk.Api.Configs;

public static class SqlServerConfigs
{
    public static WebApplicationBuilder AddDatabaseRelatedConfigs(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("Esqueceu de configurar a porra dos Secrets >:(");

        builder.Services.AddDbContext<FlavorTalkContext>(options => options.UseSqlServer(connectionString, options => options.MigrationsAssembly("FlavorTalk.Infrastructure")), ServiceLifetime.Singleton);

        // Could go to a separate file... but I'm lazy. And it's 6am.
        builder.Host.UseWolverine(opts =>
        {
            opts.PersistMessagesWithSqlServer(connectionString);
            opts.UseEntityFrameworkCoreTransactions();
            opts.Policies.UseDurableLocalQueues();
            opts.Durability.Mode = DurabilityMode.MediatorOnly;
            opts.Discovery.IncludeAssembly(typeof(Core.Setup).Assembly);
            opts.UseFluentValidation();
        });

        return builder;
    }

    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<FlavorTalkContext>();
        context.Database.Migrate();
    }

}
