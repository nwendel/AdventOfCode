namespace AdventOfCode.Common;

public static class AocMath
{
    // least-common-multiplier
    public static long Lcm(params long[] values)
        => values.Aggregate((a, b) => LcmCore(a, b));

    public static long Lcm(IEnumerable<long> values)
        => values.Aggregate((a, b) => LcmCore(a, b));

    private static long LcmCore(long a, long b)
        => (a / GcdCore(a, b)) * b;

    // greatest-common-divisor
    public static long Gcd(params long[] values)
        => values.Aggregate((a, b) => GcdCore(a, b));

    public static long Gcd(IEnumerable<long> values)
        => values.Aggregate((a, b) => GcdCore(a, b));

    private static long GcdCore(long a, long b)
    {
        while (b != 0)
        {
            var t = b;
            b = a % b;
            a = t;
        }
        return a;
    }
}
