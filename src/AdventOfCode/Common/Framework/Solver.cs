namespace AdventOfCode.Common.Framework;

public abstract class Solver : ISolver
{
    public Result SolvePart1(Input input)
        => SolvePart1Core(input);

    public Result SolvePart2(Input input)
        => SolvePart2Core(input);

    protected abstract Result SolvePart1Core(Input input);

    protected abstract Result SolvePart2Core(Input input);
}

public abstract class Solver<T> : ISolver
{
    public Result SolvePart1(Input input)
    {
        var transformed = ParseInput(input);
        return SolvePart1Core(transformed);
    }

    public Result SolvePart2(Input input)
    {
        var transformed = ParseInput(input);
        return SolvePart2Core(transformed);
    }

    protected abstract T ParseInput(Input input);

    protected abstract Result SolvePart1Core(T input);

    protected abstract Result SolvePart2Core(T input);
}
