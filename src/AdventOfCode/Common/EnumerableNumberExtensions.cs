using System.Numerics;

namespace AdventOfCode.Common;

public static class EnumerableNumberExtensions
{
    extension<T>(IEnumerable<T> self)
        where T : INumber<T>
    {
        public T Product()
            => self.Aggregate(T.One, (a, b) => a * b);
    }
}
