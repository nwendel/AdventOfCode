namespace AdventOfCode._2024;

public class Day02 : AdventBase
{
    protected override object InternalPart1()
    {
        var count = 0;

        foreach (var line in Input.Lines)
        {
            var numbers = line.ExtractNumbers();
            if (IsSafe(numbers))
            {
                count += 1;
            }
        }

        return count;
    }

    protected override object InternalPart2()
    {
        var count = 0;

        foreach (var line in Input.Lines)
        {
            var numbers = line.ExtractNumbers();
            if (IsSafe(numbers) ||
                Enumerable.Range(0, numbers.Length).Any(x => IsSafe(numbers.RemoveAt(x))))
            {
                count += 1;
            }
        }

        return count;
    }

    private static bool IsSafe(long[] numbers)
    {
        bool ok = true;
        int? firstSign = null;

        for (var ix = 0; ix < numbers.Length - 1; ix++)
        {
            var sign = Math.Sign(numbers[ix] - numbers[ix + 1]);
            var diff = Math.Abs(numbers[ix] - numbers[ix + 1]);

            if ((firstSign != null && sign != firstSign) || diff.NotInRange(1, 2, 3))
            {
                return false;
            }

            firstSign ??= sign;
        }

        return ok;
    }
}
