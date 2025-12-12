namespace AdventOfCode._2025._12;

// TODO: Since solution was much easier than expected, simplify the parsing.
public class Solver_2025_12 : Solver<ParsedInput>
{
    protected override ParsedInput ParseInput(Input input)
    {
        var blocks = input.Blocks;

        var shapes = blocks[..^1]
            .Select(x =>
            {
                var lines = x.Lines;
                var index = lines[0][..^1].ToInt();
                var grid = x.Lines.ToBlock(1..).ToMatrix(c => c == '#');
                return new Present(index, grid);
            })
            .ToArray();

        var regions = blocks[^1].Lines
            .Select(line =>
            {
                var parts = line.Split(": ");
                var size = parts[0].ToPosition2().Offset(-1, -1);
                var presentCounts = parts[1].ToLongs(" ");
                return new Region(new Rectangle2(size), presentCounts);
            })
            .ToArray();

        return new ParsedInput(shapes, regions);
    }

    protected override Result SolvePart1Core(ParsedInput input)
    {
        var shapeSizes = input.Shapes
            .Select(s => s.Grid.Count(x => x))
            .ToArray();

        var result = input.Regions.Count(r => r.Size.Area >= shapeSizes.Zip(r.PresentCounts, (size, count) => size * count).Sum());

        return result;
    }

    protected override Result SolvePart2Core(ParsedInput input)
    {
        throw new NoPuzzleException();
    }
}

public record ParsedInput(
    Present[] Shapes,
    Region[] Regions);

public record Present(
    int Index,
    Matrix2<bool> Grid);

public record Region(
    Rectangle2 Size,
    long[] PresentCounts);
