namespace AdventOfCode._2024;

public class Day12 : AdventBase
{
    protected override object InternalPart1()
    {
        var map = Input.Lines.ParseMatrix();

        var cost = 0L;

        foreach (var position in map.Locate(x => true))
        {
            var plant = map[position];

            if (plant == ' ')
            {
                continue;
            }

            var next = new Stack<Position2>();
            next.Push(position);

            var area = 0L;
            var fence = 0L;

            var areap = new HashSet<Position2>();

            while (next.Count > 0)
            {
                var current = next.Pop();
                map[current] = ' ';
                area += 1;

                areap.Add(current);

                foreach (var adjacent in Direction4s.All.Select(x => current.Move(x)))
                {
                    if (!map.Contains(adjacent))
                    {
                        fence++;
                        continue;
                    }
                    else if (map[adjacent] != plant)
                    {
                        if (!areap.Contains(adjacent))
                        {
                            fence++;
                        }
                        continue;
                    }
                    else
                    {
                        if (!next.Contains(adjacent))
                        {
                            next.Push(adjacent);
                        }
                    }
                }
            }

            cost += area * fence;
        }

        return cost;
    }

    protected override object InternalPart2()
    {
        var map2 = Input.Lines.ParseMatrix();

        var map = new Matrix<char>(map2.Width * 3, map2.Height * 3);
        for (var x = 0; x < map2.Width; x++)
        {
            for (var y = 0; y < map2.Height; y++)
            {
                for (var dx = 0; dx < 3; dx++)
                {
                    for (var dy = 0; dy < 3; dy++)
                    {
                        map[x * 3 + dx, y * 3 + dy] = map2[x, y];
                    }
                }
            }
        }

        var cost = 0L;

        foreach (var position in map.Locate(x => true))
        {
            var plant = map[position];

            if (plant == ' ')
            {
                continue;
            }

            var next = new Stack<Position2>();
            next.Push(position);

            var area = 0L;
            var sides = new List<Position2>();

            var areap = new HashSet<Position2>();

            while (next.Count > 0)
            {
                var current = next.Pop();
                map[current] = ' ';
                area += 1;

                areap.Add(current);

                foreach (var adjacent in Direction4s.All.Select(x => current.Move(x)))
                {
                    if (!map.Contains(adjacent))
                    {
                        sides.Add(adjacent);
                        continue;
                    }
                    else if (map[adjacent] != plant)
                    {
                        if (!areap.Contains(adjacent))
                        {
                            sides.Add(adjacent);
                        }
                        continue;
                    }
                    else
                    {
                        if (!next.Contains(adjacent))
                        {
                            next.Push(adjacent);
                        }
                    }
                }
            }

            // Remove inner corners
            var toRemove = new HashSet<Position2>();
            foreach (var side in sides)
            {
                if (sides.Contains(side.Offset(-1, 0)) && sides.Contains(side.Offset(0, -1)))
                {
                    toRemove.Add(side);
                }

                if (sides.Contains(side.Offset(1, 0)) && sides.Contains(side.Offset(0, -1)))
                {
                    toRemove.Add(side);
                }

                if (sides.Contains(side.Offset(-1, 0)) && sides.Contains(side.Offset(0, 1)))
                {
                    toRemove.Add(side);
                }

                if (sides.Contains(side.Offset(1, 0)) && sides.Contains(side.Offset(0, 1)))
                {
                    toRemove.Add(side);
                }
            }
            sides.RemoveAll(x => toRemove.Contains(x));

            var sideCount = 0L;

            while (sides.Any())
            {
                var remaining = new Stack<Position2>();
                remaining.Push(sides[0]);
                sideCount += 1;

                while (remaining.Count > 0)
                {
                    var current = remaining.Pop();
                    sides.Remove(current);
                    foreach (var adjacent in Direction4s.All.Select(x => current.Move(x)))
                    {
                        if (sides.Contains(adjacent))
                        {
                            remaining.Push(adjacent);
                        }
                    }
                }
            }

            cost += area / 9 * sideCount;
        }

        return cost;
    }
}
