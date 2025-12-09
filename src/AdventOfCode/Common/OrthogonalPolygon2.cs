namespace AdventOfCode.Common;

public class OrthogonalPolygon2
{
    private readonly Position2[] _vertices;

    public OrthogonalPolygon2(IEnumerable<Position2> vertices)
    {
        _vertices = vertices.ToArray();
    }

    public bool Contains(Position2 position)
    {
        // Check if point is a corner
        if (_vertices.Any(v => v.X == position.X && v.Y == position.Y))
        {
            return true;
        }

        // Check if point is on an edge
        for (var ix = 0; ix < _vertices.Length; ix++)
        {
            var p1 = _vertices[ix];
            var p2 = _vertices[(ix + 1) % _vertices.Length];

            var line = new OrthogonalLine2(p1, p2);
            if (line.Contains(position))
            {
                return true;
            }
        }

        // Ray casting algorithm for interior points
        // https://en.wikipedia.org/wiki/Point_in_polygon#Ray_casting_algorithm
        var isInside = false;
        for (var ix = 0; ix < _vertices.Length; ix++)
        {
            var p1 = _vertices[ix];
            var p2 = _vertices[(ix + 1) % _vertices.Length];

            var line = new OrthogonalLine2(p1, p2);
            if (line.IsHorizontal)
            {
                continue;
            }

            var minY = Math.Min(p1.Y, p2.Y);
            var maxY = Math.Max(p1.Y, p2.Y);
            if (minY < position.Y && position.Y <= maxY && position.X < p1.X)
            {
                isInside = !isInside;
            }
        }

        return isInside;
    }

    public bool Contains(Rectangle2 rectangle)
    {
        // If any of the corners are outside, the rectangle is also outside
        if (rectangle.Corners.Any(x => !Contains(x)))
        {
            return false;
        }

        // Verify no polygon edge crosses through the rectangle interior
        for (var ix = 0; ix < _vertices.Length; ix++)
        {
            var p1 = _vertices[ix];
            var p2 = _vertices[(ix + 1) % _vertices.Length];

            var line = new OrthogonalLine2(p1, p2);
            if (line.Intersects(rectangle))
            {
                return false;
            }
        }

        return true;
    }
}
