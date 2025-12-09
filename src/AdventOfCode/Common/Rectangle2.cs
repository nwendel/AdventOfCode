namespace AdventOfCode.Common;

public record Rectangle2
{
    public Rectangle2(long x1, long y1, long x2, long y2)
    {
        X1 = x1;
        Y1 = y1;
        X2 = x2;
        Y2 = y2;
    }

    public Rectangle2(long x, long y)
        : this(0, 0, x, y)
    {
    }

    public Rectangle2(Position2 p1, Position2 p2)
        : this(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Max(p1.X, p2.X), Math.Max(p1.Y, p2.Y))
    {
    }

    public long X1 { get; }

    public long Y1 { get; }

    public long X2 { get; }

    public long Y2 { get; }

    public long Size => Math.Abs((X2 - X1 + 1) * (Y2 - Y1 + 1));

    public IEnumerable<Position2> Corners
    {
        get
        {
            yield return new Position2(X1, Y1);
            yield return new Position2(X2, Y1);
            yield return new Position2(X1, Y2);
            yield return new Position2(X2, Y2);
        }
    }

    public IEnumerable<Position2> Positions
    {
        get
        {
            for (var y = Y1; y <= Y2; y++)
            {
                for (var x = X1; x <= X2; x++)
                {
                    yield return new Position2(x, y);
                }
            }
        }
    }

    public bool Contains(Position2 position)
        => position.X >= X1 && position.X <= X2 && position.Y >= Y1 && position.Y <= Y2;
}
