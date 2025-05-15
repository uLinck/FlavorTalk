using FlavorTalk.Api.Configs;

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

app.Run();
