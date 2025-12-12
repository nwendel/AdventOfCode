namespace AdventOfCode.Common;

public class EquationVariable : IEquationElement
{
    private EquationVariable()
    {
    }

    public bool IsSolved { get; private set; }

    public long Value
    {
        get => IsSolved ? field : throw new InvalidOperationException("Variable has not been solved");
        internal set
        {
            field = value;
            IsSolved = true;
        }
    }

    public static EquationVariable Create()
    {
        return new EquationVariable();
    }

    public static EquationVariable[] Create(int count)
    {
        var variables = new EquationVariable[count];

        for (var ix = 0; ix < count; ix++)
        {
            variables[ix] = new EquationVariable();
        }

        return variables;
    }
}
