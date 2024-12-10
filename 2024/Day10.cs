namespace AdventOfCode._2024;

public class Day10 : AdventBase
{
    protected override object InternalPart1()
    {
        var map = Input.Lines.ParseMatrix();

        var result = 0L;
        var heads = map.Locate('0');
        foreach (var head in heads)
        {
            var visited = new HashSet<Position2>();
            var stack = new Stack<(Position2 Position, int Height)>();

            stack.Push((head, 0));
            var reachable = new HashSet<Position2>();

            while (stack.Any())
            {
                var p = stack.Pop();
                if (p.Height == 9)
                {
                    reachable.Add(p.Position);
                    continue;
                }

                foreach (var direction in Direction4s.All)
                {
                    var next = p.Position.Move(direction);
                    if (map.Contains(next) && !visited.Contains(next))
                    {
                        if (map[next] - '0' != p.Height + 1)
                        {
                            continue;
                        }
                        stack.Push((next, p.Height + 1));
                        visited.Add(next);
                    }
                }
            }

            result += reachable.Count;
        }

        return result;
    }

    protected override object InternalPart2()
    {
        var map = Input.Lines.ParseMatrix();

        var result = 0L;
        var heads = map.Locate('0');
        foreach (var head in heads)
        {
            var visited = new HashSet<Position2>();
            var stack = new Stack<(Position2 Position, int Height)>();

            stack.Push((head, 0));
            var paths = 0;

            while (stack.Count != 0)
            {
                var p = stack.Pop();
                if (p.Height == 9)
                {
                    paths += 1;
                    continue;
                }

                foreach (var direction in Direction4s.All)
                {
                    var next = p.Position.Move(direction);
                    if (map.Contains(next))
                    {
                        if (map[next] - '0' != p.Height + 1)
                        {
                            continue;
                        }
                        stack.Push((next, p.Height + 1));
                        visited.Add(next);
                    }
                }
            }

            result += paths;
        }

        return result;
    }
}
