
namespace AdventOfCode._2020;

public class Solver_2020_01 : Solver<long[]>
{
    public override Day Day => new(2020, 1);

    protected override long[] ParseInput(Input input)
    {
        var parsedInput = input.Lines
            .Select(line => line.ToLong())
            .ToArray();

        return parsedInput;
    }

    protected override object SolvePart1Core(long[] input)
    {
        var combinations = Combinatorics.Combinations(input, 2);

        return SolveCore(combinations);
    }

    protected override object SolvePart2Core(long[] input)
    {
        var combinations = Combinatorics.Combinations(input, 3);

        return SolveCore(combinations);
    }

    private static long SolveCore(IEnumerable<long[]> combinations)
    {
        var result = combinations
            .First(x => x.Sum() == 2020)
            .Product();

        return result;
    }
}
