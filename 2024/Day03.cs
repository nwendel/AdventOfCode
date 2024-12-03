using System.Text.RegularExpressions;

namespace AdventOfCode._2024;

public class Day03 : AdventBase
{
    protected override object InternalPart1()
    {
        var sum = 0;
        var pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
        var regex = new Regex(pattern);

        var matches = regex.Matches(Input.Text);
        foreach (Match match in matches)
        {
            var x = int.Parse(match.Groups[1].Value);
            var y = int.Parse(match.Groups[2].Value);
            sum += x * y;
        }

        return sum;
    }

    protected override object InternalPart2()
    {
        var sum = 0;
        var pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
        var regex = new Regex(pattern);
        var doPattern = @"do\(\)";
        var dontPattern = @"don't\(\)";
        var doRegex = new Regex(doPattern);
        var dontRegex = new Regex(dontPattern);

        var matches = regex.Matches(Input.Text);
        var doMatches = doRegex.Matches(Input.Text);
        var dontMatches = dontRegex.Matches(Input.Text);

        var enabled = true;

        for (var ix = 0; ix < Input.Text.Length; ix++)
        {
            if (doMatches.Any(x => x.Index == ix))
            {
                enabled = true;
            }

            if (dontMatches.Any(x => x.Index == ix))
            {
                enabled = false;
            }

            if (enabled && matches.Any(x => x.Index == ix))
            {
                var x = int.Parse(matches.First(x => x.Index == ix).Groups[1].Value);
                var y = int.Parse(matches.First(x => x.Index == ix).Groups[2].Value);
                sum += x * y;
            }
        }

        return sum;
    }
}
