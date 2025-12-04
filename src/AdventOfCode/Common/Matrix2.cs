namespace AdventOfCode.Common;

public class Matrix2<T>
{
    private readonly T[,] _values;

    public Matrix2(int width, int height)
    {
        _values = new T[width, height];
    }

    public int Width => _values.GetLength(0);

    public int Height => _values.GetLength(1);

    public T this[Position2 position]
    {
        get => _values[position.X, position.Y];
        set => _values[position.X, position.Y] = value;
    }

    public T this[int x, int y]
    {
        get => _values[x, y];
        set => _values[x, y] = value;
    }

    public IEnumerable<Position2> Positions
    {
        get
        {
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    yield return new Position2(x, y);
                }
            }
        }
    }

    public IEnumerable<T> All
        => Positions.Select(p => this[p]);

    public bool Contains(Position2 position)
        => position.X >= 0 && position.X < Width && position.Y >= 0 && position.Y < Height;

    public void Modify(IEnumerable<Position2> positions, T value)
    {
        foreach (var position in positions)
        {
            _values[position.X, position.Y] = value;
        }
    }

    public void Modify(Rectangle2 rectangle, Func<T, T> action)
    {
        for (var x = rectangle.X1; x <= rectangle.X2; x++)
        {
            for (var y = rectangle.Y1; y <= rectangle.Y2; y++)
            {
                _values[x, y] = action(_values[x, y]);
            }
        }
    }
}
