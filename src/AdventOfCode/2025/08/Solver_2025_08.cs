namespace AdventOfCode._2025._08;

public class Solver_2025_08 : Solver<Connection[]>
{
    protected override Connection[] ParseInput(Input input)
    {
        var boxes = input.Lines
            .Select(line =>
            {
                var coords = line.ToLongs(",");
                return new Position3(coords[0], coords[1], coords[2]);
            })
            .ToArray();

        var parsedInput = Combinatorics.Combinations(boxes, 2)
            .Select(x => new Connection(
                From: x[0],
                To: x[1],
                Distance: x[0].DistanceTo(x[1])))
            .OrderBy(x => x.Distance)
            .ToArray();

        return parsedInput;
    }

    protected override Result SolvePart1Core(Connection[] input)
    {
        var circuits = new List<List<Position3>>();

        foreach (var connection in input.Take(1000))
        {
            TryConnectCircuits(circuits, connection);
        }

        var result = circuits
            .Select(c => c.Count)
            .OrderByDescending(x => x)
            .Take(3)
            .Product();

        return result;
    }

    protected override Result SolvePart2Core(Connection[] input)
    {
        var boxCount = input
            .SelectMany(x => new[] { x.From, x.To })
            .Distinct()
            .Count();

        var circuits = new List<List<Position3>>();

        foreach (var connection in input)
        {
            if (TryConnectCircuits(circuits, connection))
            {
                if (circuits[0].Count == boxCount)
                {
                    var result = connection.From.X * connection.To.X;

                    return result;
                }
            }
        }

        throw new UnreachableException();
    }

    private static bool TryConnectCircuits(List<List<Position3>> circuits, Connection connection)
    {
        var circuit1 = circuits.SingleOrDefault(c => c.Contains(connection.From));
        var circuit2 = circuits.SingleOrDefault(c => c.Contains(connection.To));

        if (circuit1 != null && circuit1 == circuit2)
        {
            return false;
        }

        if (circuit1 == null && circuit2 == null)
        {
            circuits.Add([connection.From, connection.To]);
        }
        else if (circuit1 != null && circuit2 == null)
        {
            circuit1.Add(connection.To);
        }
        else if (circuit2 != null && circuit1 == null)
        {
            circuit2.Add(connection.From);
        }
        else if (circuit1 != null && circuit2 != null)
        {
            circuit1.AddRange(circuit2);
            circuits.Remove(circuit2);
        }

        return true;
    }
}

public record Connection(
    Position3 From,
    Position3 To,
    long Distance);
