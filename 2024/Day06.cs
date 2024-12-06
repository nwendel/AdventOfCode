namespace AdventOfCode._2024;

public class Day06 : AdventBase
{
    protected override object InternalPart1()
    {
        var map = Input.Lines.ParseMatrix();

        var position = map.Locate('^').Single();
        var direction = Direction4.North;

        var visited = new HashSet<Position2>();

        while (true)
        {
            visited.Add(position);

            var next = position.Move(direction);
            if (!map.Contains(next))
            {
                break;
            }

            if (map[next] == '#')
            {
                direction = direction.TurnRight();
            }
            else
            {
                position = next;
            }
        }

        return visited.Count;
    }

    protected override object InternalPart2()
    {
        var map = Input.Lines.ParseMatrix();

        var startPosition = map.Locate('^').Single();
        var startDirection = Direction4.North;

        var blocks = map.Locate('.').ToArray();

        var goodBlocks = 0L;

        foreach (var block in blocks)
        {
            map[block] = '#';

            var visited = new HashSet<(Position2, Direction4)>();
            var position = startPosition;
            var direction = startDirection;

            while (true)
            {
                if (!visited.Add((position, direction)))
                {
                    goodBlocks += 1;
                    break;
                }

                var next = position.Move(direction);
                if (!map.Contains(next))
                {
                    break;
                }

                if (map[next] == '#')
                {
                    direction = direction.TurnRight();
                }
                else
                {
                    position = next;
                }
            }

            map[block] = '.';
        }

        return goodBlocks;
    }
}
