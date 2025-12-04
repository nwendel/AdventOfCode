
namespace AdventOfCode._2025;

// TODO: Move the check if current positions has a paper roll to somewhere common?
public class Solver_2025_04 : Solver<Matrix2<bool>>
{
    protected override Matrix2<bool> ParseInput(Input input)
    {
        var parsedInput = input.ToMatrix(x => x == '@');

        return parsedInput;
    }

    protected override object SolvePart1Core(Matrix2<bool> input)
    {
        var result = 0L;

        foreach (var position in input.Positions)
        {
            if (!input[position])
            {
                continue;
            }

            if (AdjacentPaperRolls(input, position).Count() < 4)
            {
                result += 1;
            }
        }

        return result;
    }

    protected override object SolvePart2Core(Matrix2<bool> input)
    {
        var result = 0L;

        while (true)
        {
            var toRemove = new List<Position2>();

            foreach (var position in input.Positions)
            {
                if (!input[position])
                {
                    continue;
                }

                var adjacentPaperRolls = AdjacentPaperRolls(input, position);
                if (adjacentPaperRolls.Count() < 4)
                {
                    toRemove.AddRange(position);
                }
            }

            if (toRemove.Count == 0)
            {
                break;
            }

            input.Modify(toRemove, false);
            result += toRemove.Count;
        }

        return result;
    }

    private static IEnumerable<Position2> AdjacentPaperRolls(Matrix2<bool> input, Position2 position)
    {
        foreach (var check in position.Adjacent8)
        {
            if (input.Contains(check) && input[check])
            {
                yield return check;
            }
        }
    }
}

