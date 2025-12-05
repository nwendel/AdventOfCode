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
            '^' or 'U' => Direction4.North,
            '>' or 'R' => Direction4.East,
            'v' or 'D' => Direction4.South,
            '<' or 'L' => Direction4.West,
            _ => throw new InvalidOperationException($"Invalid Direction4 character: {Text}"),
        };

    public override string ToString()
        => Text.ToString();
}
