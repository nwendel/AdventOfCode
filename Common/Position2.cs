

namespace AdventOfCode.Common;

public record Position2(long X, long Y)
{
    public Position2 Offset(long dx, long dy) => new(X + dx, Y + dy);

    public Position2 Offset(long dx, long dy, long multiplier) => new(X + dx * multiplier, Y + dy * multiplier);

    public Position2 North => new(X, Y - 1);

    public Position2 East => new(X + 1, Y);

    public Position2 South => new(X, Y + 1);

    public Position2 West => new(X - 1, Y);

    public Position2 Move(Direction4 direction)
        => direction switch
        {
            Direction4.North => North,
            Direction4.East => East,
            Direction4.South => South,
            Direction4.West => West,
        };

    public Position2 Move(Direction4 direction, long distance)
        => direction switch
        {
            Direction4.North => Offset(0, -distance),
            Direction4.East => Offset(distance, 0),
            Direction4.South => Offset(0, distance),
            Direction4.West => Offset(-distance, 0),
        };

    public override string ToString()
        => $"{X} {Y}";

    public Position2 Clamp(Rectangle2 bounds)
    {
        var x = Math.Clamp(X, bounds.X1, bounds.X2);
        var y = Math.Clamp(Y, bounds.Y1, bounds.Y2);
        return new Position2(x, y);
    }

    public int ToIndex(Rectangle2 bounds)
        => (int)((Y - bounds.Y1) * (bounds.X2 - bounds.X1 + 1) + (X - bounds.X1));
}
