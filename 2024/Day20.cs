namespace AdventOfCode._2024;

public class Day20 : AdventBase
{
    protected override object InternalPart1()
    {
        var map = Input.Lines.ParseMatrix();

        var start = map.Locate('S').Single();
        var end = map.Locate('E').Single();

        // 9452
        var goodCheats = 0;

        var queue = new Queue<(Position2 Current, int Steps, Position2? Cheated)>();
        var visited = new HashSet<(Position2, Position2? Cheated)>();
        queue.Enqueue((start, 0, null));
        visited.Add((start, null));

        while (queue.Count > 0)
        {
            var (current, steps, cheated) = queue.Dequeue();
            if (current == end)
            {
                if (steps <= 9452 - 100)
                {
                    goodCheats += 1;
                }
                continue;
            }
            foreach (var direction in Direction4s.All)
            {
                var next = current.Move(direction);
                if (map.Contains(next) && map[next] != '#')
                {
                    if (visited.Add((next, cheated)))
                    {
                        queue.Enqueue((next, steps + 1, cheated));
                    }
                }

                if (map.Contains(next) && map[next] == '#' && cheated == null)
                {
                    if (visited.Add((next, next)))
                    {
                        queue.Enqueue((next, steps + 1, next));
                    }
                }
            }
        }

        return goodCheats;
    }

    protected override object InternalPart2()
    {
        var map = Input.Lines.ParseMatrix();

        var start = map.Locate('S').Single();
        var end = map.Locate('E').Single();

        var distances = new Dictionary<Position2, int>();
        distances[start] = 0;

        var queue = new Queue<Position2>();
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            foreach (var direction in Direction4s.All)
            {
                var next = current.Move(direction);
                if (map.Contains(next) && map[next] != '#' && !distances.ContainsKey(next))
                {
                    distances[next] = distances[current] + 1;
                    queue.Enqueue(next);
                }
            }
        }

        // This assumes that the longest path is from S to E
        var positions = distances.Select(x => x.Key).ToArray();
        var combinations = AocCombinatorics.Combinations(positions, 2).ToArray();

        var goodCheats = combinations.Count(x =>
        {
            var distance = x[0].ManhattanDistanceTo(x[1]);
            if (distance > 20)
            {
                return false;
            }

            return Math.Abs(distances[x[0]] - distances[x[1]]) - distance >= 100;
        });

        return goodCheats;
    }
}
