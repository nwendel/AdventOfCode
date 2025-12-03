namespace AdventOfCode.Common.Framework;

public interface ISolver
{
    Day Day { get; }

    object SolvePart1(Input input);

    object SolvePart2(Input input);
}
