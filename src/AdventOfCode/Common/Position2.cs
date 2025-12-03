
namespace AdventOfCode.Common;

public record Position2(long X, long Y)
{
    public static readonly Position2 Zero = new(0, 0);

    public Position2 Offset(long dx, long dy)
        => new(X + dx, Y + dy);

    internal Position2 Move(Direction4 direction)
        => Move(direction, 1);

    public Position2 Move(Direction4 direction, long distance)
        => direction switch
        {
            Direction4.North => Offset(0, -distance),
            Direction4.East => Offset(distance, 0),
            Direction4.South => Offset(0, distance),
            Direction4.West => Offset(-distance, 0),
        };

    public long ManhattanDistanceTo(Position2 to)
        => Math.Abs(X - to.X) + Math.Abs(Y - to.Y);
}
