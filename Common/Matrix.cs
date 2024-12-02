namespace AdventOfCode.Common;

public class Matrix<T>
{
    private readonly T[,] _values;

    public Matrix(int width, int height)
    {
        _values = new T[width, height];
    }

    public int Width => _values.GetLength(0);

    public int Height => _values.GetLength(1);

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

    public T this[Position2 position]
    {
        get => _values[position.X, position.Y];
        set => _values[position.X, position.Y] = value;
    }

    public IEnumerable<T> Row(int y)
    {
        for (var x = 0; x < Width; x++)
        {
            yield return _values[x, y];
        }
    }

    public IEnumerable<T> Column(int x)
    {
        for (var y = 0; y < Height; y++)
        {
            yield return _values[x, y];
        }
    }

    public IEnumerable<Position2> Locate(T value)
    {
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                if (EqualityComparer<T>.Default.Equals(_values[x, y], value))
                {
                    yield return new Position2(x, y);
                }
            }
        }
    }

    public bool Contains(Position2 position)
        => position.X >= 0 && position.X < Width && position.Y >= 0 && position.Y < Height;
}
