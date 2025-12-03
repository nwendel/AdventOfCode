
namespace AdventOfCode._2015;

public class Solver_2015_01 : Solver
{
    public override Day Day => new(2015, 1);

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
            if (text[i] == '(')
            {
                floor++;
            }
            else if (text[i] == ')')
            {
                floor--;
            }

            if (floor == -1)
            {
                return i + 1;
            }
        }

        throw new UnreachableException();
    }
}
