using AdventOfCode.Common;

namespace AdventOfCode.Tests.Common;

public class CombinatoricsTests_Partitions
{
    [Fact]
    public void Partitions_4_Returns5Partitions()
    {
        var result = Combinatorics.Partitions(4).ToArray();

        Assert.Equal(5, result.Length);
        Assert.Contains(result, x => x.SequenceEqual([4]));
        Assert.Contains(result, x => x.SequenceEqual([3, 1]));
        Assert.Contains(result, x => x.SequenceEqual([2, 2]));
        Assert.Contains(result, x => x.SequenceEqual([2, 1, 1]));
        Assert.Contains(result, x => x.SequenceEqual([1, 1, 1, 1]));
    }

    [Fact]
    public void Partitions_5Into2Parts_Returns2Partitions()
    {
        var result = Combinatorics.Partitions(5, 2).ToArray();

        Assert.Equal(2, result.Length);
        Assert.Contains(result, x => x.SequenceEqual([4, 1]));
        Assert.Contains(result, x => x.SequenceEqual([3, 2]));
    }

    [Fact]
    public void Partitions_6Into3Parts_Returns3Partitions()
    {
        var result = Combinatorics.Partitions(6, 3).ToArray();

        Assert.Equal(3, result.Length);
        Assert.Contains(result, x => x.SequenceEqual([4, 1, 1]));
        Assert.Contains(result, x => x.SequenceEqual([3, 2, 1]));
        Assert.Contains(result, x => x.SequenceEqual([2, 2, 2]));
    }
}
