namespace AdventOfCode.Common;
public static class LongExtensions
{
    public static int CountTrailingZeroBits(this long value)
    {
        if (value == 0)
        {
            return 64;
        }

        int count = 0;
        while ((value & 1) == 0)
        {
            count += 1;
            value >>= 1;
        }

        return count;
    }
}
