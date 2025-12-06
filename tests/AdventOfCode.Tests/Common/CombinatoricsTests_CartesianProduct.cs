using AdventOfCode.Common;

namespace AdventOfCode.Tests.Common;

public class CombinatoricsTests_CartesianProduct
{
    [Fact]
    public void CartesianProduct_3Choose3_Returns27Combinations()
    {
        int[] values = [1, 2, 3];
        var count = 3;

        var result = Combinatorics.CartesianProduct(values, count).ToArray();

        Assert.Equal(27, result.Length);
        Assert.Contains(result, x => x.SequenceEqual([1, 1, 1]));
        Assert.Contains(result, x => x.SequenceEqual([1, 1, 2]));
        Assert.Contains(result, x => x.SequenceEqual([1, 1, 3]));
        Assert.Contains(result, x => x.SequenceEqual([1, 2, 1]));
        Assert.Contains(result, x => x.SequenceEqual([1, 2, 2]));
        Assert.Contains(result, x => x.SequenceEqual([1, 2, 3]));
        Assert.Contains(result, x => x.SequenceEqual([1, 3, 1]));
        Assert.Contains(result, x => x.SequenceEqual([1, 3, 2]));
        Assert.Contains(result, x => x.SequenceEqual([1, 3, 3]));
        Assert.Contains(result, x => x.SequenceEqual([2, 1, 1]));
        Assert.Contains(result, x => x.SequenceEqual([2, 1, 2]));
        Assert.Contains(result, x => x.SequenceEqual([2, 1, 3]));
        Assert.Contains(result, x => x.SequenceEqual([2, 2, 1]));
        Assert.Contains(result, x => x.SequenceEqual([2, 2, 2]));
        Assert.Contains(result, x => x.SequenceEqual([2, 2, 3]));
        Assert.Contains(result, x => x.SequenceEqual([2, 3, 1]));
        Assert.Contains(result, x => x.SequenceEqual([2, 3, 2]));
        Assert.Contains(result, x => x.SequenceEqual([2, 3, 3]));
        Assert.Contains(result, x => x.SequenceEqual([3, 1, 1]));
        Assert.Contains(result, x => x.SequenceEqual([3, 1, 2]));
        Assert.Contains(result, x => x.SequenceEqual([3, 1, 3]));
        Assert.Contains(result, x => x.SequenceEqual([3, 2, 1]));
        Assert.Contains(result, x => x.SequenceEqual([3, 2, 2]));
        Assert.Contains(result, x => x.SequenceEqual([3, 2, 3]));
        Assert.Contains(result, x => x.SequenceEqual([3, 3, 1]));
        Assert.Contains(result, x => x.SequenceEqual([3, 3, 2]));
        Assert.Contains(result, x => x.SequenceEqual([3, 3, 3]));
    }

    [Fact]
    public void CartesianProduct_3Choose2_Returns9Combinations()
    {
        int[] values = [1, 2, 3];
        var count = 2;

        var result = Combinatorics.CartesianProduct(values, count).ToArray();

        Assert.Equal(9, result.Length);
        Assert.Contains(result, x => x.SequenceEqual([1, 1]));
        Assert.Contains(result, x => x.SequenceEqual([1, 2]));
        Assert.Contains(result, x => x.SequenceEqual([1, 3]));
        Assert.Contains(result, x => x.SequenceEqual([2, 1]));
        Assert.Contains(result, x => x.SequenceEqual([2, 2]));
        Assert.Contains(result, x => x.SequenceEqual([2, 3]));
        Assert.Contains(result, x => x.SequenceEqual([3, 1]));
        Assert.Contains(result, x => x.SequenceEqual([3, 2]));
        Assert.Contains(result, x => x.SequenceEqual([3, 3]));
    }
}
