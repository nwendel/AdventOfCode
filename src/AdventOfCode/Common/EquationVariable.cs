namespace AdventOfCode.Common;

public class EquationVariable : IEquationElement
{
    private EquationVariable()
    {
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
