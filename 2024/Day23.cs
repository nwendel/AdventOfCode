namespace AdventOfCode._2024;

public class Day23 : AdventBase
{
    private static readonly Dictionary<string, HashSet<string>> _graph = [];

    protected override object InternalPart1()
    {
        BuildGraph(Input.Lines);
        var groups = Find();
        var filtered = groups.Where(x => x.Any(node => node.StartsWith('t'))).ToList();

        return filtered.Count;
    }

    private static void BuildGraph(string[] connections)
    {
        if (_graph.Count > 0)
        {
            return;
        }

        foreach (var connection in connections)
        {
            var nodes = connection.Split('-');
            var node1 = nodes[0];
            var node2 = nodes[1];

            if (!_graph.ContainsKey(node1))
            {
                _graph[node1] = [];
            }
            if (!_graph.ContainsKey(node2))
            {
                _graph[node2] = [];
            }

            _graph[node1].Add(node2);
            _graph[node2].Add(node1);
        }
    }

    private static List<HashSet<string>> Find()
    {
        var groups = new List<HashSet<string>>();

        foreach (var node in _graph.Keys)
        {
            var neighbors = _graph[node];
            foreach (var neighbor1 in neighbors)
            {
                foreach (var neighbor2 in neighbors)
                {
                    if (neighbor1 != neighbor2 && _graph[neighbor1].Contains(neighbor2))
                    {
                        var group = new HashSet<string> { node, neighbor1, neighbor2 };
                        if (!groups.Any(c => c.SetEquals(group)))
                        {
                            groups.Add(group);
                        }
                    }
                }
            }
        }

        return groups;
    }

    protected override object InternalPart2()
    {
        BuildGraph(Input.Lines);
        var largest = Largest();
        var password = string.Join(',', largest.OrderBy(x => x));

        return password;
    }

    private static HashSet<string> Largest()
    {
        var largest = new HashSet<string>();

        foreach (var node in _graph.Keys)
        {
            var group = new HashSet<string> { node };
            foreach (var neighbor in _graph[node])
            {
                if (group.All(member => _graph[member].Contains(neighbor)))
                {
                    group.Add(neighbor);
                }
            }

            if (group.Count > largest.Count)
            {
                largest = group;
            }
        }

        return largest;
    }
}
