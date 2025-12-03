namespace AdventOfCode.Common.Framework;

public abstract class Solver : ISolver
{
    public abstract Day Day { get; }

    public object SolvePart1(Input input)
        => SolvePart1Core(input);

    public object SolvePart2(Input input)
        => SolvePart2Core(input);

    protected abstract object SolvePart1Core(Input input);

    protected abstract object SolvePart2Core(Input input);
}

public abstract class Solver<T> : ISolver
{
    public abstract Day Day { get; }

    public object SolvePart1(Input input)
    {
        var transformed = ParseInput(input);
        return SolvePart1Core(transformed);
    }

    public object SolvePart2(Input input)
    {
        var transformed = ParseInput(input);
        return SolvePart2Core(transformed);
    }

    protected abstract T ParseInput(Input input);

    protected abstract object SolvePart1Core(T input);

    protected abstract object SolvePart2Core(T input);
}
