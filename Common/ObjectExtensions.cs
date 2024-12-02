namespace AdventOfCode.Common;

public static class ObjectExtensions
{
    public static bool NotInRange<T>(this T self, params T[] values)
        => !values.Contains(self);
}
