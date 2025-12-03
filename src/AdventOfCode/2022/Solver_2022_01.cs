namespace AdventOfCode._2022;

public class Solver_2022_01 : Solver
{
    public override Day Day => new(2022, 1);

    protected override object SolvePart1Core(Input input)
    {
        var result = input.Blocks
            .Select(x => x.Lines
                .Select(x => x.ToLong())
                .Sum())
            .Max();

        return result;
    }

    protected override object SolvePart2Core(Input input)
    {
        var result = input.Blocks
            .Select(x => x.Lines
                .Select(x => x.ToLong())
                .Sum())
            .OrderByDescending(x => x)
            .Take(3)
            .Sum();

        return result;
    }
}
