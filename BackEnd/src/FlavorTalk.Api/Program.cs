using FlavorTalk.Api.Configs;
using FlavorTalk.Api.Extensions;
using FlavorTalk.Shared.GenericControllersStuff;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddAppSettingsConfigs()
    .AddAuth()
    .AddControllerConfigs()
    .AddDatabaseRelatedConfigs();

GlobalConfigs.SetupGlobalConfigs();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var endpointDataSources = app.Services.GetRequiredService<IEnumerable<EndpointDataSource>>();
foreach (var dataSource in endpointDataSources)
{
    foreach (var endpoint in dataSource.Endpoints)
    {
        Console.WriteLine($"[ENDPOINT] {endpoint.DisplayName}");
    }
}

app.Run();
