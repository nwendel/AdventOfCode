namespace AdventOfCode._2015;

public class Solver_2015_11 : Solver<CustomNumber>
{
    protected override CustomNumber ParseInput(Input input)
    {
        var parsedInput = new CustomNumber(input.Text, "abcdefghjkmnpqrstuvwxyz");

        return parsedInput;
    }

    protected override object SolvePart1Core(CustomNumber input)
    {
        var result = NextValid(input);

        return result.Value;
    }

    protected override object SolvePart2Core(CustomNumber input)
    {
        var result = NextValid(NextValid(input));

        return result.Value;
    }

    private static CustomNumber NextValid(CustomNumber input)
    {
        while (true)
        {
            input = input.Increment();

            if (IsValid(input.Value))
            {
                return input;
            }
        }
    }

    private static bool IsValid(string password)
    {
        var hasStraight = password
            .SlidingChunk(3)
            .Any(chunk => chunk[1] == chunk[0] + 1 && chunk[2] == chunk[0] + 2);
        if (!hasStraight)
        {
            return false;
        }

        var count = 0;
        char? last = null;

        for (var ix = 0; ix < password.Length - 1; ix++)
        {
            if (password[ix] == password[ix + 1] && password[ix] != last)
            {
                count++;
                last = password[ix];
                ix += 1;
            }
        }

        return count >= 2;
    }
}
