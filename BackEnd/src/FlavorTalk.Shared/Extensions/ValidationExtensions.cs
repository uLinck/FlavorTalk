using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace FlavorTalk.Shared.Extensions;
public static class ValidationExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CannotBeNull<T>([NotNull] this T? value, [CallerArgumentExpression("value")] string? argumentName = null, string message = "Value cannot be null.") where T : class
    {
        if (value == null)
            throw new ArgumentNullException(argumentName, message);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CannotBeNull<T>([NotNull] this T? value, [CallerArgumentExpression("value")] string? argumentName = null, string message = "Value cannot be null.") where T : struct
    {
        if (!value.HasValue)
            throw new ArgumentNullException(argumentName, message);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CannotBeDefault<T>(this T value, [CallerArgumentExpression("value")] string? argumentName = null, string message = "Value cannot be default.") where T : struct
    {
        if (EqualityComparer<T>.Default.Equals(value, default))
            throw new ArgumentException(message, argumentName);
    }

    public static void CannotBeNullOrEmpty<T>([NotNull] this IEnumerable<T>? items, [CallerArgumentExpression(nameof(items))] string? argumentName = null, string message = "Value cannot be null or empty.")
    {
        items.CannotBeNull(argumentName: argumentName, message: message);

        if (items.IsEmpty())
            throw new ArgumentException(message, argumentName);

        if (default(T) is null) //Reference type
            foreach (var item in items)
                if (item is null)
                    (null as object).CannotBeNull(argumentName: argumentName, message: message);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEmpty<TSource>(this IEnumerable<TSource> source) => !source.Any();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEmpty<TSource>(this ICollection<TSource> source) => source.Count == 0;

    public static bool IsNullOrEmpty<TSource>([NotNullWhen(false)] this IEnumerable<TSource>? source) =>
        source == null || source.IsEmpty();
}
