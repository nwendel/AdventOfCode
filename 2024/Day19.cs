namespace AdventOfCode._2024;

public class Day19 : AdventBase
{
    protected override object InternalPart1()
    {
        var towels = Input.Lines[0].Split(", ");
        var designs = Input.Blocks[1].Lines;

        var count = 0;
        foreach (var design in designs)
        {
            bool[] match = new bool[design.Length + 1];
            match[0] = true;

            for (var ix = 1; ix <= design.Length; ix++)
            {
                foreach (var towel in towels)
                {
                    if (ix >= towel.Length && design.Substring(ix - towel.Length, towel.Length) == towel)
                    {
                        match[ix] = match[ix] || match[ix - towel.Length];
                    }
                }
            }

            if (match[design.Length])
            {
                count += 1;
            }
        }

        return count;
    }

    protected override object InternalPart2()
    {
        var towels = Input.Lines[0].Split(", ");
        var designs = Input.Blocks[1].Lines;

        var count = 0L;
        foreach (var design in designs)
        {
            var memo = new Dictionary<int, long>()
            {
                [design.Length] = 1L,
            };
            count += Combinations(design, towels, 0, memo);
        }

        return count;
    }

    private static long Combinations(string design, string[] towels, int start, Dictionary<int, long> memo)
    {
        if (memo.TryGetValue(start, out long value))
        {
            return value;
        }

        var combinations = 0L;
        foreach (var towel in towels)
        {
            if (start + towel.Length <= design.Length && design.Substring(start, towel.Length) == towel)
            {
                combinations += Combinations(design, towels, start + towel.Length, memo);
            }
        }

        memo[start] = combinations;
        return combinations;
    }
}
