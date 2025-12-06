namespace AdventOfCode._2015_10;

public class Solver_2015_10 : Solver<string>
{
    protected override string ParseInput(Input input)
    {
        var parsedInput = input.Text;
        return parsedInput;
    }

    protected override Result SolvePart1Core(string input)
    {
        var result = LookAndSay(input, 40).Length;

        return result;
    }

    protected override Result SolvePart2Core(string input)
    {
        var result = LookAndSay(input, 50).Length;

        return result;
    }

    private static string LookAndSay(string input, int count)
    {
        var result = input;

        for (var step = 0; step < count; step++)
        {
            result = LookAndSayOnce(result);
        }

        return result;
    }

    private static string LookAndSayOnce(string input)
    {
        var builder = new StringBuilder();
        var ix = 0;

        while (ix < input.Length)
        {
            var c = input[ix];
            var count = 1;

            while (ix + count < input.Length && input[ix + count] == c)
            {
                count++;
            }

            builder.Append(count);
            builder.Append(c);

            ix += count;
        }
        return builder.ToString();
    }
}
