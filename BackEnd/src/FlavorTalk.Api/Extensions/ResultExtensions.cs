using FluentResults;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace FlavorTalk.Api.Extensions;

public static class ResultExtensions
{
    public static Exception ToException(this ResultBase result) => new(string.Join(", ", result.Errors.Select(e => e.Message)));
}

