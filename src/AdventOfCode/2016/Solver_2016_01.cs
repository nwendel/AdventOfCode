
namespace AdventOfCode._2016;

public class Solver_2016_01 : Solver<Instruction[]>
{
    public static readonly Position2 _start = new(0, 0);

    public override Day Day => new(2016, 1);

    protected override Instruction[] ParseInput(Input input)
    {
        var parsedInput = input
            .Split(", ")
            .Select(x => new Instruction(
                x[0].ToTurn2(),
                x[1..].ToLong()))
            .ToArray();

        return parsedInput;
    }

    protected override object SolvePart1Core(Instruction[] input)
    {
        var position = _start;
        var direction = Direction4.North;

        foreach (var instruction in input)
        {
            direction = direction.Turn(instruction.Turn);
            position = position.Move(direction, instruction.Distance);
        }

        var result = _start.ManhattanDistanceTo(position);

        return result;
    }

    protected override object SolvePart2Core(Instruction[] input)
    {
        var position = _start;
        var direction = Direction4.North;

        var visited = new HashSet<Position2> { position };

        foreach (var instruction in input)
        {
            direction = direction.Turn(instruction.Turn);

            for (var ix = 0; ix < instruction.Distance; ix++)
            {
                position = position.Move(direction);

                if (!visited.Add(position))
                {
                    var result = _start.ManhattanDistanceTo(position);

                    return result;
                }
            }
        }

        throw new UnreachableException();
    }
}

public record Instruction(
    Turn2 Turn,
    long Distance);
