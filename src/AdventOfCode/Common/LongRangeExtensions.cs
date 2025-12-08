namespace AdventOfCode.Common;

public static class LongRangeExtensions
{
    extension(IEnumerable<LongRange> ranges)
    {
        public IEnumerable<LongRange> Merge()
        {
            var sorted = ranges
                .OrderBy(r => r.Start)
                .ToList();
            var merged = new List<LongRange>();

            foreach (var range in sorted)
            {
                if (merged.Count == 0)
                {
                    merged.Add(new LongRange(range.Start, range.End));
                }
                else
                {
                    var last = merged[^1];

                    if (range.Start <= last.End + 1)
                    {
                        merged[^1] = new LongRange(last.Start, Math.Max(last.End, range.End));
                    }
                    else
                    {
                        merged.Add(new LongRange(range.Start, range.End));
                    }
                }
            }

            return merged;
        }
    }
}
