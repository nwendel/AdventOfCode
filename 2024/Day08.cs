namespace AdventOfCode._2024;

public class Day08 : AdventBase
{
    protected override object InternalPart1()
    {
        var map = Input.Lines.ParseMatrix();

        var antinodes = new HashSet<Position2>();

        var antennaTypes = map.All
            .Where(x => x != '.')
            .ToHashSet();
        foreach (var antennaType in antennaTypes)
        {
            var positions = map
                .Locate(antennaType)
                .ToArray();

            if (positions.Length == 1)
            {
                continue;
            }

            var combinations = AocCombinatorics.Combinations(positions, 2, false).ToArray();

            foreach (var combination in combinations)
            {
                var (first, second) = (combination[0], combination[1]);
                var dx = first.X - second.X;
                var dy = first.Y - second.Y;

                var check1 = new Position2(first.X + dx, first.Y + dy);
                if (map.Contains(check1))
                {
                    antinodes.Add(check1);
                }


                var check2 = new Position2(second.X - dx, second.Y - dy);
                if (map.Contains(check2))
                {
                    antinodes.Add(check2);
                }
            }
        }

        return antinodes.Count;
    }

    protected override object InternalPart2()
    {
        var map = Input.Lines.ParseMatrix();

        var antinodes = new HashSet<Position2>();

        var antennaTypes = map.All
            .Where(x => x != '.')
            .ToHashSet();
        foreach (var antennaType in antennaTypes)
        {
            var positions = map
                .Locate(antennaType)
                .ToArray();

            foreach (var position in positions)
            {
                antinodes.Add(position);
            }

            if (positions.Length == 1)
            {
                continue;
            }

            var combinations = AocCombinatorics.Combinations(positions, 2, false).ToArray();

            foreach (var combination in combinations)
            {
                var (first, second) = (combination[0], combination[1]);
                var dx = first.X - second.X;
                var dy = first.Y - second.Y;

                var ix = 1;
                while (true)
                {
                    var check1 = new Position2(first.X + dx * ix, first.Y + dy * ix);
                    if (map.Contains(check1))
                    {
                        antinodes.Add(check1);
                    }
                    else
                    {
                        break;
                    }

                    ix += 1;
                }


                ix = 1;
                while (true)
                {
                    var check2 = new Position2(second.X - dx * ix, second.Y - dy * ix);
                    if (map.Contains(check2))
                    {
                        antinodes.Add(check2);
                    }
                    else
                    {
                        break;
                    }

                    ix += 1;
                }
            }
        }

        return antinodes.Count;
    }
}
