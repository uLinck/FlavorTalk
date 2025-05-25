using FlavorTalk.Api.Configs;
using FlavorTalk.Api.Extensions;
using FlavorTalk.Shared.Extensions;
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

app.MapCommandEndpoints(typeof(FlavorTalk.Core.Setup).Assembly);

app.Run();
