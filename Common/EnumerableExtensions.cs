using System.Numerics;

namespace AdventOfCode.Common;

public static class EnumerableExtensions
{
    public static int CountAny<T>(this IEnumerable<T> self, IEnumerable<T> values)
        => values
            .Select(x => self.Count(e => EqualityComparer<T>.Default.Equals(e, x)))
            .Sum();

    public static T Product<T>(this IEnumerable<T> self)
        where T : INumber<T>
        => self.Aggregate(T.One, (a, b) => a * b);
}
