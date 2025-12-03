
namespace AdventOfCode._2025;

public class Solver_2025_02 : Solver<long[][]>
{
    protected override long[][] ParseInput(Input input)
    {
        var parsedInput = input
            .Split(",", "-")
            .Chunk(2)
            .ToLongs();

        return parsedInput;
    }

    protected override object SolvePart1Core(long[][] input)
    {
        var result = 0L;

        foreach (var range in input)
        {
            for (var ix = range[0]; ix <= range[1]; ix++)
            {
                var id = $"{ix}";

                if (id.Length % 2 == 0)
                {
                    var m = id.Length / 2;
                    if (id[..m] == id[m..])
                    {
                        result += ix;
                    }
                }
            }
        }

        return result;
    }

    protected override object SolvePart2Core(long[][] input)
    {
        var result = 0L;

        foreach (var range in input)
        {
            for (var ix = range[0]; ix <= range[1]; ix++)
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
