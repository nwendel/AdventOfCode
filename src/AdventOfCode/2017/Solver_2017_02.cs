namespace AdventOfCode._2017_02;

public class Solver_2017_02 : Solver<long[][]>
{
    protected override long[][] ParseInput(Input input)
    {
        var parsedInput = input.Lines.ToLongs("\t");

        return parsedInput;
    }

    protected override Result SolvePart1Core(long[][] input)
    {
        var result = input.Sum(row => row.Max() - row.Min());

        return result;
    }

    protected override Result SolvePart2Core(long[][] input)
    {
        var result = 0L;

        foreach (var line in input)
        {
            var combinations = Combinatorics.Combinations(line, 2);

            foreach (var combination in combinations)
            {
                var max = combination.Max();
                var min = combination.Min();

                if (max % min == 0)
                {
                    result += max / min;
                    break;
                }
            }
        }

        return result;
    }
}
