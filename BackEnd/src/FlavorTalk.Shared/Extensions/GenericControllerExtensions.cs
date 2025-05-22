using FlavorTalk.Shared.GenericControllersStuff;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FlavorTalk.Shared.Extensions;

public static class GenericControllerExtensions
{
    public static IMvcBuilder AddGenericControllers(this IMvcBuilder builder, Assembly coreAssembly)
    {
        builder.ConfigureApplicationPartManager(manager =>
        {
            manager.FeatureProviders.Add(new GenericControllerFeatureProvider(coreAssembly));
        });

        builder.Services.Configure<MvcOptions>(options =>
        {
            options.Conventions.Add(new GenericControllerRouteConvention());
        });

        return builder;
    }
}
