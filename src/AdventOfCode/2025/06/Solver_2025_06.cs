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

            Func<IEnumerable<long>, long>? operation = number[^1] switch
            {
                '*' => nums => nums.Product(),
                '+' => nums => nums.Sum(),
                _ => null
            };

            if (operation is not null)
            {
                number = number[..^1];
            }

            numbers.Add(long.Parse(number));

            if (operation is not null)
            {
                result += operation(numbers);
                numbers = [];
            }
        }

        return result;
    }
}

public record ParsedInput(
    long[] Numbers,
    char Operator);

