namespace AdventOfCode.Common;
public record Position2(long X, long Y)
{
    public Position2 Offset(long x, long y) => new(X + x, Y + y);

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
}
