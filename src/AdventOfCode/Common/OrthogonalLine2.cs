namespace AdventOfCode.Common;

public record OrthogonalLine2
{
    public OrthogonalLine2(Position2 from, Position2 to)
    {
        From = from;
        To = to;

        if (!IsHorizontal && !IsVertical)
        {
            throw new ArgumentException("Line must be horizontal or vertical");
        }
    }

    public Position2 From { get; }

    public Position2 To { get; }

    public bool IsHorizontal => From.Y == To.Y;

    public bool IsVertical => From.X == To.X;

    public bool Contains(Position2 position)
    {
        if (IsHorizontal)
        {
            if (position.Y != From.Y)
            {
                return false;
            }

            var minX = Math.Min(From.X, To.X);
            var maxX = Math.Max(From.X, To.X);
            return position.X >= minX && position.X <= maxX;
        }
        else if (IsVertical)
        {
            if (position.X != From.X)
            {
                return false;
            }

            var minY = Math.Min(From.Y, To.Y);
            var maxY = Math.Max(From.Y, To.Y);
            return position.Y >= minY && position.Y <= maxY;
        }

        return false;
    }

    public bool Intersects(Rectangle2 rectangle)
    {
        if (IsHorizontal)
        {
            var y = From.Y;
            var minX = Math.Min(From.X, To.X);
            var maxX = Math.Max(From.X, To.X);

            // Check if horizontal line crosses through the rectangle interior (strictly between Y bounds)
            if (y > rectangle.Y1 && y < rectangle.Y2)
            {
                // Check if line overlaps with rectangle X range
                if (maxX > rectangle.X1 && minX < rectangle.X2)
                {
                    return true;
                }
            }
        }
        else if (IsVertical)
        {
            var x = From.X;
            var minY = Math.Min(From.Y, To.Y);
            var maxY = Math.Max(From.Y, To.Y);

            // Check if vertical line crosses through the rectangle interior (strictly between X bounds)
            if (x > rectangle.X1 && x < rectangle.X2)
            {
                // Check if line overlaps with rectangle Y range
                if (maxY > rectangle.Y1 && minY < rectangle.Y2)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
