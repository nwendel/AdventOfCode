
namespace AdventOfCode._2019_02;

public class Solver_2019_02 : Solver
{
    protected override Result SolvePart1Core(Input input)
    {
        var program = input.ExtractNumbers();

        program[1] = 12;
        program[2] = 2;

        var result = SolveCore(program);

        return result;
    }

    protected override Result SolvePart2Core(Input input)
    {
        var program = input.ExtractNumbers();

        var values = Enumerable.Range(0, 100).ToArray();
        var combinations = Combinatorics.CartesianProduct(values, 2);

        foreach (var combination in combinations)
        {
            var copy = new long[program.Length];
            Array.Copy(program, copy, program.Length);

            copy[1] = combination[0];
            copy[2] = combination[1];

            var result = SolveCore(copy);
            if (result == 19690720)
            {
                return 100L * combination[0] + combination[1];
            }
        }

        throw new UnreachableException();
    }

    private static long SolveCore(long[] program)
    {
        for (var ix = 0; ix < program.Length; ix += 4)
        {
            var opcode = (OpCode)program[ix];
            if (opcode == OpCode.Halt)
            {
                break;
            }

            var param1 = program[program[ix + 1]];
            var param2 = program[program[ix + 2]];
            var outputPos = program[ix + 3];

            program[outputPos] = opcode switch
            {
                OpCode.Add => param1 + param2,
                OpCode.Multiply => param1 * param2,
                _ => throw new InvalidOperationException($"Unknown opcode: {opcode}")
            };
        }

        return program[0];
    }
}

public enum OpCode
{
    Add = 1,
    Multiply = 2,
    Halt = 99,
}
