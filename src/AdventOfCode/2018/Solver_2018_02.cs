namespace AdventOfCode._2018;

public class Solver_2018_02 : Solver<string[]>
{
    protected override string[] ParseInput(Input input)
    {
        var parsedInput = input.Lines
            .Select(x => x.Text)
            .ToArray();

        return parsedInput;
    }

    protected override Result SolvePart1Core(string[] input)
    {
        var countWithTwo = 0;
        var countWithThree = 0;

        foreach (var id in input)
        {
            var letterCounts = id.GroupBy(c => c)
                .Select(g => g.Count())
                .ToHashSet();

            if (letterCounts.Contains(2))
            {
                countWithTwo++;
            }

            if (letterCounts.Contains(3))
            {
                countWithThree++;
            }
        }

        var result = countWithTwo * countWithThree;

        return result;
    }

    protected override Result SolvePart2Core(string[] input)
    {
        var combinations = Combinatorics.Combinations(input, 2);

        foreach (var pair in combinations)
        {
            var id1 = pair[0];
            var id2 = pair[1];

            if (id1.Length != id2.Length)
            {
                continue;
            }

            var differences = 0;
            var differenceIndex = -1;

            for (var i = 0; i < id1.Length; i++)
            {
                if (id1[i] != id2[i])
                {
                    differences++;
                    differenceIndex = i;

                    if (differences > 1)
                    {
                        break;
                    }
                }
            }

            if (differences == 1)
            {
                var result = id1.Remove(differenceIndex, 1);
                return result;
            }
        }

        throw new InvalidOperationException("No matching pair found");
    }
}
