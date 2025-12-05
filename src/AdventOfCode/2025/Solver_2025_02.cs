
namespace AdventOfCode._2025;

public class Solver_2025_02 : Solver<LongRange[]>
{
    protected override LongRange[] ParseInput(Input input)
    {
        var parsedInput = input
            .Split(",")
            .ToRanges();

        return parsedInput;
    }

    protected override object SolvePart1Core(LongRange[] input)
    {
        var result = 0L;

        foreach (var range in input)
        {
            foreach (var ix in range)
            {
                var id = $"{ix}";

                var m = id.Length / 2;
                if (id[..m] == id[m..])
                {
                    result += ix;
                }
            }
        }

        return result;
    }

    protected override object SolvePart2Core(LongRange[] input)
    {
        var result = 0L;

        foreach (var range in input)
        {
            foreach (var ix in range)
            {
                var id = $"{ix}";

                for (var checkLength = 1; checkLength <= id.Length / 2; checkLength++)
                {
                    var check = id[..checkLength];
                    var fullCheck = string.Concat(Enumerable.Repeat(check, id.Length / checkLength));

                    if (fullCheck == id)
                    {
                        result += ix;
                        break;
                    }
                }
            }
        }

        return result;
    }
}
