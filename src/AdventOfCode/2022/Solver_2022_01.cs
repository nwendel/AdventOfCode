namespace AdventOfCode._2022;

public class Solver_2022_01 : Solver
{
    protected override object SolvePart1Core(Input input)
    {
        var result = input.Blocks
            .Select(x => x.Lines.ToLongs().Sum())
            .Max();

        var result2 = input.Blocks
            .Select(x => x.Lines.ToLongs().Sum())
            .Max();

        return result;
    }

    protected override object SolvePart2Core(Input input)
    {
        var result = input.Blocks
            .Select(x => x.Lines.ToLongs().Sum())
            .OrderByDescending(x => x)
            .Take(3)
            .Sum();

        return result;
    }
}
