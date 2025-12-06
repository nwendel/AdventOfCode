using System.Text.Json;

namespace AdventOfCode._2015_12;

public class Solver_2015_12 : Solver
{
    protected override Result SolvePart1Core(Input input)
    {
        var result = input.ExtractNumbers().Sum();
        return result;
    }

    protected override Result SolvePart2Core(Input input)
    {
        var json = JsonDocument.Parse(input.Text);
        var result = SumJson(json.RootElement);

        return result;
    }

    private static long SumJson(JsonElement element)
    {
        var sum = element.ValueKind switch
        {
            JsonValueKind.Object => element.EnumerateObject().Any(x => x.Value.ValueKind == JsonValueKind.String && x.Value.GetString() == "red") ? 0 : element.EnumerateObject().Sum(p => SumJson(p.Value)),
            JsonValueKind.Array => element.EnumerateArray().Sum(SumJson),
            JsonValueKind.Number => element.GetInt64(),
            JsonValueKind.String => 0,
            _ => throw new UnreachableException($"Kind: {element.ValueKind}"),
        };

        return sum;
    }
}
