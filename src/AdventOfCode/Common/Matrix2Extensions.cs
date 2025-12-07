namespace AdventOfCode.Common;

public static class Matrix2Extensions
{
    extension(Matrix2<char> self)
    {
        public string ToText()
        {
            var builder = new StringBuilder();

            for (var y = 0; y < self.Height; y++)
            {
                for (var x = 0; x < self.Width; x++)
                {
                    builder.Append(self[x, y]);
                }
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}
