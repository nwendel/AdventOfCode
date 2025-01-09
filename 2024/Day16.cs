namespace AdventOfCode._2024;

public class Day16 : AdventBase
{
    protected override object InternalPart1()
    {
        var map = Input.Blocks[0].Lines.ParseMatrix();

        var start = map.Locate('S').Single();
        var end = map.Locate('E').Single();
        map[start] = '.';
        map[end] = '.';

        var visited = new HashSet<(Position2, Direction4)>
        {
            (start, Direction4.East)
        };

        var paths = new List<(Position2 Position, Direction4 Direction, long Score)>()
        {
            (start, Direction4.East, 0)
        };

        while (true)
        {
            var current = paths.OrderBy(x => x.Score).First();
            paths.Remove(current);

            var next = current.Position.Move(current.Direction);

            if (next == end)
            {
                return current.Score + 1;
            }

            if (map[next] != '#' && !visited.Contains((next, current.Direction)))
            {
                paths.Add((next, current.Direction, current.Score + 1));
                visited.Add((next, current.Direction));
            }

            var left = current.Direction.TurnLeft();
            if (!visited.Contains((current.Position, left)))
            {
                paths.Add((current.Position, left, current.Score + 1000));
                visited.Add((current.Position, left));
            }

            var right = current.Direction.TurnRight();
            if (!visited.Contains((current.Position, right)))
            {
                paths.Add((current.Position, right, current.Score + 1000));
                visited.Add((current.Position, right));
            }
        }

        throw new NotImplementedException();
    }

    protected override object InternalPart2()
    {
        var map = Input.Lines.ParseMatrix();
        var start = map.Locate('S').Single();

        var best = int.MaxValue;
        var visited = new Dictionary<(Position2 Position, Direction4 Direction), int>();
        var paths = new List<HashSet<Position2>>();
        var heads = new Queue<(Position2 Position, Direction4 Direction, int Score, HashSet<Position2> Path)>();
        heads.Enqueue((start, Direction4.East, 0, new HashSet<Position2> { start }));
        visited[(start, Direction4.East)] = 0;

        while (heads.Count > 0)
        {
            var trail = heads.Dequeue();
            var score = trail.Score;
            var (left, straight, right) = (trail.Direction.TurnLeft(), trail.Direction, trail.Direction.TurnRight());

            TryPath(left, score + 1001);
            TryPath(straight, score + 1);
            TryPath(right, score + 1001);

            void TryPath(Direction4 direction, int score)
            {
                var nextp = trail.Position.Move(direction);
                var newPath = new HashSet<Position2>(trail.Path)
                {
                    nextp,
                };

                var next = map[nextp];

                if (next == '#')
                {
                    return;
                }

                if (next == 'E')
                {
                    if (score <= best)
                    {
                        if (score < best)
                        {
                            paths.Clear();
                        }

                        best = score;
                        paths.Add(newPath);
                    }
                    return;
                }

                if (visited.TryGetValue((nextp, direction), out var visitedScore) && visitedScore < score)
                {
                    return;
                }

                visited[(nextp, direction)] = score;

                heads.Enqueue((nextp, direction, score, newPath));
            }
        }

        return best;
    }
}