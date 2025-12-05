namespace AdventOfCode._2015;

public class Solver_2015_06 : Solver<Action[]>
{
    protected override Action[] ParseInput(Input input)
    {
        var parsedInput = input.Lines
            .Select(x =>
            {
                var kind = x.Text[..7] switch
                {
                    "turn on" => ActionKind.On,
                    "turn of" => ActionKind.Off,
                    "toggle " => ActionKind.Toggle,
                    _ => throw new InvalidOperationException("Unknown action kind"),
                };
                var numbers = x.ExtractNumbers();

                return new Action(
                    kind,
                    new Rectangle2(numbers[0], numbers[1], numbers[2], numbers[3]));
            })
            .ToArray();

        return parsedInput;
    }

    protected override Result SolvePart1Core(Action[] input)
    {
        var lights = new Matrix2<bool>(1000, 1000);

        foreach (var action in input)
        {
            lights.Modify(
                action.Rectangle,
                x => action.Kind switch
                {
                    ActionKind.On => true,
                    ActionKind.Off => false,
                    ActionKind.Toggle => !x,
                });
        }

        var result = lights.All.Count(x => x);

        return result;
    }

    protected override Result SolvePart2Core(Action[] input)
    {
        var lights = new Matrix2<long>(1000, 1000);

        foreach (var action in input)
        {
            lights.Modify(
                action.Rectangle,
                x => action.Kind switch
                {
                    ActionKind.On => x + 1,
                    ActionKind.Off => x == 0 ? x : x - 1,
                    ActionKind.Toggle => x + 2,
                });
        }

        var result = lights.All.Sum();

        return result;
    }
}

public enum ActionKind
{
    On,
    Off,
    Toggle,
}

public record Action(
    ActionKind Kind,
    Rectangle2 Rectangle);

