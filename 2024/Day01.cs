namespace AdventOfCode._2024;

public class Day01 : AdventBase
{
    protected override object InternalPart1()
    {
        var numbers = Input.Lines.ExtractNumbers();
        var left = numbers
            .Select(x => x[0])
            .OrderBy(x => x)
            .ToArray();
        var right = numbers
            .Select(x => x[1])
            .OrderBy(x => x)
            .ToArray();

        var result = left
            .Zip(right)
            .Sum(x => Math.Abs(x.First - x.Second));
        return result;
    }

    protected override object InternalPart2()
    {
        var numbers = Input.Lines.ExtractNumbers();
        var left = numbers
            .Select(x => x[0])
            .OrderBy(x => x)
            .ToArray();
        var right = numbers
            .Select(x => x[1])
            .GroupBy(x => x)
            .ToDictionary(x => x.Key, x => x.Count());

        var result = left
            .Sum(x => x * right.GetValueOrDefault(x));
        return result;
    }
}
