namespace AdventOfCode._2025_05;

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

    protected override Result SolvePart1Core(ParsedInput input)
    {
        var result = input.Ids.Count(id => input.Ranges.Any(range => range.Contains(id)));

        return result;
    }

    protected override Result SolvePart2Core(ParsedInput input)
    {
        var ranges = input.Ranges.Merge();
        var result = ranges.Sum(x => x.Count);

        return result;
    }
}

public record ParsedInput(
    IEnumerable<LongRange> Ranges,
    IEnumerable<long> Ids);
