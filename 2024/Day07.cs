namespace AdventOfCode._2024;

public class Day07 : AdventBase
{
    protected override object InternalPart1()
    {
        char[] operators = ['+', '*'];
        return Solve(operators);
    }

    protected override object InternalPart2()
    {
        char[] operators = ['+', '*', '|'];
        return Solve(operators);
    }

    private long Solve(char[] operators)
    {
        var result = 0L;

        foreach (var line in Input.Lines)
        {
            var numbers = line.ExtractNumbers();
            var testValue = numbers[0];
            var remaining = numbers[1..] ?? throw new UnreachableException();

            var combinations = AocCombinatorics.Permutations(operators, remaining.Length - 1);

            foreach (var combination in combinations)
            {
                var left = remaining[0];
                for (int i = 0; i < combination.Length; i++)
                {
                    var right = remaining[i + 1];
                    left = combination[i] switch
                    {
                        '+' => left + right,
                        '*' => left * right,
                        '|' => long.Parse(left.ToString() + right.ToString()),
                        _ => throw new UnreachableException()
                    };
                }

                if (left == testValue)
                {
                    result += left;
                    break;
                }
            }
        }

        return result;
    }
}
