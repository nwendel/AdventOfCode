namespace AdventOfCode._2020_01;

public class Solver_2020_01 : Solver<long[]>
{
    protected override long[] ParseInput(Input input)
    {
        var parsedInput = input.Lines
            .Select(line => line.ToLong())
            .ToArray();

        return parsedInput;
    }

    protected override Result SolvePart1Core(long[] input)
    {
        var combinations = Combinatorics.Combinations(input, 2);

        return SolveCore(combinations);
    }

    protected override Result SolvePart2Core(long[] input)
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
