namespace AdventOfCode._2021;

public class Day01 : AdventBase
{
    protected override object InternalPart1()
    {
        var increases = Input.Lines
            .Select(int.Parse)
            .SlidingWindow(2)
            .Where(x => x[1] > x[0])
            .Count();
        return increases;
    }

    protected override object InternalPart2()
    {
        var increases = Input.Lines
            .Select(int.Parse)
            .SlidingWindow(3)
            .Select(x => x.Sum())
            .SlidingWindow(2)
            .Where(x => x[1] > x[0])
            .Count();
        return increases;
    }
}
