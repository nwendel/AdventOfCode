namespace AdventOfCode._2024;

public class Day04 : AdventBase
{
    protected override object InternalPart1()
    {
        var matrix = Input.Lines.ParseMatrix();

        var count = 0L;

        foreach (var line in matrix.Rows.Concat(matrix.Columns).Concat(matrix.Diagonals))
        {
            var text = new string(line);
            count += text.Count("XMAS");
            count += text.Count("SAMX");
        }

        return count;
    }

    protected override object InternalPart2()
    {
        var matrix = Input.Lines.ParseMatrix();

        var count = 0L;

        for (var y = 0; y < matrix.Height - 2; y++)
        {
            for (var x = 0; x < matrix.Width - 2; x++)
            {
                var text = new string([matrix[x, y], matrix[x + 1, y + 1], matrix[x + 2, y + 2], matrix[x + 2, y], matrix[x + 1, y + 1], matrix[x, y + 2]]);

                count += text.Count("MASMAS");
                count += text.Count("SAMSAM");
                count += text.Count("MASSAM");
                count += text.Count("SAMMAS");
            }
        }

        return count;
    }
}