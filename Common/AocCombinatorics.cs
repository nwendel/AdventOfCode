namespace AdventOfCode.Common;

public static class AocCombinatorics
{
    public static IEnumerable<T[]> Permutations<T>(T[] values, int count)
    {
        var indices = new int[count];
        while (true)
        {
            var combination = new T[count];
            for (int i = 0; i < count; i++)
            {
                combination[i] = values[indices[i]];
            }

            yield return combination;

            var k = count - 1;
            while (k >= 0 && indices[k] == values.Length - 1)
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
                indices[j] = 0;
            }
        }
    }

    public static IEnumerable<T[]> Permutations<T>(params T[][] values)
    {
        var indices = new int[values.Length];
        var n = values.Length;

        while (true)
        {
            var combination = new T[n];
            for (int i = 0; i < n; i++)
            {
                combination[i] = values[i][indices[i]];
            }

            yield return combination;

            var k = n - 1;
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

            var k = count - 1;
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
        var indices = new int[values.Length];
        var n = values.Length;

        while (true)
        {
            var combination = new T[n];
            for (int i = 0; i < n; i++)
            {
                combination[i] = values[i][indices[i]];
            }
            yield return combination;

            var k = n - 1;
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
