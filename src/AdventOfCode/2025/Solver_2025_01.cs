namespace AdventOfCode2._2025;

// TODO: Common parsing
public class Solver_2025_01 : Solver<Instruction[]>
{
    protected override Instruction[] ParseInput(Input input)
    {
        var parsedInput = input.Lines
            .Select(line =>
            {
                var turn = line[0].ToTurn2();
                var count = line[1..].ToLong();
                return new Instruction(turn, count);
            })
            .ToArray();

        return parsedInput;
    }

    protected override Result SolvePart1Core(Instruction[] input)
    {
        var result = 0L;
        var current = 50L;

        foreach (var instruction in input)
        {
            current = Math.Wrap(current + instruction.Count * (long)instruction.Turn, 0..100);
            if (current == 0)
            {
                result += 1;
            }
        }

        return result;
    }

    protected override Result SolvePart2Core(Instruction[] input)
    {
        var result = 0L;
        var current = 50L;

        foreach (var instruction in input)
        {
            for (var ix = 0; ix < instruction.Count; ix++)
            {
                current = Math.Wrap(current + (long)instruction.Turn, 0..100);
                if (current == 0)
                {
                    result += 1;
                }
            }
        }

        return result;
    }
}

public record Instruction(
    Turn2 Turn,
    long Count);
