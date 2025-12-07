namespace AdventOfCode._2015._05;

public class Solver_2015_05 : Solver<string[]>
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
        string[] forbidden = ["ab", "cd", "pq", "xy"];

        var result = input
            .Where(text => text.Count(c => "aeiou".Contains(c)) >= 3)
            .Where(text => text.SlidingChunk(2).Any(chunk => chunk[0] == chunk[1]))
            .Where(text => !forbidden.Any(text.Contains))
            .Count();

        return result;
    }

    protected override Result SolvePart2Core(string[] input)
    {
        var result = input
            .Where(text => CountPairs(text) > 0)
            .Where(text => text.SlidingChunk(3).Any(chunk => chunk[0] == chunk[2]))
            .Count();

        return result;
    }

    // TODO: Can this be linq:ed someway?
    private static int CountPairs(string text)
    {
        var count = 0;

        for (var ix = 0; ix < text.Length - 1; ix++)
        {
            var pair = text.Substring(ix, 2);
            if (text.IndexOf(pair, ix + 2) >= 0)
            {
                count++;
            }
        }

        return count;
    }
}
