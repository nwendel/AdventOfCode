namespace AdventOfCode.Common;

public class CustomNumber
{
    private readonly char[] _digits;
    private readonly Dictionary<char, int> _digitToValue;
    private readonly char[] _value;

    public CustomNumber(string value, string digits)
    {
        var invalid = value.Except(digits);
        if (invalid.Any())
        {
            throw new ArgumentException($"Value contains characters not in digits set {invalid}");
        }

        _digits = digits.ToCharArray();
        _digitToValue = _digits.Select((c, ix) => (c, ix)).ToDictionary(x => x.c, x => x.ix);
        _value = value.ToCharArray();
    }

    public string Value => new(_value);

    public CustomNumber Increment()
    {
        var newValue = (char[])_value.Clone();

        var ix = newValue.Length - 1;

        while (ix >= 0)
        {
            var value = _digitToValue[newValue[ix]];

            if (value + 1 < _digits.Length)
            {
                newValue[ix] = _digits[value + 1];
                for (var i = ix + 1; i < newValue.Length; i++)
                {
                    newValue[i] = _digits[0];
                }

                return new CustomNumber(new string(newValue), new string(_digits));
            }

            ix--;
        }

        throw new UnreachableException("Overflow");
    }
}
