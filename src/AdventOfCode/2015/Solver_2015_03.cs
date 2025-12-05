
namespace AdventOfCode._2015;

public class Solver_2015_03 : Solver<Direction4[]>
{
    protected override Direction4[] ParseInput(Input input)
    {
        var parsedInput = input
            .Select(x => x.ToDirection4())
            .ToArray();

        return parsedInput;
    }

    protected override Result SolvePart1Core(Direction4[] input)
    {
        var santa = new Position2(0, 0);
        var visited = new HashSet<Position2> { santa };

        foreach (var move in input)
        {
            santa = santa.Move(move);
            visited.Add(santa);
        }

        return visited.Count;
    }

    protected override Result SolvePart2Core(Direction4[] input)
    {
        var alternate = false;
        var santa = new Position2(0, 0);
        var roboSanta = new Position2(0, 0);

        var visited = new HashSet<Position2> { santa };

        foreach (var move in input)
        {
            if (alternate)
            {
                santa = santa.Move(move);
                visited.Add(santa);
            }
            else
            {
                roboSanta = roboSanta.Move(move);
                visited.Add(roboSanta);
            }

            alternate = !alternate;
        }

        return visited.Count;
    }
}
