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

    public static IEnumerable<T[]> Permutations<T>(params T[][] values)
    {
        var indices = new int[values.Length];
        while (true)
        {
            var combination = new T[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                combination[i] = values[i][indices[i]];
            }
            yield return combination;

            int k = values.Length - 1;
            while (k >= 0 && indices[k] == values[k].Length - 1)
            {
                k--;
            }

            if (k < 0)
            {
                yield break;
            }

            indices[k]++;
            for (int j = k + 1; j < values.Length; j++)
            {
                indices[j] = 0;
            }
        }
    }

    public static IEnumerable<T[]> Combinations<T>(T[] values, int count, bool allowSame)
    {
        var indices = new int[count];

        while (true)
        {
            if (!allowSame && indices.Distinct().Count() != count)
            {
                var combination = new T[count];
                for (int i = 0; i < count; i++)
                {
                    combination[i] = values[indices[i]];
                }

                yield return combination;
            }

            int k = count - 1;
            while (k >= 0 && indices[k] == values.Length - count + k)
            {
                k--;
            }

            if (k < 0)
            {
                yield break;
            }

            indices[k]++;

            for (int j = k + 1; j < count; j++)
            {
                indices[j] = indices[j - 1] + 1;
            }
        }
    }

    public static IEnumerable<T[]> Combinations<T>(params T[][] values)
    {
        int[] indices = new int[values.Length];
        int n = values.Length;

        while (true)
        {
            T[] combination = new T[n];
            for (int i = 0; i < n; i++)
            {
                combination[i] = values[i][indices[i]];
            }
            yield return combination;

            int k = n - 1;
            while (k >= 0)
            {
                indices[k]++;
                if (indices[k] < values[k].Length)
                {
                    break;
                }
                indices[k] = 0;
                k--;
            }

            if (k < 0)
            {
                break;
            }
        }
    }
}
