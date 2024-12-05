namespace AdventOfCode.Common;

public static class ArrayExtensions
{
    public static T[] RemoveAt<T>(this T[] self, int index)
    {
        var result = new T[self.Length - 1];

        if (index > 0)
        {
            Array.Copy(self, 0, result, 0, index);
        }

        if (index < self.Length - 1)
        {
            Array.Copy(self, index + 1, result, index, self.Length - index - 1);
        }

        return result;
    }

    public static bool IsOrdered<T>(this T[] values)
    {
        for (int i = 0; i < values.Length - 1; i++)
        {
            if (Comparer<T>.Default.Compare(values[i], values[i + 1]) > 0)
            {
                return false;
            }
        }

        return true;
    }
}
