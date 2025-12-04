namespace AdventOfCode.Common;

public record GraphNode
{
    public GraphNode(string name)
    {
        Name = name;
    }

    public string Name { get; init; }
}
