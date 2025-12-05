using System.Numerics;

namespace AdventOfCode._2025;

public class Solver_2025_05 : Solver<ParsedInput>
{
    protected override ParsedInput ParseInput(Input input)
    {
        var blocks = input.Blocks;
        var result = new ParsedInput(
            blocks[0].Lines.ToRanges(),
            blocks[1].ToLongs());

        return result;
    }

    protected override object SolvePart1Core(ParsedInput input)
    {
        var result = input.Ids.Count(id => input.Ranges.Any(range => range.Contains(id)));

        return result;
    }

    protected override object SolvePart2Core(ParsedInput input)
    {
        BigInteger result = 0;

        var ranges = input.Ranges.Merge();
        foreach (var range in ranges)
        {
            result += range.End - range.Start + 1;
        }

        return result;
    }
}

public record ParsedInput(
    IEnumerable<LongRange> Ranges,
    IEnumerable<long> Ids);
