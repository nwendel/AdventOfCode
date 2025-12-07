namespace AdventOfCode._2015._14;

public class Solver_2015_14 : Solver<Reindeer[]>
{
    protected override Reindeer[] ParseInput(Input input)
    {
        var parsedInput = input.Lines
            .Parse<Reindeer>("{Name} can fly {Speed} km/s for {FlyTime} seconds, but then must rest for {RestTime} seconds.");

        return parsedInput;
    }

    protected override Result SolvePart1Core(Reindeer[] input)
    {
        var result = input
            .Select(reindeer => CalculateDistance(reindeer, 2503))
            .Max();

        return result;
    }

    protected override Result SolvePart2Core(Reindeer[] input)
    {
        var points = input.ToDictionary(k => k.Name, _ => 0);

        for (var time = 1; time <= 2503; time++)
        {
            var distances = input
                .Select(reindeer => (reindeer.Name, Distance: CalculateDistance(reindeer, time)))
                .ToArray();

            var maxDistance = distances.Max(x => x.Distance);
            foreach (var (name, distance) in distances)
            {
                if (distance == maxDistance)
                {
                    points[name]++;
                }
            }
        }

        var result = points.Values.Max();

        return result;
    }

    private static long CalculateDistance(Reindeer reindeer, int time)
    {
        var cycleTime = reindeer.FlyTime + reindeer.RestTime;
        var cycles = time / cycleTime;
        var remaining = time % cycleTime;

        var distance = cycles * reindeer.Speed * reindeer.FlyTime;
        distance += reindeer.Speed * Math.Min(remaining, reindeer.FlyTime);

        return distance;
    }
}

public record Reindeer(
    string Name,
    long Speed,
    long FlyTime,
    long RestTime);
