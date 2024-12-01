namespace AdventOfCode.Common;

public static class LongEnumerableExtensions
{
    public static long Product(this IEnumerable<long> self)
        => self.Aggregate(1L, (a, b) => a * b);
}
