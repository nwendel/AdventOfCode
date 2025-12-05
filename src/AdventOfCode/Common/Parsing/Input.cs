using System.Collections;

namespace AdventOfCode.Common.Parsing;

public partial class Input : IEnumerable<InputChar>
{
    public Input(string text)
    {
        Text = text.Trim();
    }

    public string Text { get; }

    public InputChar this[int index] => new(Text[index]);

    public Input this[Range range] => new(Text[range]);

    public Input[] Lines => Text
        .Split(["\r\n", "\n"], StringSplitOptions.None)
        .Select(x => new Input(x))
        .ToArray();

    public Input[] Blocks => Text
        .Split(["\r\n\r\n", "\n\n"], StringSplitOptions.None)
        .Select(x => new Input(x))
        .ToArray();

    public Input Digits()
        => new(new string(Text.Where(x => char.IsDigit(x)).ToArray()));

    public long[] ExtractNumbers()
    {
        var matches = NumberRegex().Matches(Text);
        var result = matches
            .Select(x => long.Parse(x.Value))
            .ToArray();
        return result;
    }

    [GeneratedRegex(@"-?\d+")]
    private static partial Regex NumberRegex();

    public Input Replace(string oldValue, string newValue)
    {
        var modifiedText = Text.Replace(oldValue, newValue);
        return new Input(modifiedText);
    }

    public Input[] Split(string separator) => Text
        .Split(separator)
        .Select(x => new Input(x))
        .ToArray();

    public Input[] Split(params string[] separators) => Text
        .Split(separators, StringSplitOptions.RemoveEmptyEntries)
        .Select(x => new Input(x))
        .ToArray();

    public Direction4[] ToDirection4s() => this
        .Select(x => x.ToDirection4())
        .ToArray();

    public long ToLong() => long.Parse(Text);

    public long[] ToLongs(params string[] separators)
        => Split(separators)
            .Select(x => x.ToLong())
            .ToArray();

    public Matrix2<T> ToMatrix<T>(Func<char, T> parse)
    {
        var lines = Lines;
        var width = lines.Max(x => x.Text.Length);
        var height = lines.Length;

        var matrix = new Matrix2<T>(width, height);

        for (var y = 0; y < height; y++)
        {
            var line = lines[y];
            for (var x = 0; x < line.Text.Length; x++)
            {
                matrix[x, y] = parse(line[x].Text);
            }
        }

        return matrix;
    }

    public IEnumerator<InputChar> GetEnumerator()
        => Text.Select(x => new InputChar(x)).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    public override string ToString()
        => Text;
}
