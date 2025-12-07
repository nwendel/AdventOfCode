namespace AdventOfCode.Common.Parsing;

public static class Extensions
{
    extension(IEnumerable<IEnumerable<Input>> self)
    {
        public long[][] ToLongs()
            => self
                .Select(x => x.ToLongs())
                .ToArray();

        public Matrix2<Input> ToMatrix()
        {
            var rows = self.ToArray();
            var height = rows.Length;
            var width = rows.Max(r => r.Count());
            var matrix = new Matrix2<Input>(width, height);

            for (var y = 0; y < height; y++)
            {
                var row = rows[y].ToArray();
                for (var x = 0; x < row.Length; x++)
                {
                    matrix[x, y] = row[x];
                }
            }

            return matrix;
        }

    }

    extension(IEnumerable<Input> self)
    {
        public Direction4[][] ToDirection4s()
            => self
                .Select(x => x.ToDirection4s())
                .ToArray();

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

        public Input[][] Split(string separator)
            => self.Split([separator]);

        public Input[][] Split(string[] separators)
            => self
                .Select(x => x.Split(separators))
                .ToArray();

        public T[] Parse<T>(string format)
            => self
                .Select(x => InputFormatParser.ParseLine<T>(x.Text, format))
                .ToArray();
    }
}
