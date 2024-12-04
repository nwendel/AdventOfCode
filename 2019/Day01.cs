namespace AdventOfCode._2019;

public class Day01 : AdventBase
{
    protected override object InternalPart1()
    {
        var fuel = 0L;

        foreach (var line in Input.Lines)
        {
            var mass = long.Parse(line);

            fuel += mass / 3 - 2;
        }

        return fuel;
    }

    protected override object InternalPart2()
    {
        var fuel = 0L;

        foreach (var line in Input.Lines)
        {
            var mass = long.Parse(line);

            while (true)
            {
                mass = mass / 3 - 2;
                if (mass <= 0)
                {
                    break;
                }

                fuel += mass;
            }
        }

        return fuel;
    }
}
