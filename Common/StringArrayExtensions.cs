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

    public static Matrix<char> ParseMatrix(this string[] self)
    {
        var matrix = new Matrix<char>(self.Select(x => x.Length).Max(), self.Length);
        for (var y = 0; y < self.Length; y++)
        {
            for (var x = 0; x < self[y].Length; x++)
            {
                matrix[x, y] = self[y][x];
            }
        }
        return matrix;
    }
}
