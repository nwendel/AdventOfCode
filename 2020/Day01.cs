namespace AdventOfCode._2020;

public class Day01 : AdventBase
{
    protected override object InternalPart1()
    {
        for (var ix = 0; ix < Input.Lines.Length - 1; ix++)
        {
            for (var ix2 = ix + 1; ix2 < Input.Lines.Length; ix2++)
            {
                var first = long.Parse(Input.Lines[ix]);
                var second = long.Parse(Input.Lines[ix2]);
                if (first + second == 2020)
                {
                    return first * second;
                }
            }
        }

        throw new UnreachableException();
    }

    protected override object InternalPart2()
    {
        for (var ix = 0; ix < Input.Lines.Length - 2; ix++)
        {
            for (var ix2 = ix + 1; ix2 < Input.Lines.Length - 1; ix2++)
            {
                for (var ix3 = ix2 + 1; ix3 < Input.Lines.Length; ix3++)
                {
                    var first = long.Parse(Input.Lines[ix]);
                    var second = long.Parse(Input.Lines[ix2]);
                    var third = long.Parse(Input.Lines[ix3]);
                    if (first + second + third == 2020)
                    {
                        return first * second * third;
                    }
                }
            }
        }

        throw new UnreachableException();
    }
}
