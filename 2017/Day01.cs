namespace AdventOfCode._2017;

public class Day01 : AdventBase
{
    protected override object InternalPart1()
    {
        var line = Input.Text() + Input.Text()[0];

        var sum = 0;
        for (var ix = 0; ix < line.Length - 1; ix++)
        {
            if (line[ix] == line[ix + 1])
            {
                sum += line[ix] - '0';
            }
        }

        return sum;
    }

    protected override object InternalPart2()
    {
        var offset = Input.Text().Length / 2;
        var line = Input.Text() + Input.Text();

        var sum = 0;
        for (var ix = 0; ix < Input.Text().Length; ix++)
        {
            if (line[ix] == line[ix + offset])
            {
                sum += line[ix] - '0';
            }
        }

        return sum;
    }
}
