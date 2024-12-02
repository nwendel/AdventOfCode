namespace AdventOfCode._2015;

public class Day05 : AdventBase
{
    protected override object InternalPart1()
    {
        var nice = 0;

        foreach (var line in Input.Lines)
        {
            if (line.CountAny("aeiou") > 2 &&
                HasRepeat(line) &&
                !line.ContainsAny("ab", "cd", "pq", "xy"))
            {
                nice += 1;
            }
        }

        return nice;
    }

    private bool HasRepeat(string value)
    {
        for (var ix = 0; ix < value.Length - 1; ix++)
        {
            if (value[ix] == value[ix + 1])
            {
                return true;
            }
        }

        return false;
    }

    protected override object InternalPart2()
    {
        var nice = 0;

        foreach (var line in Input.Lines)
        {
            if (HasRepeat2(line) &&
                HasRepeat3(line))
            {
                nice += 1;
            }
        }

        return nice;
    }

    private bool HasRepeat2(string value)
    {
        for (var ix = 0; ix < value.Length - 2; ix++)
        {
            var check = value[ix..(ix + 2)];
            var remaining = value[(ix + 2)..];

            if (remaining.Contains(check))
            {
                return true;
            }
        }

        return false;
    }

    private bool HasRepeat3(string value)
    {
        for (var ix = 0; ix < value.Length - 2; ix++)
        {
            if (value[ix] == value[ix + 2])
            {
                return true;
            }
        }

        return false;
    }
}
