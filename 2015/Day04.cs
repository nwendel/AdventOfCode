using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode._2015;

public class Day04 : AdventBase
{
    private static readonly MD5 _algorithm = MD5.Create();

    protected override object InternalPart1()
    {
        for (var ix = 0L; true; ix++)
        {
            if (IsValid(ix, "00000"))
            {
                return ix;
            }
        }
    }

    protected override object InternalPart2()
    {
        for (var ix = 0L; true; ix++)
        {
            if (IsValid(ix, "000000"))
            {
                return ix;
            }
        }
    }

    private bool IsValid(long ix, string startsWith)
    {
        var value = $"{Input.Text()}{ix}";
        var bytes = Encoding.ASCII.GetBytes(value);
        var hash = Convert.ToHexString(_algorithm.ComputeHash(bytes));

        return hash.StartsWith(startsWith);
    }
}
