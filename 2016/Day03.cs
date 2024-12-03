namespace AdventOfCode._2016;

public class Day03 : AdventBase
{
    protected override object InternalPart1()
    {
        var count = 0;

        foreach (var numbers in Input.Lines.ExtractNumbers())
        {
            var sorted = numbers.OrderBy(x => x).ToArray();
            if (sorted[0] + sorted[1] > sorted[2])
            {
                count += 1;
            }
        }

        return count;
    }

    protected override object InternalPart2()
    {
        var count = 0;

        var lines = Input.Lines.ExtractNumbers();
        for (var ix = 0; ix < lines.Length; ix += 3)
        {
            var numbers1 = lines[ix];
            var numbers2 = lines[ix + 1];
            var numbers3 = lines[ix + 2];
            for (var jx = 0; jx < 3; jx++)
            {
                var sorted = new[] { numbers1[jx], numbers2[jx], numbers3[jx] }.OrderBy(x => x).ToArray();
                if (sorted[0] + sorted[1] > sorted[2])
                {
                    count += 1;
                }
            }
        }

        return count;
    }
}
