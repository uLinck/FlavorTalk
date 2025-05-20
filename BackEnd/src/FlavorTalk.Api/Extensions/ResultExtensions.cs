using FluentResults;

namespace FlavorTalk.Api.Extensions;

public static class ResultExtensions
{
    public static Exception ToException(this ResultBase result) => new(string.Join(", ", result.Errors.Select(e => e.Message)));
}
