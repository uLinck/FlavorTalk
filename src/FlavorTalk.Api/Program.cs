using FlavorTalk.Api.Configs;
using FlavorTalk.Domain;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddAppSettingsConfigs()
    .AddAuth()
    .AddSwaggerConfigs()
    .AddDatabaseRelatedConfigs();

ValidatorOptions.Global.LanguageManager.Enabled = false;

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

app.Run();
