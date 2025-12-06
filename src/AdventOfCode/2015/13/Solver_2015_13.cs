namespace AdventOfCode._2015_13;

[Slow(Part2 = true)]
public class Solver_2015_13 : Solver<ParsedInput>
{
    protected override ParsedInput ParseInput(Input input)
    {
        var lines = input.Lines
            .Split([" ", "."])
            .ToArray();
        var happinessDeltas = lines.ToDictionary(
            k => Key(k.First().Text, k.Last().Text),
            v => v[3].ToLong() * (v[2].Text == "gain" ? 1 : -1));
        var people = lines
            .Select(x => x.First().Text)
            .Distinct()
            .ToArray();
        var result = new ParsedInput(
            People: people,
            Deltas: happinessDeltas);

        return result;
    }

    protected override Result SolvePart1Core(ParsedInput input)
    {
        var result = long.MinValue;

        var permutations = Combinatorics.Permutations(input.People);
        foreach (var permuation in permutations)
        {
            var happiness = 0L;

            for (var ix = 0; ix < permuation.Length; ix++)
            {
                var who = permuation[ix];
                var neighbour = permuation[Math.Wrap(ix + 1, 0..permuation.Length)];

                happiness += input.Deltas[Key(who, neighbour)];
                happiness += input.Deltas[Key(neighbour, who)];
            }

            if (happiness > result)
            {
                result = happiness;
            }
        }

        return result;
    }

    protected override Result SolvePart2Core(ParsedInput input)
    {
        var modifiedInput = input with
        {
            People = input.People.Append("Me").ToArray(),
            Deltas = input.Deltas
                .Concat(input.People.SelectMany(x => new[]
                {
                    new KeyValuePair<string, long>(Key("Me", x), 0L),
                    new KeyValuePair<string, long>(Key(x, "Me"), 0L),
                }))
                .ToDictionary(x => x.Key, x => x.Value),
        };

        var result = SolvePart1Core(modifiedInput);

        return result;
    }

    private static string Key(string who, string neighbor)
        => $"{who}->{neighbor}";
}

public record ParsedInput(
    string[] People,
    Dictionary<string, long> Deltas);
