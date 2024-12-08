namespace AdventOfCode.Common;

public record Rectangle2(long X1, long Y1, long X2, long Y2)
{
    public Rectangle2(long x, long y)
        : this(0, 0, x, y)
    {
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
