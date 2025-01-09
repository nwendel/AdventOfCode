namespace AdventOfCode._2024;

public class Day14 : AdventBase
{
    protected override object InternalPart1()
    {
        var width = 101;
        var height = 103;

        var robots = Input.Lines
            .Select(x =>
            {
                var n = x.ExtractNumbers();
                var robot = new Robot
                {
                    Position = new Position2(n[0], n[1]),
                    Vector = new Position2(n[2], n[3])
                };
                return robot;
            })
            .ToList();

        for (var ix = 0; ix < 100; ix++)
        {
            foreach (var robot in robots)
            {
                robot.Position = robot.Position.Offset(robot.Vector.X, robot.Vector.Y);
                if (robot.Position.X < 0)
                {
                    robot.Position = new Position2(width + robot.Position.X, robot.Position.Y);
                }
                if (robot.Position.X >= width)
                {
                    robot.Position = new Position2(robot.Position.X - width, robot.Position.Y);
                }
                if (robot.Position.Y < 0)
                {
                    robot.Position = new Position2(robot.Position.X, height + robot.Position.Y);
                }
                if (robot.Position.Y >= height)
                {
                    robot.Position = new Position2(robot.Position.X, robot.Position.Y - height);
                }
            }
        }

        var quadrants = new long[4];
        foreach (var robot in robots)
        {
            if (robot.Position.X == width / 2 || robot.Position.Y == height / 2)
            {
                continue;
            }

            if (robot.Position.X < width / 2 && robot.Position.Y < height / 2)
            {
                quadrants[0]++;
            }
            else if (robot.Position.X >= width / 2 && robot.Position.Y < height / 2)
            {
                quadrants[1]++;
            }
            else if (robot.Position.X < width / 2 && robot.Position.Y >= height / 2)
            {
                quadrants[2]++;
            }
            else
            {
                quadrants[3]++;
            }
        }

        return quadrants[0] * quadrants[1] * quadrants[2] * quadrants[3];
    }

    protected override object InternalPart2()
    {
        var width = 101;
        var height = 103;

        var robots = Input.Lines
            .Select(x =>
            {
                var n = x.ExtractNumbers();
                var robot = new Robot
                {
                    Position = new Position2(n[0], n[1]),
                    Vector = new Position2(n[2], n[3])
                };
                return robot;
            })
            .ToList();

        var ix = 0;
        while (true)
        {
            foreach (var robot in robots)
            {
                robot.Position = robot.Position.Offset(robot.Vector.X, robot.Vector.Y);
                if (robot.Position.X < 0)
                {
                    robot.Position = new Position2(width + robot.Position.X, robot.Position.Y);
                }
                if (robot.Position.X >= width)
                {
                    robot.Position = new Position2(robot.Position.X - width, robot.Position.Y);
                }
                if (robot.Position.Y < 0)
                {
                    robot.Position = new Position2(robot.Position.X, height + robot.Position.Y);
                }
                if (robot.Position.Y >= height)
                {
                    robot.Position = new Position2(robot.Position.X, robot.Position.Y - height);
                }
            }

            ix += 1;

            if (robots.Select(x => x.Position).Distinct().Count() == robots.Count)
            {
                break;
            }
        }

        return ix;
    }

    private class Robot
    {
        public required Position2 Position { get; set; }

        public required Position2 Vector { get; set; }
    }
}