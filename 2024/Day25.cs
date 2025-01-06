namespace AdventOfCode._2024;

public class Day25 : AdventBase
{
    protected override object InternalPart1()
    {
        var locks = new List<int[]>();
        var keys = new List<int[]>();

        var width = Input.Lines.First().Length;

        foreach (var block in Input.Blocks)
        {
            var lines = block.Lines;
            var matrix = lines.ParseMatrix();
            if (matrix.Row(0).Count(x => x == '#') == matrix.Width)
            {
                locks.Add(matrix.Columns.Select(x => x.Count(x => x == '#')).ToArray());
            }
            else
            {
                keys.Add(matrix.Columns.Select(x => x.Count(x => x == '#')).ToArray());
            }
        }

        int count = 0;

        foreach (var l in locks)
        {
            foreach (var k in keys)
            {
                var fits = l.Zip(k).All(x => x.First + x.Second <= width);
                if (fits)
                {
                    count += 1;
                }
            }
        }

        return count;
    }

    protected override object InternalPart2()
    {
        return 0;
    }
}
