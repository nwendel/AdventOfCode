namespace AdventOfCode._2020;

public class Day01 : AdventBase
{
    protected override object InternalPart1()
    {
        var numbers = Input.Lines
            .Select(x => long.Parse(x))
            .ToArray();
        var combinations = AocMath.Combinations(numbers, 2, false);
        foreach (var combination in combinations)
        {
            if (combination.Sum() == 2020)
            {
                return combination.Product();
            }
        }

        throw new UnreachableException();
    }

    protected override object InternalPart2()
    {
        var numbers = Input.Lines
            .Select(x => long.Parse(x))
            .ToArray();

        var combinations = AocMath.Combinations(numbers, 3, false);
        foreach (var combination in combinations)
        {
            if (combination.Sum() == 2020)
            {
                return combination.Product();
            }
        }

        throw new UnreachableException();
    }
}
