namespace AdventOfCode.Common;

public record Position3(long X, long Y, long Z)
{
    public long DistanceTo(Position3 to)
    {
        var dx = X - to.X;
        var dy = Y - to.Y;
        var dz = Z - to.Z;

        return (long)Math.Sqrt(dx * dx + dy * dy + dz * dz);
    }
}
