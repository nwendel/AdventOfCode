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

        public long[][] ToLongs(params string[] separators)
            => self
                .Select(x => x.ToLongs(separators))
                .ToArray();
    }
}
