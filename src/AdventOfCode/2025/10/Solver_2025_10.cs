namespace AdventOfCode._2025._10;

// TODO: Some more parsing improvements
public class Solver_2025_10 : Solver<Machine[]>
{
    protected override Machine[] ParseInput(Input input)
    {
        var machines = new List<Machine>();

        foreach (var line in input.Lines)
        {
            var parts = line.Split(" ");

            var lights = parts[0].Text[1..^1];
            var target = new State(lights.Select(c => c == '#').ToArray());

            var buttons = new List<int[]>();
            for (var ix = 1; ix < parts.Length - 1; ix++)
            {
                var buttonIxs = parts[ix].Text[1..^1];
                var lightIndices = buttonIxs.Split(',').Select(int.Parse).ToArray();
                buttons.Add(lightIndices);
            }

            var joltage = parts[^1].Text[1..^1]
                .Split(',')
                .Select(int.Parse)
                .ToArray();

            machines.Add(new Machine(target, buttons.ToArray(), joltage));
        }

        return machines.ToArray();
    }

    protected override Result SolvePart1Core(Machine[] input)
    {
        var result = 0L;

        foreach (var machine in input)
        {
            var minPresses = FindMinimumPressesForState(machine);
            result += minPresses;
        }

        return result;
    }

    private static long FindMinimumPressesForState(Machine machine)
    {
        var startState = new State(new bool[machine.Target.Length]);

        var queue = new Queue<(State state, int presses)>();
        var visited = new HashSet<State>();

        queue.Enqueue((startState, 0));
        visited.Add(startState);

        while (queue.Count > 0)
        {
            var (state, presses) = queue.Dequeue();

            if (state == machine.Target)
            {
                return presses;
            }

            for (var ix = 0; ix < machine.Buttons.Length; ix++)
            {
                var newState = state.Copy();
                foreach (var lightIndex in machine.Buttons[ix])
                {
                    newState[lightIndex] = !newState[lightIndex];
                }

                if (visited.Add(newState))
                {
                    queue.Enqueue((newState, presses + 1));
                }
            }
        }

        throw new UnreachableException();
    }

    protected override Result SolvePart2Core(Machine[] input)
    {
        var result = 0L;

        foreach (var machine in input)
        {
            var minPresses = FindMinimumPressesForJoltage(machine);
            result += minPresses;
        }

        return result;
    }

    private static long FindMinimumPressesForJoltage(Machine machine)
    {
        var equations = new List<Equation>();

        var variables = EquationVariable.Create(machine.Buttons.Length);
        foreach (var variable in variables)
        {
            var equation = new Equation(() => variable >= EquationConstant.Zero);
            equations.Add(equation);
        }

        for (var ix = 0; ix < machine.Joltage.Length; ix++)
        {
            var multipliers = new EquationConstant[machine.Buttons.Length];
            for (var ix2 = 0; ix2 < machine.Buttons.Length; ix2++)
            {
                if (machine.Buttons[ix2].Contains(ix))
                {
                    multipliers[ix2] = new(1);
                }
                else
                {
                    multipliers[ix2] = new(0);
                }
            }

            var joltage = new EquationConstant(machine.Joltage[ix]);

            var equation = new Equation(() => variables * multipliers == joltage);
            equations.Add(equation);
        }

        EquationSolver.Solve(equations, new EquationOptimizer(() => variables.Sum(), EquationOptimizationGoal.Minimize));
        return variables.Sum(v => v.Value);
    }
}

public record State
{
    private readonly bool[] _values;

    public State(bool[] values)
    {
        _values = values;
    }

    public bool this[int index]
    {
        get => _values[index];
        set => _values[index] = value;
    }

    public int Length => _values.Length;

    public State Copy() => new((bool[])_values.Clone());

    public virtual bool Equals(State? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return _values.AsSpan().SequenceEqual(other._values);
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();

        foreach (var value in _values)
        {
            hash.Add(value);
        }

        return hash.ToHashCode();
    }
}

public record Machine(
    State Target,
    int[][] Buttons,
    int[] Joltage);
