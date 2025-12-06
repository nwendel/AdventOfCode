namespace AdventOfCode.Common;

public static class Combinatorics
{
    public static IEnumerable<T[]> CartesianProduct<T>(T[] values)
        => CartesianProduct(values, values.Length);

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

    public static IEnumerable<T[]> MultiCombinations<T>(T[] values)
        => MultiCombinations(values, values.Length);

    public static IEnumerable<T[]> MultiCombinations<T>(T[] values, int count)
    {
        if (count <= 0 || values.Length == 0)
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

            var newValue = indices[ix] + 1;
            for (var i = ix; i < count; i++)
            {
                indices[i] = newValue;
            }
        }
    }

    public static IEnumerable<T[]> Permutations<T>(T[] values)
        => Permutations(values, values.Length);

    public static IEnumerable<T[]> Permutations<T>(T[] values, int count)
    {
        if (count < 0 || count > values.Length)
        {
            yield break;
        }

        var used = new bool[values.Length];
        var current = new T[count];

        foreach (var permutation in PermutationsRecursive(values, count, used, current, 0))
        {
            yield return permutation;
        }
    }

    private static IEnumerable<T[]> PermutationsRecursive<T>(T[] values, int count, bool[] used, T[] current, int depth)
    {
        if (depth == count)
        {
            yield return (T[])current.Clone();
            yield break;
        }

        for (var i = 0; i < values.Length; i++)
        {
            if (used[i])
            {
                continue;
            }

            used[i] = true;
            current[depth] = values[i];

            foreach (var perm in PermutationsRecursive(values, count, used, current, depth + 1))
            {
                yield return perm;
            }

            used[i] = false;
        }
    }
}
