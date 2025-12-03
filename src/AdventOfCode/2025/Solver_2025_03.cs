

namespace AdventOfCode._2025;

public class Solver_2025_03 : Solver<string[]>
{
    public override Day Day => new(2025, 3);

    protected override string[] ParseInput(Input input)
    {
        var parsedInput = input.Lines
            .Select(x => x.Text)
            .ToArray();

        return parsedInput;
    }

    protected override object SolvePart1Core(string[] input)
    {
        var result = SolveCore(input, 2);

        return result;
    }

    protected override object SolvePart2Core(string[] input)
    {
        var result = SolveCore(input, 12);

        return result;
    }

    private long SolveCore(string[] input, int length)
    {
        var result = 0L;

        foreach (var line in input)
        {
            var remaining = line;
            var number = 0L;

            for (var ix = length - 1; ix >= 0; ix--)
            {
                var max = remaining[..^ix].Max();
                var index = remaining.IndexOf(max);

                number = number * 10 + max - '0';

                remaining = remaining[(index + 1)..];
            }

            result += number;
        }

        return result;
    }
}
