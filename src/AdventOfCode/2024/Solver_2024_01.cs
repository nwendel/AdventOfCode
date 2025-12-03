namespace AdventOfCode2._2024;

public class Solver_2024_01 : Solver<ParsedInput>
{
    protected override ParsedInput ParseInput(Input input)
    {
        var lineNumbers = input.Lines.ToLongs(" ");

        var left = lineNumbers
            .Select(x => x[0])
            .OrderBy(x => x)
            .ToArray();
        var right = lineNumbers
            .Select(x => x[1])
            .OrderBy(x => x)
            .ToArray();

        return new(left, right);
    }

    protected override object SolvePart1Core(ParsedInput input)
    {
        var result = input.Left
            .Zip(input.Right)
            .Sum(x => Math.Abs(x.First - x.Second));

        return result;
    }

    protected override object SolvePart2Core(ParsedInput input)
    {
        var rightGroups = input.Right
            .GroupBy(x => x)
            .ToDictionary(x => x.Key, x => x.Count());

        var result = input.Left
            .Sum(x => x * rightGroups.GetValueOrDefault(x));

        return result;
    }
}

public sealed record ParsedInput(
    long[] Left,
    long[] Right);
