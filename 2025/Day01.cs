namespace AdventOfCode._2025;

public class Day01 : AdventBase
{
    protected override object InternalPart1()
    {
        var result = 0;
        var p = 50;

        foreach (var line in Input.Lines)
        {
            var t = line.First().ToTurn2();
            var c = int.Parse(line[1..]);

            p = (p + c * (int)t + 10000) % 100;

            if (p == 0)
            {
                result += 1;
            }
        }

        return result;
    }

    protected override object InternalPart2()
    {
        var result = 0;
        var p = 50;

        foreach (var line in Input.Lines)
        {
            var t = line.First().ToTurn2();
            var c = int.Parse(line[1..]);

            for (var ix = 1; ix <= c; ix++)
            {
                p = (p + (int)t + 100) % 100;

                if (p == 0)
                {
                    result += 1;
                }
            }
        }

        return result;
    }
}
