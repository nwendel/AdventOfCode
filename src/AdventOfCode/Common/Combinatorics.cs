namespace AdventOfCode.Common;

public static class Combinatorics
{
    public static IEnumerable<T[]> CartesianProduct<T>(T[] values, int count)
    {
        if (count <= 0)
        {
            yield break;
        }

        var indices = new int[count];
        while (true)
        {
            var result = new T[count];
            for (var i = 0; i < count; i++)
            {
                result[i] = values[indices[i]];
            }

            yield return result;

            var ix = count - 1;
            while (ix >= 0 && indices[ix] == values.Length - 1)
            {
                ix--;
            }

            if (ix < 0)
            {
                yield break;
            }

            indices[ix]++;
            for (var i = ix + 1; i < count; i++)
            {
                indices[i] = 0;
            }
        }
    }

    public static IEnumerable<T[]> Combinations<T>(T[] values, int count)
    {
        if (count > values.Length)
        {
            yield break;
        }

        var ixs = new int[count];
        for (var ix = 0; ix < count; ix++)
        {
            ixs[ix] = ix;
        }

        while (true)
        {
            var combination = new T[count];
            for (var ix = 0; ix < count; ix++)
            {
                combination[ix] = values[ixs[ix]];
            }

            yield return combination;

            var k = count - 1;
            while (k >= 0 && ixs[k] == values.Length - count + k)
            {
                k--;
            }

            if (k < 0)
            {
                yield break;
            }

            ixs[k]++;
            for (var j = k + 1; j < count; j++)
            {
                ixs[j] = ixs[j - 1] + 1;
            }
        }
    }

    public static IEnumerable<T[]> Permutations<T>(T[] values)
    {
        var indices = new int[values.Length];
        while (true)
        {
            if (indices.Distinct().Count() == indices.Length)
            {
                var combination = new T[values.Length];
                for (var i = 0; i < values.Length; i++)
                {
                    combination[i] = values[indices[i]];
                }

                yield return combination;
            }

            var ix1 = values.Length - 1;
            while (ix1 >= 0 && indices[ix1] == values.Length - 1)
            {
                ix1--;
            }

            if (ix1 < 0)
            {
                yield break;
            }

            indices[ix1]++;

            for (var ix2 = ix1 + 1; ix2 < values.Length; ix2++)
            {
                indices[ix2] = 0;
            }
        }
    }
}
