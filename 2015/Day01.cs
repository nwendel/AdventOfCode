using System.Diagnostics;

namespace AdventOfCode._2015;

public class Day01 : AdventBase
{
    protected override object InternalPart1()
        => Input.Text.Count(x => x == '(') - Input.Text.Count(x => x == ')');

    protected override object InternalPart2()
    {
        var ix = 1L;
        var floor = 0L;

        foreach (var move in Input.Text)
        {
            floor += move switch
            {
                '(' => 1,
                ')' => -1,
                _ => throw new UnreachableException(),
            };

            if (floor == -1)
            {
                return ix;
            }

            ix++;
        }

        throw new UnreachableException();
    }
}
