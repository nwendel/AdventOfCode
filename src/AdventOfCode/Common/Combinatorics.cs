namespace AdventOfCode.Common;

public static class Combinatorics
{
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
}
