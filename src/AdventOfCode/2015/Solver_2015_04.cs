
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode._2015;

public class Solver_2015_04 : Solver
{
    private static readonly MD5 _algorithm = MD5.Create();

    public override Day Day => new(2015, 4);

    protected override object SolvePart1Core(Input input)
    {
        var result = SolveCore(input, "00000");

        return result;
    }

    protected override object SolvePart2Core(Input input)
    {
        var result = SolveCore(input, "000000");

        return result;
    }

    private static long SolveCore(Input input, string prefix)
    {
        for (var ix = 0; true; ix++)
        {
            var value = $"{input.Text}{ix}";
            var bytes = Encoding.ASCII.GetBytes(value);
            var hash = Convert.ToHexString(_algorithm.ComputeHash(bytes));

            if (hash.StartsWith(prefix))
            {
                return ix;
            }
        }
    }
}
