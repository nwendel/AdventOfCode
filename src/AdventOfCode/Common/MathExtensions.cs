namespace AdventOfCode.Common;

public static class MathExtensions
{
    extension(Math)
    {
        public static long Wrap(long value, Range range)
        {
            var min = range.Start.Value;
            var max = range.End.Value;
            var rangeSize = max - min + 1;

            var wrappedValue = value - min;
            while (wrappedValue < 0)
            {
                wrappedValue += rangeSize;
            }

            wrappedValue %= rangeSize;

            return wrappedValue + min;
        }
    }
}
