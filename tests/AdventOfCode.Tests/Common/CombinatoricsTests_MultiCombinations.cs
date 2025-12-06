using AdventOfCode.Common;

namespace AdventOfCode.Tests.Common;

public class CombinatoricsTests_Multicombinations
{
    [Fact]
    public void Multicombinations_3Choose3_Returns10Combinations()
    {
        int[] values = [1, 2, 3];

        var result = Combinatorics.MultiCombinations(values).ToArray();

        Assert.Equal(10, result.Length);
        Assert.Contains(result, x => x.SequenceEqual([1, 1, 1]));
        Assert.Contains(result, x => x.SequenceEqual([1, 1, 2]));
        Assert.Contains(result, x => x.SequenceEqual([1, 1, 3]));
        Assert.Contains(result, x => x.SequenceEqual([1, 2, 2]));
        Assert.Contains(result, x => x.SequenceEqual([1, 2, 3]));
        Assert.Contains(result, x => x.SequenceEqual([1, 3, 3]));
        Assert.Contains(result, x => x.SequenceEqual([2, 2, 2]));
        Assert.Contains(result, x => x.SequenceEqual([2, 2, 3]));
        Assert.Contains(result, x => x.SequenceEqual([2, 3, 3]));
        Assert.Contains(result, x => x.SequenceEqual([3, 3, 3]));
    }

    [Fact]
    public void Multicombinations_3Choose2_Returns6Combinations()
    {
        int[] values = [1, 2, 3];
        var result = Combinatorics.MultiCombinations(values, 2).ToArray();

        Assert.Equal(6, result.Length);
        Assert.Contains(result, x => x.SequenceEqual([1, 1]));
        Assert.Contains(result, x => x.SequenceEqual([1, 2]));
        Assert.Contains(result, x => x.SequenceEqual([1, 3]));
        Assert.Contains(result, x => x.SequenceEqual([2, 2]));
        Assert.Contains(result, x => x.SequenceEqual([2, 3]));
        Assert.Contains(result, x => x.SequenceEqual([3, 3]));
    }
}
