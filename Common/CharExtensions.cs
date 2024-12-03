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
            'N' => Direction4.North,
            'E' => Direction4.East,
            'S' => Direction4.South,
            'W' => Direction4.West,
            '^' => Direction4.North,
            '>' => Direction4.East,
            'v' => Direction4.South,
            '<' => Direction4.West,
            'U' => Direction4.North,
            'R' => Direction4.East,
            'D' => Direction4.South,
            'L' => Direction4.West,
            _ => throw new ArgumentOutOfRangeException(nameof(self))
        };
}
