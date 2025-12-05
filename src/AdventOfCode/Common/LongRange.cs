namespace AdventOfCode.Common;

public class LongRange
{
    public LongRange(long start, long end)
    {
        Start = start;
        End = end;
    }
    public long Start { get; }

    public long End { get; }

    public bool Contains(long value)
        => value >= Start && value <= End;

    public bool Overlaps(LongRange other)
        => Start <= other.End && End >= other.Start;
}
