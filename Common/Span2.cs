namespace AdventOfCode.Common;

public class Span2(long X1, long Y1, long X2, long Y2)
{
    public IEnumerable<Position2> Positions
    {
        get
        {
            for (var x = X1; x <= X2; x++)
            {
                for (var y = Y1; y <= Y2; y++)
                {
                    yield return new Position2(x, y);
                }
            }
        }
    }
}
