namespace AdventOfCode._2020_02;

public class Solver_2020_02 : Solver<PasswordEntry[]>
{
    protected override PasswordEntry[] ParseInput(Input input)
    {
        var parsedInput = input.Lines
            .Select(x =>
            {
                var parts = x.Text.Split([':', ' ', '-'], StringSplitOptions.RemoveEmptyEntries);
                return new PasswordEntry(
                    Min: int.Parse(parts[0]),
                    Max: int.Parse(parts[1]),
                    Letter: parts[2][0],
                    Password: parts[3]);
            })
            .ToArray();

        return parsedInput;
    }

    protected override Result SolvePart1Core(PasswordEntry[] input)
    {
        var validCount = input.Count(entry =>
        {
            var letterCount = entry.Password.Count(c => c == entry.Letter);
            return letterCount >= entry.Min && letterCount <= entry.Max;
        });

        return validCount;
    }

    protected override Result SolvePart2Core(PasswordEntry[] input)
    {
        var validCount = input.Count(entry =>
        {
            var pos1 = entry.Min - 1;
            var pos2 = entry.Max - 1;

            var hasAtPos1 = pos1 < entry.Password.Length && entry.Password[pos1] == entry.Letter;
            var hasAtPos2 = pos2 < entry.Password.Length && entry.Password[pos2] == entry.Letter;

            return hasAtPos1 ^ hasAtPos2;
        });

        return validCount;
    }
}

public record PasswordEntry(
    int Min,
    int Max,
    char Letter,
    string Password);
