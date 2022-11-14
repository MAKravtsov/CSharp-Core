using System.Diagnostics.CodeAnalysis;

namespace Common.Extensions;

public static class ArrayExtensions
{
    public static string ToJsonString<T>(
        [NotNull] this T[] array)
    {
        return $"[{string.Join(", ", array)}]";
    }
}
