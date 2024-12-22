namespace AdventOfCode._2024;

public class Day21 : AdventBase
{
    private static readonly Dictionary<char, Position2> _numericKeypad = new()
    {
        { '7', new(0, 0) }, { '8', new(1, 0) }, { '9', new(2, 0) },
        { '4', new(0, 1) }, { '5', new(1, 1) }, { '6', new(2, 1) },
        { '1', new(0, 2) }, { '2', new(1, 2) }, { '3', new(2, 2) },
                            { '0', new(1, 3) }, { 'A', new(2, 3) }
    };

    private static readonly Dictionary<char, Position2> _directionKeypad = new()
    {
                            { '^', new(1, 0) }, { 'a', new(2, 0) },
        { '<', new(0, 1) }, { 'v', new(1, 1) }, { '>', new(2, 1) }
    };

    private static readonly Dictionary<(char Start, char End), string[]> _moves = new();
    private static readonly Dictionary<(string Code, int MaxDepth, int Depth), long> _memo = new();

    protected override void InternalOnLoad()
    {
        BuildMoveCache(_numericKeypad);
        BuildMoveCache(_directionKeypad);
    }

    private static void BuildMoveCache(Dictionary<char, Position2> keypad)
    {
        foreach (var startEnd in AocCombinatorics.Permutations(keypad.Keys.ToArray(), 2))
        {
            var start = startEnd[0];
            var end = startEnd[1];
            var moves = FindValidMoves(keypad, start, end);
            _moves[(start, end)] = moves;
        }
    }

    private static string[] FindValidMoves(Dictionary<char, Position2> keypad, char from, char to)
    {
        var dx = keypad[to].X - keypad[from].X;
        var dy = keypad[to].Y - keypad[from].Y;

        var moves = "";

        if (Math.Sign(dx) == -1)
        {
            moves += new string('<', (int)Math.Abs(dx));
        }

        if (Math.Sign(dx) == 1)
        {
            moves += new string('>', (int)Math.Abs(dx));
        }

        if (Math.Sign(dy) == -1)
        {
            moves += new string('^', (int)Math.Abs(dy));
        }

        if (Math.Sign(dy) == 1)
        {
            moves += new string('v', (int)Math.Abs(dy));
        }

        var moveCombinations = AocCombinatorics.Permutations(moves.ToCharArray(), moves.Length, false)
            .Select(x => new string(x))
            .Distinct()
            .Where(x =>
            {
                var current = keypad[from];
                foreach (var move in x)
                {
                    current = current.Move(move.ToDirection4());
                    if (!keypad.ContainsValue(current))
                    {
                        return false;
                    }
                }

                return true;
            })
            .Select(x => x + "a")
            .ToArray();

        return moveCombinations;
    }

    protected override object InternalPart1()
    {
        var sum = 0L;

        foreach (var code in Input.Lines)
        {
            var value = int.Parse(code.TrimEnd('A'));
            var length = Shortest(code, 2, 0);

            sum += length * value;
        }

        return sum;
    }

    private static long Shortest(string code, int maxDepth, int depth)
    {
        if (_memo.TryGetValue((code, maxDepth, depth), out var shortest))
        {
            return shortest;
        }

        var current = depth == 0
            ? 'A'
            : 'a';
        var length = 0L;

        foreach (var next in code)
        {
            if (depth == maxDepth)
            {
                length += _moves[(current, next)].First().Length;
            }
            else
            {
                length += _moves[(current, next)].Min(x => Shortest(x, maxDepth, depth + 1));
            }

            current = next;
        }

        _memo[(code, maxDepth, depth)] = length;
        return length;
    }

    protected override object InternalPart2()
    {
        var sum = 0L;

        foreach (var code in Input.Lines)
        {
            var value = int.Parse(code.TrimEnd('A'));
            var length = Shortest(code, 25, 0);

            sum += length * value;
        }

        return sum;
    }
}
