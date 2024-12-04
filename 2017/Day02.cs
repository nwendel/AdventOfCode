namespace AdventOfCode._2017;

public class Day02 : AdventBase
{
    protected override object InternalPart1()
    {
        var checksum = 0L;

        foreach (var line in Input.Lines)
        {
            var numbers = line.ExtractNumbers();
            checksum += numbers.Max() - numbers.Min();
        }

        return checksum;
    }

    protected override object InternalPart2()
    {
        var checksum = 0L;

        foreach (var line in Input.Lines)
        {
            var numbers = line.ExtractNumbers();
            var combinations = AocMath.Combinations(numbers, numbers)
                .Where(x => x.First != x.Second)
                .ToArray();
            foreach (var combination in combinations)
            {
                var min = Math.Min(combination.First, combination.Second);
                var max = Math.Max(combination.First, combination.Second);

                if (max % min == 0)
                {
                    checksum += max / min;
                    break;
                }
            }
        }

        return checksum;
    }
}
