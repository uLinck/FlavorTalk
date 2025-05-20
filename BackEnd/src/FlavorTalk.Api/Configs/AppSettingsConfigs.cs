using FlavorTalk.Shared;

namespace FlavorTalk.Api.Configs;

public static class AppSettingsConfigs
{
    public static WebApplicationBuilder AddAppSettingsConfigs(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        return builder;
    }
}
