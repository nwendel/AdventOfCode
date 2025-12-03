
namespace AdventOfCode._2015;

public class Solver_2015_02 : Solver<long[][]>
{
    protected override long[][] ParseInput(Input input)
    {
        var parsedInput = input.Lines.ToLongs("x");

        return parsedInput;
    }

    protected override object SolvePart1Core(long[][] input)
    {
        var result = input
            .Select(x => new[] { x[0] * x[1], x[1] * x[2], x[0] * x[2] })
            .Sum(x => x.Sum() * 2 + x.Min());

        return result;
    }

    protected override object SolvePart2Core(long[][] input)
    {
        var result = input
            .Select(x => x.OrderBy(e => e).ToArray())
            .Sum(x => (x[0] + x[1]) * 2 + x.Product());

        return result;
    }
}
