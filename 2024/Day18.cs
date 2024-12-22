namespace AdventOfCode._2024;

public class Day18 : AdventBase
{
    protected override object InternalPart1()
    {
        var map = new Matrix<char>(71, 71);
        var bytes = Input.Lines.ExtractNumbers()
            .Select(x => new Position2(x[0], x[1]))
            .ToArray();

        for (var ix = 0; ix < 1024; ix++)
        {
            map[bytes[ix]] = '#';
        }

        var end = new Position2(map.Width - 1, map.Height - 1);


        var queue = new Queue<(Position2 pos, int steps)>();
        queue.Enqueue((new Position2(0, 0), 0));
        var visited = new HashSet<Position2>();

        while (queue.Count > 0)
        {
            var (current, steps) = queue.Dequeue();
            if (current.Equals(end))
            {
                return steps;
            }

            foreach (var direction in Direction4s.All)
            {
                var next = current.Move(direction);
                if (map.Contains(next) && !visited.Contains(next) && map[next] == 0)
                {
                    queue.Enqueue((next, steps + 1));
                    visited.Add(next);
                }
            }
        }

        throw new UnreachableException();
    }

    protected override object InternalPart2()
    {
        var ix = 1024;

        while (true)
        {
            var reachedExit = false;

            var map = new Matrix<char>(71, 71);
            var bytes = Input.Lines.ExtractNumbers()
                .Select(x => new Position2(x[0], x[1]))
                .ToArray();

            for (var ix2 = 0; ix2 < ix; ix2++)
            {
                map[bytes[ix2]] = '#';
            }

            var end = new Position2(map.Width - 1, map.Height - 1);

            var queue = new Queue<(Position2 pos, int steps)>();
            queue.Enqueue((new Position2(0, 0), 0));
            var visited = new HashSet<Position2>();

            while (queue.Count > 0)
            {
                var (current, steps) = queue.Dequeue();
                if (current.Equals(end))
                {
                    reachedExit = true;
                    break;
                }

                foreach (var direction in Direction4s.All)
                {
                    var next = current.Move(direction);
                    if (map.Contains(next) && !visited.Contains(next) && map[next] == 0)
                    {
                        queue.Enqueue((next, steps + 1));
                        visited.Add(next);
                    }
                }
            }

            if (!reachedExit)
            {
                foreach (var p in map.Locate(x => true))
                {
                    if (map[p] == 0)
                    {
                        map[p] = ' ';
                    }
                }

                Console.WriteLine(map.ToText());

                return bytes[ix - 1].ToString();
            }

            ix += 1;
        }


        throw new UnreachableException();
    }
}
