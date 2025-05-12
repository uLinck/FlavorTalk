using FlavorTalk.Api.Configs;
using FluentResults;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddAppSettingsConfigs()
    .AddAuth()
    .AddControllerConfigs()
    .AddDatabaseRelatedConfigs();

ValidatorOptions.Global.LanguageManager.Enabled = false;
Result.Setup(options => options.DefaultTryCatchHandler = exception =>
{
    var error = new Error(exception.Message);

    if (exception is ValidationException e)
        foreach (var err in e.Errors)
            error = error.CausedBy(err.ErrorMessage);

    return error;
});

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
