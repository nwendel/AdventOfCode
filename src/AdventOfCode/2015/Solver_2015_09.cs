namespace AdventOfCode._2015_09;

internal class Solver_2015_09 : Solver<Graph<long>>
{
    protected override Graph<long> ParseInput(Input input)
    {
        var lines = input.Lines
            .Select(x => x.Split(" to ", " = "))
            .Select(x => new
            {
                From = new GraphNode(x[0].Text),
                To = new GraphNode(x[1].Text),
                Distance = x[2].ToLong(),
            })
            .ToArray();

        var nodes = new HashSet<GraphNode>();
        foreach (var line in lines)
        {
            nodes.Add(line.From);
            nodes.Add(line.To);
        }

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

    private IEnumerable<long> SolveCore(Graph<long> input)
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
