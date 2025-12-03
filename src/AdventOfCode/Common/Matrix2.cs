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

    public IEnumerable<T> All
    {
        get
        {
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    yield return _values[x, y];
                }
            }
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
