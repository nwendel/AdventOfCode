namespace AdventOfCode.Common.Parsing;

public static class Extensions
{
    extension(IEnumerable<IEnumerable<Input>> self)
    {
        public long[][] ToLongs()
            => self
                .Select(x => x.ToLongs())
                .ToArray();
    }

    extension(IEnumerable<Input> self)
    {
        public long[] ToLongs()
            => self
                .Select(x => x.ToLong())
                .ToArray();

        public long[][] ToLongs(string separator)
            => self.ToLongs([separator]);

        public long[][] ToLongs(string[] separators)
            => self
                .Select(x => x.ToLongs(separators))
                .ToArray();

        public LongRange[] ToRanges()
            => self
                .Select(x =>
                {
                    var parts = x.ToLongs("-");
                    return new LongRange(parts[0], parts[1]);
                })
                .ToArray();
    }
}
