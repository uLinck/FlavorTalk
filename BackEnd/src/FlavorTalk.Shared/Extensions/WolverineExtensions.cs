using FluentResults;
using Wolverine;

namespace FlavorTalk.Shared.Extensions;

public static class WolverineExtensions
{
    public static async Task<Result<TRes>> TrySendAsync<TRes>(this IMessageBus bus, object command)
        where TRes : class
    {
        var res = await Result.Try(() =>
            bus.InvokeAsync<Result<TRes>>(command));

        if (res.IsFailed)
            return Result.Fail(res.Errors);

        return res.Value;
    }
}
