namespace AdventOfCode._2018;

public class Day01 : AdventBase
{
    protected override object InternalPart1()
    {
        var sum = 0L;

        foreach (var line in Input.Lines)
        {
            sum += long.Parse(line);
        }

        return sum;
    }

    protected override object InternalPart2()
    {
        var sum = 0L;

        var seen = new HashSet<long>();
        while (true)
        {
            foreach (var line in Input.Lines)
            {
                sum += long.Parse(line);
                if (!seen.Add(sum))
                {
                    return sum;
                }
            }
        }
    }
}
