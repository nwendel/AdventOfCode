namespace AdventOfCode.Common;

public class EquationConstant : IEquationElement
{
    public static readonly EquationConstant Zero = new(0);

    public EquationConstant(long value)
    {
        Value = value;
    }

    public long Value { get; }
}
