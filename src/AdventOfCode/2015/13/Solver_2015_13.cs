namespace AdventOfCode._2015_13;

// TODO: Maybe use Graph here?
[Slow(Part2 = true)]
public class Solver_2015_13 : Solver<ParsedInput>
{
    protected override ParsedInput ParseInput(Input input)
    {
        var lines = input.Lines
            .Parse<HappinessRule>("{Person} would {Direction} {Amount} happiness units by sitting next to {Neighbor}.");

        var happinessDeltas = lines.ToDictionary(
            k => Key(k.Person, k.Neighbor),
            v => v.Amount * (long)v.Direction);

        var people = lines
            .Select(x => x.Person)
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

public enum Direction
{
    Lose = -1,
    Gain = 1
}

public record HappinessRule(
    string Person,
    Direction Direction,
    long Amount,
    string Neighbor);

public record ParsedInput(
    string[] People,
    Dictionary<string, long> Deltas);
