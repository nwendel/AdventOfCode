namespace AdventOfCode.Common;

public static class CharExtensions
{
    public static Turn ToTurn(this char self)
        => self switch
        {
            'L' => Turn.Left,
            'R' => Turn.Right,
            _ => throw new ArgumentOutOfRangeException(nameof(self))
        };

    public static Direction4 ToDirection4(this char self)
        => self switch
        {
            'N' or '^' or 'U' => Direction4.North,
            'E' or '>' or 'R' => Direction4.East,
            'S' or 'v' or 'D' => Direction4.South,
            'W' or '<' or 'L' => Direction4.West,
            _ => throw new ArgumentOutOfRangeException(nameof(self))
        };
}
