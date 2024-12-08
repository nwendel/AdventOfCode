namespace AdventOfCode._2022;

public class Day01 : AdventBase
{
    protected override object InternalPart1()
    {
        var max = Input.Blocks
            .Select(x => x.Lines
                .Select(int.Parse)
                .Sum())
            .Max();

        return max;
    }

    protected override object InternalPart2()
    {
        var sum3 = Input.Blocks
            .Select(x => x.Lines
                .Select(int.Parse)
                .Sum())
            .OrderByDescending(x => x)
            .Take(3)
            .Sum();
        return sum3;
    }
}
