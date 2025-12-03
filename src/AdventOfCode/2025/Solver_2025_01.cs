namespace AdventOfCode2._2025;

public class Solver_2025_01 : Solver
{
    protected override object SolvePart1Core(Input input)
    {
        var result = 0L;
        var current = 50L;

        foreach (var line in input.Lines)
        {
            var turn = line[0].ToTurn2();
            var count = line[1..].ToLong();

            current = Math.Wrap(current + count * (long)turn, 0..99);
            if (current == 0)
            {
                result += 1;
            }
        }

        return result;
    }

    protected override object SolvePart2Core(Input input)
    {
        var result = 0L;
        var current = 50L;

        foreach (var line in input.Lines)
        {
            var turn = line[0].ToTurn2();
            var count = line[1..].ToLong();

            for (var ix = 0; ix < count; ix++)
            {
                current = Math.Wrap(current + (long)turn, 0..99);
                if (current == 0)
                {
                    result += 1;
                }
            }
        }

        return result;
    }
}
