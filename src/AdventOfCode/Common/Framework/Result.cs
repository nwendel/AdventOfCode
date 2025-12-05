using System.Numerics;

namespace AdventOfCode.Common.Framework;

// TODO: Maybe implicit operators to cast from Result?
public class Result
{
    private readonly object _value;

    private Result(object value)
    {
        _value = value;
    }

    public object Value => _value;

    public static implicit operator Result(long value) => new(value);

    public static implicit operator Result(string value) => new(value);

    public static implicit operator Result(BigInteger value) => new(value);

    public override string? ToString()
        => _value.ToString();
}
