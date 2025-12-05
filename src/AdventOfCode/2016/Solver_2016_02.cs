namespace AdventOfCode._2016;

public class Solver_2016_02 : Solver<Direction4[][]>
{
    protected override Direction4[][] ParseInput(Input input)
    {
        var parsedInput = input.Lines.ToDirection4s();

        return parsedInput;
    }

    protected override Result SolvePart1Core(Direction4[][] input)
    {
        var result = 0L;

        var position = new Position2(1, 1);
        var bounds = new Rectangle2(2, 2);

        foreach (var moves in input)
        {
            foreach (var move in moves)
            {
                position = position.Move(move).Clamp(bounds);
            }

            result = result * 10 + position.ToIndex(bounds) + 1;
        }

        return result;
    }

    protected override Result SolvePart2Core(Direction4[][] input)
    {
        var result = new StringBuilder();

        var keypad = "  1   234 56789 ABC   D  ";
        var position = new Position2(0, 2);
        var bounds = new Rectangle2(4, 4);

        foreach (var moves in input)
        {
            foreach (var move in moves)
            {
                var check = position.Move(move).Clamp(bounds);
                if (keypad[check.ToIndex(bounds)] != ' ')
                {
                    position = check;
                }
            }

            result.Append(keypad[position.ToIndex(bounds)]);
        }

        return result.ToString();
    }
}

