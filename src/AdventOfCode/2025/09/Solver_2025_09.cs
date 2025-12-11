namespace AdventOfCode._2025._09;

public class Solver_2025_09 : Solver<Position2[]>
{
    // TODO: ToPosition2s and ToPosition2 extension methods
    protected override Position2[] ParseInput(Input input)
    {
        var parsedInput = input.Lines
            .Select(x => x.ToPosition2())
            .ToArray();

        return parsedInput;
    }

    protected override Result SolvePart1Core(Position2[] input)
    {
        var result = Combinatorics.Combinations(input, 2)
            .Select(x => new Rectangle2(x[0], x[1]).Area)
            .Max();

        return result;
    }

    protected override Result SolvePart2Core(Position2[] input)
    {
        var polygon = new OrthogonalPolygon2(input);

        var rectangle = Combinatorics.Combinations(input, 2)
            .Select(x => new Rectangle2(x[0], x[1]))
            .OrderByDescending(x => x.Area)
            .Where(x => polygon.Contains(x))
            .First();

        return rectangle.Area;
    }
}
