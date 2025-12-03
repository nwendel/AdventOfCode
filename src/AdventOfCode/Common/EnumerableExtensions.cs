using System.Numerics;

namespace AdventOfCode.Common;

public static class EnumerableExtensions
{
    public static T Product<T>(this IEnumerable<T> self)
        where T : INumber<T>
        => self.Aggregate(T.One, (a, b) => a * b);

    public static IEnumerable<T> Repeat<T>(this IEnumerable<T> self)
    {
        while (true)
        {
            foreach (var item in self)
            {
                yield return item;
            }
        }
    }

    public static IEnumerable<T[]> SlidingWindow<T>(this IEnumerable<T> self, int size)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(size);

        var window = new Queue<T>(size);
        foreach (var item in self)
        {
            window.Enqueue(item);
            if (window.Count == size)
            {
                yield return window.ToArray();
                window.Dequeue();
            }
        }
    }
}
