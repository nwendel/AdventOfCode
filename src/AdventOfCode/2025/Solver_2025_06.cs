namespace AdventOfCode._2025_06;

public class Solver_2025_06 : Solver
{
    protected override Result SolvePart1Core(Input input)
    {
        var result = 0L;

        var matrix = input.Lines
            .Split(" ")
            .ToMatrix();

        foreach (var column in matrix.Columns)
        {
            var numbers = column[..^1].ToLongs();
            result += column[^1].Text switch
            {
                "*" => numbers.Product(),
                "+" => numbers.Sum(),
                _ => throw new InvalidOperationException($"Unknown operator {column[^1].Text}"),
            };
        }

        return result;
    }

    // TODO: Can this be simplified?
    protected override Result SolvePart2Core(Input input)
    {
        var result = 0L;

        var matrix = input.ToMatrix();

        var numbers = new List<long>();
        foreach (var column in matrix.Columns.Reverse())
        {
            var number = string.Concat(column.Where(ch => ch != ' '));

            if (number == "")
            {
                continue;
            }

            char? oper = number[^1] is '*' or '+'
                ? number[^1]
                : null;
            if (oper is not null)
            {
                number = number[..^1];
            }

            numbers.Add(long.Parse(number));

            if (oper is not null)
            {
                var value = oper switch
                {
                    '*' => numbers.Product(),
                    '+' => numbers.Sum(),
                    _ => throw new InvalidOperationException($"Unknown operator {oper}"),
                };
                result += value;

                numbers = [];
            }
        }

        return result;
    }
}

public record ParsedInput(
    long[] Numbers,
    char Operator);

