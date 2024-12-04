using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode._2016;

public class Day05 : AdventBase
{
    private static readonly MD5 _algorithm = MD5.Create();

    protected override object InternalPart1()
    {
        var code = string.Empty;
        var ix = 0;

        while (code.Length < 8)
        {
            var value = $"{Input.Text()}{ix}";
            var bytes = Encoding.ASCII.GetBytes(value);
            var hash = Convert.ToHexString(_algorithm.ComputeHash(bytes));
            if (hash.StartsWith("00000"))
            {
                code += hash[5];
            }

            ix += 1;
        }

        return code;
    }

    protected override object InternalPart2()
    {
        var code = "--------".ToCharArray();
        var ix = 0;

        while (code.Contains('-'))
        {
            var value = $"{Input.Text()}{ix}";
            var bytes = Encoding.ASCII.GetBytes(value);
            var hash = Convert.ToHexString(_algorithm.ComputeHash(bytes));
            if (hash.StartsWith("00000"))
            {
                var pos = hash[5] - '0';
                if (pos < 8 && code[pos] == '-')
                {
                    code[pos] = hash[6];
                }
            }

            ix += 1;
        }

        return new string(code);
    }
}
