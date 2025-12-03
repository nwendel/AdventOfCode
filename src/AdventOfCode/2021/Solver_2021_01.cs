namespace AdventOfCode._2021;

public class Solver_2021_01 : Solver<long[]>
{
    public override Day Day => new(2021, 1);

    protected override long[] ParseInput(Input input)
    {
        var parsedInput = input.Lines
            .Select(x => x.ToLong())
            .ToArray();

        return parsedInput;
    }

    protected override object SolvePart1Core(long[] input)
    {
        var result = input
            .SlidingWindow(2)
            .Where(x => x[1] > x[0])
            .Count();

        return result;
    }

    protected override object SolvePart2Core(long[] input)
    {
        var modifiedInput = input
            .SlidingWindow(3)
            .Select(x => x.Sum())
            .ToArray();

        var result = SolvePart1Core(modifiedInput);

        return result;
    }
}
