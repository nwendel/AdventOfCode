namespace AdventOfCode._2015_09;

internal class Solver_2015_09 : Solver<Graph<long>>
{
    protected override Graph<long> ParseInput(Input input)
    {
        var lines = input.Lines
            .Parse<Route>("{From} to {To} = {Distance}");

        var nodes = lines
            .SelectMany(x => new[] { x.From, x.To })
            .ToHashSet();

        var edges = lines
            .Select(x => new GraphEdge<long>(x.From, x.To, x.Distance))
            .ToArray();

        var parsedInput = new Graph<long>(nodes, edges);

        return parsedInput;
    }

    protected override Result SolvePart1Core(Graph<long> input)
    {
        var result = SolveCore(input).Min();

        return result;
    }

    protected override Result SolvePart2Core(Graph<long> input)
    {
        var result = SolveCore(input).Max();
        return result;
    }

    private static IEnumerable<long> SolveCore(Graph<long> input)
    {
        var permutations = Combinatorics.Permutations(input.Nodes);

        foreach (var nodes in permutations)
        {
            var distance = 0L;

            for (var i = 1; i < nodes.Length; i++)
            {
                var edge = input.GetEdge(nodes[i - 1], nodes[i]);
                distance += edge.Value;
            }

            yield return distance;
        }
    }
}

public record Route(
    GraphNode From,
    GraphNode To,
    long Distance);
