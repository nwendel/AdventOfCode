namespace AdventOfCode.Common;

public static class MathExtensions
{
    extension(Math)
    {
        public static long Wrap(long value, Range range)
        {
            var min = range.Start.Value;
            var max = range.End.Value;
            var rangeSize = max - min;

            var wrappedValue = value - min;
            while (wrappedValue < 0)
            {
                wrappedValue += rangeSize;
            }

            wrappedValue %= rangeSize;

            return wrappedValue + min;
        }

        // Greatest-common-divisor
        public static long Gcd(IEnumerable<long> values)
            => values.Aggregate(Gcd);

        public static long Gcd(long a, long b)
        {
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }

        // least-common-multiple
        public static long Lcm(IEnumerable<long> values)
            => values.Aggregate(Lcm);

        public static long Lcm(long a, long b)
            => (a / Gcd(a, b)) * b;
    }
}
