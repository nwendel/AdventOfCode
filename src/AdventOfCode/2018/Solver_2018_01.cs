namespace AdventOfCode._2018_01;

public class Solver_2018_01 : Solver<long[]>
{
    protected override long[] ParseInput(Input input)
    {
        var parsedInput = input.Lines
            .Select(line => long.Parse(line.Text))
            .ToArray();

        return parsedInput;
    }

    protected override Result SolvePart1Core(long[] input)
    {
        var result = input.Sum();

        return result;
    }

    protected override Result SolvePart2Core(long[] input)
    {
        var result = 0L;

        var seen = new HashSet<long>();

        foreach (var delta in input.Repeat())
        {
            result += delta;

            if (!seen.Add(result))
            {
                return result;
            }
        }

        throw new UnreachableException();
    }
}
