namespace AdventOfCode._2025;

internal class Day03 : AdventBase
{
    protected override object InternalPart1()
    {
        var result = 0L;

        foreach (var line in Input.Lines)
        {
            var max = 0;

            var first = line[..^1].Max();
            var index = line.IndexOf(first);
            var second = line[(index + 1)..].Max();

            var value = (first - '0') * 10 + (second - '0');

            result += value;
        }
        return result;
    }

    protected override object InternalPart2()
    {
        var result = 0L;

        foreach (var line in Input.Lines)
        {
            var value = 0L;
            var index = 0;

            for (var position = 1; position <= 12; position++)
            {
                var first = line[index..^(13 - position - 1)].Max();
                index = line[index..].IndexOf(first) + 1 + index;

                value = value * 10 + (first - '0');
            }

            result += value;
        }

        return result;
    }
}
