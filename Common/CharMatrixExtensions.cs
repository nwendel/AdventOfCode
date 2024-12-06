using System.Text;

namespace AdventOfCode.Common;

public static class CharMatrixExtensions
{
    public static string ToText(this Matrix<char> self)
    {
        var sb = new StringBuilder();
        for (var y = 0; y < self.Height; y++)
        {
            for (var x = 0; x < self.Width; x++)
            {
                sb.Append(self[x, y]);
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }
}