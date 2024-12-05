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
            var combinations = AocMath.Combinations(numbers, 2, false);
            foreach (var combination in combinations)
            {
                var min = combination.Min();
                var max = combination.Max();

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
