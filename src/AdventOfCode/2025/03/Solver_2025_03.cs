namespace AdventOfCode._2025_03;

public class Solver_2025_03 : Solver<string[]>
{
    protected override string[] ParseInput(Input input)
    {
        var parsedInput = input.Lines
            .Select(x => x.Text)
            .ToArray();

        return parsedInput;
    }

    protected override Result SolvePart1Core(string[] input)
    {
        var result = SolveCore(input, 2);

        return result;
    }

    protected override Result SolvePart2Core(string[] input)
    {
        var result = SolveCore(input, 12);

        return result;
    }

    private static long SolveCore(string[] input, int length)
    {
        var result = 0L;

        foreach (var line in input)
        {
            var remaining = line;
            var number = 0L;

            for (var keep = length - 1; keep >= 0; keep--)
            {
                var max = remaining[..^keep].Max();
                var ix = remaining.IndexOf(max);
                remaining = remaining[(ix + 1)..];

                number = number * 10 + max - '0';
            }

            result += number;
        }

        return result;
    }
}
