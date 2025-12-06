using AdventOfCode.Common;
using Xunit;

namespace AdventOfCode.Tests.Common;

public class CombinatoricsTests_Permutations
{
    [Fact]
    public void Permutations_3Choose3_Returns6Permutations()
    {
        int[] values = [1, 2, 3];
        var result = Combinatorics.Permutations(values, 3).ToArray();

        Assert.Equal(6, result.Length);
        Assert.Contains(result, x => x.SequenceEqual([1, 2, 3]));
        Assert.Contains(result, x => x.SequenceEqual([1, 3, 2]));
        Assert.Contains(result, x => x.SequenceEqual([2, 1, 3]));
        Assert.Contains(result, x => x.SequenceEqual([2, 3, 1]));
        Assert.Contains(result, x => x.SequenceEqual([3, 1, 2]));
        Assert.Contains(result, x => x.SequenceEqual([3, 2, 1]));
    }

    [Fact]
    public void Permutations_3Choose2_Returns6Permutations()
    {
        int[] values = [1, 2, 3];
        var result = Combinatorics.Permutations(values, 2).ToArray();

        Assert.Equal(6, result.Length);
        Assert.Contains(result, x => x.SequenceEqual([1, 2]));
        Assert.Contains(result, x => x.SequenceEqual([1, 3]));
        Assert.Contains(result, x => x.SequenceEqual([2, 1]));
        Assert.Contains(result, x => x.SequenceEqual([2, 3]));
        Assert.Contains(result, x => x.SequenceEqual([3, 1]));
        Assert.Contains(result, x => x.SequenceEqual([3, 2]));
    }
}
