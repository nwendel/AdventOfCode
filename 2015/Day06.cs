namespace AdventOfCode._2015;

public class Day06 : AdventBase
{
    protected override object InternalPart1()
    {
        var lights = new Matrix<bool>(1000, 1000);

        foreach (var line in Input.Lines)
        {
            var numbers = line.ExtractNumbers();
            var span = new Span2(numbers[0], numbers[1], numbers[2], numbers[3]);
            var action = ParseAction(line);

            foreach (var position in span.Positions)
            {
                switch (action)
                {
                    case ActionKind.TurnOn:
                        lights[position] = true;
                        break;
                    case ActionKind.TurnOff:
                        lights[position] = false;
                        break;
                    case ActionKind.Toggle:
                        lights[position] = !lights[position];
                        break;
                }
            }
        }

        return lights.All.Count(x => x);
    }

    protected override object InternalPart2()
    {
        var lights = new Matrix<long>(1000, 1000);

        foreach (var line in Input.Lines)
        {
            var numbers = line.ExtractNumbers();
            var span = new Span2(numbers[0], numbers[1], numbers[2], numbers[3]);
            var action = ParseAction(line);

            foreach (var position in span.Positions)
            {
                switch (action)
                {
                    case ActionKind.TurnOn:
                        lights[position] += 1;
                        break;
                    case ActionKind.TurnOff:
                        if (lights[position] > 0)
                        {
                            lights[position] -= 1;
                        }
                        break;
                    case ActionKind.Toggle:
                        lights[position] += 2;
                        break;
                }
            }
        }

        return lights.All.Sum();
    }

    private static ActionKind ParseAction(string line)
    {
        if (line.StartsWith("turn off"))
        {
            return ActionKind.TurnOff;
        }

        if (line.StartsWith("turn on"))
        {
            return ActionKind.TurnOn;
        }

        return ActionKind.Toggle;
    }

    private enum ActionKind
    {
        TurnOn,
        TurnOff,
        Toggle
    }
}
