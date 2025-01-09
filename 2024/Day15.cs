namespace AdventOfCode._2024;

public class Day15 : AdventBase
{
    protected override object InternalPart1()
    {
        var map = Input.Blocks[0].Lines.ParseMatrix();

        var moves = Input.Blocks[1].Text.Trim();

        var robot = map.Locate('@').Single();
        map[robot] = '.';

        foreach (var move in moves)
        {
            var (dx, dy) = move switch
            {
                '^' => (0, -1),
                'v' => (0, 1),
                '>' => (1, 0),
                '<' => (-1, 0),
                _ => (0, 0),
            };

            if (dx == 0 && dy == 0)
            {
                continue;
            }

            var next = robot.Offset(dx, dy);
            if (map[next] == '.')
            {
                robot = next;
            }
            else if (map[next] == '#')
            {
                continue;
            }
            else
            {
                var n2 = next;
                while (true)
                {
                    n2 = n2.Offset(dx, dy);
                    if (map[n2] == '#')
                    {
                        break;
                    }
                    if (map[n2] == '.')
                    {
                        map[n2] = 'O';
                        map[next] = '.';
                        robot = next;
                        break;
                    }
                }
            }
        }

        var boxes = map.Locate('O')
            .Sum(P => 100 * P.Y + P.X);
        return boxes;
    }

    protected override object InternalPart2()
    {
        var map2 = Input.Blocks[0].Lines.ParseMatrix();

        var map = new Matrix<char>(map2.Width * 2, map2.Height);
        for (var x = 0; x < map2.Width; x++)
        {
            for (var y = 0; y < map2.Height; y++)
            {
                var p = new Position2(x, y);
                var p2 = new Position2(x * 2, y);
                var p3 = new Position2(x * 2 + 1, y);
                switch (map2[p])
                {
                    case '#':
                        map[p2] = '#';
                        map[p3] = '#';
                        break;
                    case '@':
                        map[p2] = '@';
                        map[p3] = '.';
                        break;
                    case 'O':
                        map[p2] = '[';
                        map[p3] = ']';
                        break;
                    case '.':
                        map[p2] = '.';
                        map[p3] = '.';
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }


        var moves = Input.Blocks[1].Text.Trim();

        var robot = map.Locate('@').Single();
        map[robot] = '.';

        foreach (var movet in moves)
        {
            var move = movet switch
            {
                '^' => Direction4.North,
                'v' => Direction4.South,
                '>' => Direction4.East,
                '<' => Direction4.West,
                _ => (Direction4)(-1),
            };

            if (move == (Direction4)(-1))
            {
                continue;
            }

            if (TryPush(map, [robot], move))
            {
                robot = robot.Move(move);
            }

        }

        var boxes = map.Locate('[')
            .Sum(P => 100 * P.Y + P.X);
        return boxes;
    }

    private bool TryPush(Matrix<char> map, Position2[] positions, Direction4 move)
    {
        if (move == Direction4.West || move == Direction4.East)
        {
            var next = positions.Single().Move(move);
            if (map[next] == '#')
            {
                return false;
            }
            else if (map[next] == '.' || TryPush(map, [next], move))
            {
                map[next] = map[positions.Single()];
                return true;
            }

            return false;
        }
        else
        {
            if (positions.Any(p => map[p.Move(move)] == '#'))
            {
                return false;
            }

            var np = positions
                .Select(p => p.Move(move))
                .SelectMany<Position2, Position2>(p => map[p] switch
                {
                    '.' => [],
                    '[' => [p, p.Offset(1, 0)],
                    ']' => [p, p.Offset(-1, 0)],
                })
                .Distinct()
                .ToArray();

            if (np.Length == 0 || TryPush(map, np, move))
            {
                var ms = positions
                    .Select(p => new { From = p, To = p.Move(move) })
                    .ToArray();
                foreach (var m in ms)
                {
                    map[m.To] = map[m.From];
                    map[m.From] = '.';
                }

                return true;
            }

            return false;
        }
    }
}