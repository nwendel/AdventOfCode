namespace AdventOfCode._2025;

public class Solver_2025_04 : Solver<Matrix2<Element>>
{
    protected override Matrix2<Element> ParseInput(Input input)
    {
        var parsedInput = input.ToMatrix(x => x == '@'
            ? Element.PaperRoll
            : Element.Empty);

        return parsedInput;
    }

    protected override Result SolvePart1Core(Matrix2<Element> input)
    {
        var result = 0L;

        foreach (var position in input.Positions)
        {
            if (CanAccess(input, position))
            {
                result += 1;
            }
        }

        return result;
    }

    protected override Result SolvePart2Core(Matrix2<Element> input)
    {
        var result = 0L;

        while (true)
        {
            var toRemove = new List<Position2>();

            foreach (var position in input.Positions)
            {
                if (CanAccess(input, position))
                {
                    toRemove.Add(position);
                }
            }

            if (toRemove.Count == 0)
            {
                break;
            }

            input.Modify(toRemove, Element.Empty);
            result += toRemove.Count;
        }

        return result;
    }

    private static bool CanAccess(Matrix2<Element> input, Position2 position)
    {
        if (input[position] == Element.Empty)
        {
            return false;
        }

        var adjacent = position.Adjacent8
            .Count(x => input.Contains(x) &&
                        input[x] == Element.PaperRoll);

        return adjacent < 4;
    }
}

public enum Element
{
    Empty,
    PaperRoll,
}

