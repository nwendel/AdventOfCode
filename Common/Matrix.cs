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

    public IEnumerable<T[]> Rows
    {
        get
        {
            for (var y = 0; y < Height; y++)
            {
                var row = new T[Width];
                for (var x = 0; x < Width; x++)
                {
                    row[x] = _values[x, y];
                }
                yield return row;
            }
        }
    }

    public IEnumerable<T[]> Columns
    {
        get
        {
            for (var x = 0; x < Width; x++)
            {
                var column = new T[Height];
                for (var y = 0; y < Height; y++)
                {
                    column[y] = _values[x, y];
                }
                yield return column;
            }
        }
    }

    public IEnumerable<T[]> Diagonals
    {
        get
        {
            for (var k = 0; k < Width + Height - 1; k++)
            {
                var diagonal = new List<T>();
                for (var y = 0; y <= k; y++)
                {
                    var x = k - y;
                    if (x < Width && y < Height)
                    {
                        diagonal.Add(_values[x, y]);
                    }
                }
                if (diagonal.Count > 0)
                {
                    yield return diagonal.ToArray();
                }
            }

            for (var k = 1 - Width; k < Height; k++)
            {
                var diagonal = new List<T>();
                for (var y = 0; y < Height; y++)
                {
                    var x = y - k;
                    if (x >= 0 && x < Width)
                    {
                        diagonal.Add(_values[x, y]);
                    }
                }
                if (diagonal.Count > 0)
                {
                    yield return diagonal.ToArray();
                }
            }
        }
    }

    public IEnumerable<Position2> Locate(Predicate<T> predicate)
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (predicate(_values[x, y]))
                {
                    yield return new Position2(x, y);
                }
            }
        }
    }

    public IEnumerable<Position2> Locate(T value)
        => Locate(x => EqualityComparer<T>.Default.Equals(x, value));

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

    public bool Contains(Position2 position)
        => position.X >= 0 && position.X < Width && position.Y >= 0 && position.Y < Height;
}
