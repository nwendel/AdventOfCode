
namespace AdventOfCode.Common;

// TODO: This needs to be re-evalutaed when more puzzles needs graphs!
public class Graph<TEdge>
{
    public Graph(IEnumerable<GraphNode> nodes, IEnumerable<GraphEdge<TEdge>> edges)
    {
        Nodes = nodes.ToArray();
        Edges = edges.ToArray();
    }

    public GraphNode[] Nodes { get; }

    public GraphEdge<TEdge>[] Edges { get; }

    internal GraphEdge<TEdge> GetEdge(GraphNode node1, GraphNode node2)
        => Edges.Single(e => (e.From == node1 && e.To == node2) || (e.From == node2 && e.To == node1));
}
