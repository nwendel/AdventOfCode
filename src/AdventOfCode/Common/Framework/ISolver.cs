namespace AdventOfCode.Common.Framework;

public interface ISolver
{
    public Day Day
    {
        get
        {
            var type = GetType().Name;
            var year = int.Parse(type.Substring(7, 4));
            var number = int.Parse(type.Substring(12, 2));

            return new Day(year, number);
        }
    }

    object SolvePart1(Input input);

    object SolvePart2(Input input);
}
