
namespace AdventOfCode._2015;

public class Solver_2015_01 : Solver
{
    protected override object SolvePart1Core(Input input)
    {
        var text = input.Text;
        var result = text.Count(c => c == '(') - text.Count(c => c == ')');

        return result;
    }

    protected override object SolvePart2Core(Input input)
    {
        var text = input.Text;

        var floor = 0;

        for (var i = 0; i < text.Length; i++)
        {
            floor += text[i] switch
            {
                '(' => 1,
                ')' => -1,
                _ => throw new InvalidOperationException($"Invalid character {text[i]}"),
            };

            if (floor == -1)
            {
                return i + 1;
            }
        }

        throw new UnreachableException();
    }
}
