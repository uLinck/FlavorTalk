using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
}
