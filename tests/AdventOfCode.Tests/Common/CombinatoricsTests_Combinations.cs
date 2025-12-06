using AdventOfCode.Common;

namespace AdventOfCode.Tests.Common;

public class CombinatoricsTests_Combinations
{
    [Fact]
    public void Combinations_3Choose3_Returns1Combination()
    {
        int[] values = [1, 2, 3];

        var result = Combinatorics.Combinations(values, 3).ToArray();

        var combination = Assert.Single(result);
        Assert.True(combination.SequenceEqual(values));
    }

    [Fact]
    public void Combinations_3Choose2_Returns3Combinations()
    {
        int[] values = [1, 2, 3];

        var result = Combinatorics.Combinations(values, 2).ToArray();

        Assert.Equal(3, result.Length);
        Assert.Contains(result, x => x.SequenceEqual([1, 2]));
        Assert.Contains(result, x => x.SequenceEqual([1, 3]));
        Assert.Contains(result, x => x.SequenceEqual([2, 3]));
    }
}
