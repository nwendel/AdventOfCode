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

    public long X1 { get; }

    public long Y1 { get; }

    public long X2 { get; }

    public long Y2 { get; }

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
