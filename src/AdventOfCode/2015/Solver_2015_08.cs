namespace AdventOfCode._2015_08;

public class Solver_2015_08 : Solver<string[]>
{
    protected override string[] ParseInput(Input input)
    {
        var parsedInput = input.Lines
            .Select(x => x.Text)
            .ToArray();

        return parsedInput;
    }

    protected override Result SolvePart1Core(string[] input)
    {
        var result = 0L;

        foreach (var line in input)
        {
            var memory = line[1..^1]
                .Replace(@"\""", @"""")
                .Replace(@"\\", @"\");
            memory = Regex.Replace(memory, @"\\x[0-9a-fA-F]{2}", "X");

            result += line.Length - memory.Length;
        }

        return result;
    }

    protected override Result SolvePart2Core(string[] input)
    {
        var result = 0L;

        foreach (var line in input)
        {
            var encoded = 0;
            foreach (var c in line)
            {
                encoded += c switch
                {
                    '"' => 2,
                    '\\' => 2,
                    _ => 1,
                };
            }

            result += encoded + 2 - line.Length;
        }

        return result;
    }
}
