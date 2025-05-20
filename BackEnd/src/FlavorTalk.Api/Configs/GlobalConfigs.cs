using FluentResults;
using FluentValidation;

namespace FlavorTalk.Api.Configs;

public static class GlobalConfigs
{
    public static void SetupGlobalConfigs()
    {
        ValidatorOptions.Global.LanguageManager.Enabled = false;
        Result.Setup(options => options.DefaultTryCatchHandler = exception =>
        {
            var error = new Error(exception.Message);

            if (exception is ValidationException e)
                foreach (var err in e.Errors)
                    error = error.CausedBy(err.ErrorMessage);

            return error;
        });
    }
}
