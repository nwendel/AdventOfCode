namespace AdventOfCode.Common;

public static class EnumerableExtensions
{
    public static int CountAny<T>(this IEnumerable<T> self, IEnumerable<T> values)
        => values
            .Select(x => self.Count(e => EqualityComparer<T>.Default.Equals(e, x)))
            .Sum();
}
