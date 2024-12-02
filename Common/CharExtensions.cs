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
}
