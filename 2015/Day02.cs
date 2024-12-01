namespace AdventOfCode._2015;

internal class Day02 : AdventBase
{
    protected override object InternalPart1()
        => Input.Lines
            .ExtractNumbers()
            .Select(x => new long[] { x[0] * x[1], x[1] * x[2], x[0] * x[2] })
            .Sum(x => x.Sum() * 2 + x.Min());

    protected override object InternalPart2()
        => Input.Lines
            .ExtractNumbers()
            .Select(x => x.OrderBy(e => e).ToArray())
            .Sum(x => x[0] * 2 + x[1] * 2 + x.Product());
}
