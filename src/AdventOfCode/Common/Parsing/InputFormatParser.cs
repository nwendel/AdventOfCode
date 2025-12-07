namespace AdventOfCode.Common.Parsing;

internal class InputFormatParser
{
    public static T ParseLine<T>(string line, string format)
    {
        var tokens = ParseFormat(format);
        var values = ExtractValues(line, tokens);

        var constructors = typeof(T).GetConstructors();
        var constructor = constructors
            .Single(c => c.GetParameters().Length == values.Count);

        if (constructor == null)
        {
            throw new InvalidOperationException($"No constructor found for {typeof(T).Name} with {values.Count} parameters");
        }

        var parameters = constructor.GetParameters();
        var constructorArgs = new object[parameters.Length];

        for (var ix = 0; ix < parameters.Length; ix++)
        {
            var paramName = parameters[ix].Name ?? "";
            var expectedName = values[ix].Name;

            if (!string.Equals(paramName, expectedName, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"Parameter name mismatch at position {ix}: expected '{expectedName}' but constructor parameter is '{paramName}'");
            }

            constructorArgs[ix] = ConvertValue(values[ix].Value, parameters[ix].ParameterType);
        }

        return (T)constructor.Invoke(constructorArgs);
    }

    private static List<FormatToken> ParseFormat(string format)
    {
        var tokens = new List<FormatToken>();
        var ix = 0;

        while (ix < format.Length)
        {
            if (format[ix] == '{')
            {
                var endIndex = format.IndexOf('}', ix);
                if (endIndex == -1)
                {
                    throw new InvalidOperationException($"Unclosed {{ in format string at position {ix}");
                }

                var typeSpec = format[(ix + 1)..endIndex];
                tokens.Add(new FormatToken(FormatTokenKind.Variable, typeSpec));
                ix = endIndex + 1;
            }
            else
            {
                var nextBrace = format.IndexOf('{', ix);
                var literal = nextBrace == -1
                    ? format[ix..]
                    : format[ix..nextBrace];

                if (literal.Length > 0)
                {
                    tokens.Add(new FormatToken(FormatTokenKind.Literal, literal));
                }

                ix = nextBrace == -1 ? format.Length : nextBrace;
            }
        }

        return tokens;
    }

    private static List<ParsedValue> ExtractValues(string line, List<FormatToken> tokens)
    {
        var values = new List<ParsedValue>();
        var position = 0;

        for (var i = 0; i < tokens.Count; i++)
        {
            var token = tokens[i];

            if (token.Kind == FormatTokenKind.Literal)
            {
                var literalIndex = line.IndexOf(token.Value, position, StringComparison.Ordinal);
                if (literalIndex == -1)
                {
                    throw new InvalidOperationException($"Expected literal '{token.Value}' not found in line at position {position}");
                }

                position = literalIndex + token.Value.Length;
            }
            else
            {
                var nextToken = i + 1 < tokens.Count ? tokens[i + 1] : null;

                string value;
                if (nextToken?.Kind == FormatTokenKind.Literal)
                {
                    var endIndex = line.IndexOf(nextToken.Value, position, StringComparison.Ordinal);
                    if (endIndex == -1)
                    {
                        throw new InvalidOperationException($"Expected literal '{nextToken.Value}' not found");
                    }

                    value = line[position..endIndex].Trim();
                    position = endIndex;
                }
                else
                {
                    value = line[position..].Trim();
                    position = line.Length;
                }

                if (string.IsNullOrWhiteSpace(token.Value))
                {
                    throw new InvalidOperationException($"Parameter name cannot be empty in placeholder '{{{token.Value}}}'");
                }

                values.Add(new ParsedValue(token.Value, value));
            }
        }

        return values;
    }

    private static object ConvertValue(string value, Type targetType)
    {
        if (targetType == typeof(string))
        {
            return value;
        }

        if (targetType == typeof(long))
        {
            return long.Parse(value);
        }

        if (targetType == typeof(int))
        {
            return int.Parse(value);
        }

        if (targetType == typeof(char))
        {
            return value.Length > 0 ? value[0] : '\0';
        }

        if (targetType == typeof(LongRange))
        {
            var parts = value.Split('-');
            if (parts.Length != 2)
            {
                throw new InvalidOperationException($"Cannot parse '{value}' as LongRange");
            }

            return new LongRange(long.Parse(parts[0]), long.Parse(parts[1]));
        }

        if (targetType == typeof(GraphNode))
        {
            return new GraphNode(value);
        }

        if (targetType.IsEnum)
        {
            if (Enum.TryParse(targetType, value, ignoreCase: true, out var enumValue))
            {
                return enumValue;
            }

            throw new InvalidOperationException($"Cannot parse '{value}' as enum {targetType.Name}");
        }

        throw new InvalidOperationException($"Unsupported type: {targetType.Name}");
    }

    private enum FormatTokenKind
    {
        Literal,
        Variable
    }

    private record FormatToken(
        FormatTokenKind Kind,
        string Value);

    private record ParsedValue(
        string Name,
        string Value);
}
