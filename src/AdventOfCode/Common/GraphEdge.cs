namespace AdventOfCode.Common;

public class GraphEdge<T>
{
    public GraphEdge(GraphNode from, GraphNode to, T value)
    {
        From = from;
        To = to;
        Value = value;
    }

    public GraphNode From { get; }

    public GraphNode To { get; }

    public T Value { get; }
}
