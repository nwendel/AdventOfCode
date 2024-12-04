namespace AdventOfCode._2016;

public class Day06 : AdventBase
{
    protected override object InternalPart1()
    {
        var matrix = Input.Lines.ParseMatrix();

        var message = string.Empty;
        foreach (var row in matrix.Columns)
        {
            var frequent = row
                .GroupBy(c => c)
                .Where(c => c.Key != 0)
                .OrderByDescending(g => g.Count())
                .First().Key;
            message += frequent;
        }

        return message;
    }

    protected override object InternalPart2()
    {
        var matrix = Input.Lines.ParseMatrix();

        var message = string.Empty;
        foreach (var row in matrix.Columns)
        {
            var frequent = row
                .GroupBy(c => c)
                .Where(c => c.Key != 0)
                .OrderBy(g => g.Count())
                .First().Key;
            message += frequent;
        }

        return message;
    }
}
