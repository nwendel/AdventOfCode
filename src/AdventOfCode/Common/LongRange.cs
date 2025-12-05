using System.Collections;

namespace AdventOfCode.Common;

public class LongRange : IEnumerable<long>
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

    public IEnumerator<long> GetEnumerator()
    {
        for (var ix = Start; ix <= End; ix++)
        {
            yield return ix;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
