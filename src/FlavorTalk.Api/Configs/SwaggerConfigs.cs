using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace FlavorTalk.Api.Configs;

public static class SwaggerConfigs
{
    public static WebApplicationBuilder AddSwaggerConfigs(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "FlavorTalk API", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {{
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }});
            c.CustomSchemaIds(type => type.ToString().Replace("+", "."));
            c.MapType<TimeSpan>(() => new OpenApiSchema { Type = "string", Format = "time-span" });
        });

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        return builder;
    }
}
