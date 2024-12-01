using System.Text.RegularExpressions;

namespace AdventOfCode.Common;

public static partial class StringExtensions
{
    public static (T1, T2) Parse2<T1, T2>(this string self, char separator = ' ')
    {
        var parts = self.Split(separator, StringSplitOptions.RemoveEmptyEntries);

        return (ParseCore<T1>(parts[0]), ParseCore<T2>(parts[1]));
    }

    public static T[] ParseN<T>(string self, char separator = ' ')
    {
        var parts = self.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        var result = parts
            .Select(ParseCore<T>)
            .ToArray();
        return result;
    }

    public static long[] ExtractNumbers(this string self)
    {
        var matches = NumberRegex().Matches(self);
        var result = matches
            .Select(x => long.Parse(x.Value))
            .ToArray();
        return result;
    }

    [GeneratedRegex(@"-?\d+")]
    private static partial Regex NumberRegex();

    private static T ParseCore<T>(string input)
    {
        if (typeof(T) == typeof(string))
        {
            return (T)(object)input;
        }
        else if (typeof(T) == typeof(long))
        {
            return (T)(object)long.Parse(input);
        }
        else if (typeof(T) == typeof(double))
        {
            return (T)(object)double.Parse(input);
        }

        throw new NotSupportedException();
    }

    public static string SkipUntilIncluding(this string self, char value)
    {
        var ix = self.IndexOf(value) + 1;
        if (ix == 0)
        {
            throw new InvalidOperationException();
        }

        return self[ix..];
    }
}
