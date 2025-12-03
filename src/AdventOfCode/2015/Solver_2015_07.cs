

using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode._2015;

public class Solver_2015_07 : Solver<Instruction[]>
{
    public override Day Day => new(2015, 7);

    protected override Instruction[] ParseInput(Input input)
    {
        var parsedInput = input.Lines
            .Select(line =>
            {
                var parts = line.Text.Split(" -> ");
                var result = parts[1];
                var expr = parts[0];
                var tokens = expr.Split(' ');

                var operand1 = tokens.Length != 2 ? tokens[0] : tokens[1];
                OperationKind? operation = tokens.Length switch
                {
                    1 => null,
                    2 => OperationKind.Not,
                    3 => tokens[1] switch
                    {
                        "AND" => OperationKind.And,
                        "OR" => OperationKind.Or,
                        "LSHIFT" => OperationKind.LShift,
                        "RSHIFT" => OperationKind.RShift,
                        _ => throw new InvalidOperationException("Unknown operation"),
                    },
                    _ => throw new InvalidOperationException("Invalid instruction format"),
                };
                var operand2 = tokens.Length == 3 ? tokens[2] : null;

                return new Instruction(
                    Operand1: operand1,
                    Operation: operation,
                    Operand2: operand2,
                    Result: result);
            })
            .ToArray();

        return parsedInput;

    }

    protected override object SolvePart1Core(Instruction[] input)
    {
        var wires = new Dictionary<string, ushort>();
        var remaining = new List<Instruction>(input);

        var result = SolveCore(remaining, wires);

        return result;
    }

    protected override object SolvePart2Core(Instruction[] input)
    {
        var wires = new Dictionary<string, ushort>();
        var remaining = new List<Instruction>(input);

        var b = SolvePart1Core(input);
        wires["b"] = (ushort)b;
        remaining.RemoveAll(x => x.Result == "b");

        var result = SolveCore(remaining, wires);

        return result;
    }

    private static ushort SolveCore(List<Instruction> input, Dictionary<string, ushort> wires)
    {
        ushort result;

        while (!wires.TryGetValue("a", out result))
        {
            ushort? value = null;
            var instruction = input.First(x => TryEvaluate(x, wires, out value));
            wires[instruction.Result] = value!.Value;

            input.Remove(instruction);
        }

        return result;
    }

    private static bool TryEvaluate(
        Instruction instruction,
        Dictionary<string, ushort> wires,
        [NotNullWhen(true)] out ushort? value)
    {
        value = null;

        var value1 = GetValue(instruction.Operand1, wires);
        if (value1 == null)
        {
            return false;
        }

        if (instruction.Operation == null)
        {
            value = value1.Value;
            return true;
        }

        if (instruction.Operation == OperationKind.Not)
        {
            value = (ushort)~value1.Value;
            return true;
        }

        if (instruction.Operand2 == null)
        {
            throw new InvalidOperationException("No operand 2");
        }

        var value2 = GetValue(instruction.Operand2, wires);
        if (value2 == null)
        {
            return false;
        }

        value = instruction.Operation switch
        {
            OperationKind.And => (ushort)(value1.Value & value2.Value),
            OperationKind.Or => (ushort)(value1.Value | value2.Value),
            OperationKind.LShift => (ushort)(value1.Value << value2.Value),
            OperationKind.RShift => (ushort)(value1.Value >> value2.Value),
            _ => throw new InvalidOperationException("Unknown operation"),
        };
        return true;
    }

    private static ushort? GetValue(string operand, Dictionary<string, ushort> wires)
    {
        if (ushort.TryParse(operand, out var value))
        {
            return value;
        }
        if (wires.TryGetValue(operand, out var wireValue))
        {
            return wireValue;
        }
        return null;
    }

}

public enum OperationKind
{
    And,
    Or,
    LShift,
    RShift,
    Not,
}

public record Instruction(
    string Operand1,
    OperationKind? Operation,
    string? Operand2,
    string Result);
