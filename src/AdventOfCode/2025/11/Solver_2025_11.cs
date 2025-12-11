namespace AdventOfCode._2025._11;

// TODO: Use graph?
public class Solver_2025_11 : Solver<Dictionary<string, string[]>>
{
    protected override Dictionary<string, string[]> ParseInput(Input input)
    {
        var parsedInput = new Dictionary<string, string[]>();

        foreach (var line in input.Lines)
        {
            var parts = line.Text.Split(": ");
            var device = parts[0];
            var outputs = parts[1].Split(' ');
            parsedInput[device] = outputs;
        }

        return parsedInput;
    }

    protected override Result SolvePart1Core(Dictionary<string, string[]> input)
    {
        var result = CountPaths(input, "you", "out");

        return result;
    }

    protected override Result SolvePart2Core(Dictionary<string, string[]> input)
    {
        var result =
            CountPaths(input, "svr", "dac") * CountPaths(input, "dac", "fft") * CountPaths(input, "fft", "out") +
            CountPaths(input, "svr", "fft") * CountPaths(input, "fft", "dac") * CountPaths(input, "dac", "out");

        return result;
    }

    private static long CountPaths(
        Dictionary<string, string[]> graph,
        string current,
        string target,
        Memoize<string, long>? memo = null)
    {
        memo ??= new Memoize<string, long>((node, memo) => CountPaths(graph, node, target, memo));

        if (current == target)
        {
            return 1;
        }

        if (!graph.TryGetValue(current, out var neighbors))
        {
            return 0;
        }

        var count = 0L;
        foreach (var next in neighbors)
        {
            count += memo.Get(next);
        }

        return count;
    }
}
