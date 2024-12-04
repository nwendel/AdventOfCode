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

    // permutations
    public static IEnumerable<(T First, T Second)> Permutations<T>(T[] first, T[] second)
    {
        foreach (var f in first)
        {
            foreach (var s in second)
            {
                yield return (f, s);
            }
        }
    }

    // combinations
    public static IEnumerable<(T First, T Second)> Combinations<T>(T[] first, T[] second)
    {
        for (int i = 0; i < first.Length; i++)
        {
            for (int j = i + 1; j < second.Length; j++)
            {
                yield return (first[i], second[j]);
            }
        }
    }
}
