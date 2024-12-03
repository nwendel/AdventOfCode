namespace AdventOfCode._2016;

public class Day02 : AdventBase
{
    protected override object InternalPart1()
    {
        var code = 0L;
        var button = new Position2(1, 1);
        var bounds = new Span2(2, 2);

        foreach (var line in Input.Lines)
        {
            foreach (var move in line)
            {
                button = button.Move(move.ToDirection4()).Clamp(bounds);
            }

            code *= 10;
            code += button.ToIndex(bounds) + 1;
        }

        return code;
    }

    protected override object InternalPart2()
    {
        var keypad = "  1   234 56789 ABC   D  ";
        var code = "";
        var button = new Position2(0, 2);
        var bounds = new Span2(4, 4);

        foreach (var line in Input.Lines)
        {
            foreach (var move in line)
            {
                var check = button.Move(move.ToDirection4()).Clamp(bounds);
                if (keypad[check.ToIndex(bounds)] != ' ')
                {
                    button = check;
                }
            }

            code += keypad[button.ToIndex(bounds)];
        }

        return code;
    }
}
