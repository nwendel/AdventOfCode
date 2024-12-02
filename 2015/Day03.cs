namespace AdventOfCode._2015;

public class Day03 : AdventBase
{
    protected override object InternalPart1()
    {
        var santa = new Position2(0, 0);
        var visited = new HashSet<Position2> { santa };

        foreach (var move in Input.Text())
        {
            santa = santa.Move(move.ToDirection4());
            visited.Add(santa);
        }

        return visited.Count;
    }

    protected override object InternalPart2()
    {
        var alternate = false;

        var santa = new Position2(0, 0);
        var roboSanta = new Position2(0, 0);

        var visited = new HashSet<Position2> { santa };

        foreach (var move in Input.Text())
        {
            if (alternate)
            {
                santa = santa.Move(move.ToDirection4());
                visited.Add(santa);
            }
            else
            {
                roboSanta = roboSanta.Move(move.ToDirection4());
                visited.Add(roboSanta);
            }

            alternate = !alternate;
        }

        return visited.Count;
    }
}
