namespace AdventOfCode.Common;

public static class StringArrayExtensions
{
    public static long[] ExtractNumber(this string[] self, int ix)
        => self.ExtractNumbers()[ix];

    public static long[][] ExtractNumbers(this string[] self)
    {
        var result = self
            .Select(x => x.ExtractNumbers())
            .ToArray();
        return result;
    }
}
