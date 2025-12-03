namespace AdventOfCode._2017;

public class Solver_2017_01 : Solver<List<long>>
{
    protected override List<long> ParseInput(Input input)
    {
        var parsedInput = input.Text
            .Select(c => (long)(c - '0'))
            .ToList();

        return parsedInput;
    }

    protected override object SolvePart1Core(List<long> input)
    {
        input.Add(input[0]);

        var result = input
            .SlidingWindow(2)
            .Where(x => x[0] == x[1])
            .Sum(x => x[0]);

        return result;
    }

    protected override object SolvePart2Core(List<long> input)
    {
        var result = 0L;

        var count = input.Count;
        var offset = count / 2;

        for (var ix = 0; ix < count; ix++)
        {
            var matchIx = (ix + offset) % count;
            if (input[ix] == input[matchIx])
            {
                result += input[ix];
            }
        }

        return result;
    }
}
