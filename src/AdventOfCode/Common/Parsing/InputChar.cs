namespace AdventOfCode.Common.Parsing;

public class InputChar
{
    public InputChar(char text)
    {
        Text = text;
    }

    public char Text { get; }

    public Turn2 ToTurn2()
        => Text switch
        {
            'L' => Turn2.Left,
            'R' => Turn2.Right,
            _ => throw new InvalidOperationException($"Invalid Turn2 character: {Text}"),
        };

    public Direction4 ToDirection4()
        => Text switch
        {
            '^' => Direction4.North,
            '>' => Direction4.East,
            'v' => Direction4.South,
            '<' => Direction4.West,
            _ => throw new InvalidOperationException($"Invalid Direction4 character: {Text}"),
        };

    public override string ToString()
        => Text.ToString();
}
