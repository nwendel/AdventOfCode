namespace AdventOfCode._2024;

public class Day05 : AdventBase
{
    protected override object InternalPart1()
    {
        var sum = 0L;

        var rules = new List<(long, long)>();
        foreach (var line in Input.Blocks[0].Lines)
        {
            var numbers = line.ExtractNumbers();
            rules.Add((numbers[0], numbers[1]));
        }

        foreach (var line in Input.Blocks[1].Lines)
        {
            var numbers = line.ExtractNumbers();
            if (IsCorrect(numbers))
            {
                sum += numbers[numbers.Length / 2];
            }
        }

        return sum;

        bool IsCorrect(long[] numbers)
        {
            for (var ix = 0; ix < numbers.Length - 1; ix++)
            {
                for (var ix2 = ix + 1; ix2 < numbers.Length; ix2++)
                {
                    if (rules.Any(x => x.Item1 == numbers[ix2] && x.Item2 == numbers[ix]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }

    protected override object InternalPart2()
    {
        var sum = 0L;

        var rules = new List<(long, long)>();
        foreach (var line in Input.Blocks[0].Lines)
        {
            var numbers = line.ExtractNumbers();
            rules.Add((numbers[0], numbers[1]));
        }

        foreach (var line in Input.Blocks[1].Lines)
        {
            var numbers = line.ExtractNumbers();
            if (Reorder(numbers))
            {
                sum += numbers[numbers.Length / 2];
            }
        }

        return sum;

        bool Reorder(long[] numbers)
        {
            var change = true;
            var incorrect = false;

            while (change)
            {
                change = false;
                for (var ix = 0; ix < numbers.Length - 1; ix++)
                {
                    for (var ix2 = ix + 1; ix2 < numbers.Length; ix2++)
                    {
                        if (rules.Any(x => x.Item1 == numbers[ix2] && x.Item2 == numbers[ix]))
                        {
                            (numbers[ix2], numbers[ix]) = (numbers[ix], numbers[ix2]);
                            change = true;
                            incorrect = true;
                        }
                    }
                }
            }

            return incorrect;
        }
    }
}
