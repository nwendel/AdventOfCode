using AdventOfCode.Common;

namespace AdventOfCode.Tests.Common;

public class CombinatoricsTests_WeakCompositions
{
    [Fact]
    public void WeakCompositions_3Into2Parts_Returns4Compositions()
    {
        var result = Combinatorics.WeakCompositions(3, 2).ToArray();

        Assert.Equal(4, result.Length);
        Assert.Contains(result, x => x.SequenceEqual([0, 3]));
        Assert.Contains(result, x => x.SequenceEqual([1, 2]));
        Assert.Contains(result, x => x.SequenceEqual([2, 1]));
        Assert.Contains(result, x => x.SequenceEqual([3, 0]));
    }

    [Fact]
    public void WeakCompositions_4Into3Parts_Returns15Compositions()
    {
        var result = Combinatorics.WeakCompositions(4, 3).ToArray();

        Assert.Equal(15, result.Length);
        Assert.Contains(result, x => x.SequenceEqual([0, 0, 4]));
        Assert.Contains(result, x => x.SequenceEqual([0, 1, 3]));
        Assert.Contains(result, x => x.SequenceEqual([0, 2, 2]));
        Assert.Contains(result, x => x.SequenceEqual([0, 3, 1]));
        Assert.Contains(result, x => x.SequenceEqual([0, 4, 0]));
        Assert.Contains(result, x => x.SequenceEqual([1, 0, 3]));
        Assert.Contains(result, x => x.SequenceEqual([1, 1, 2]));
        Assert.Contains(result, x => x.SequenceEqual([1, 2, 1]));
        Assert.Contains(result, x => x.SequenceEqual([1, 3, 0]));
        Assert.Contains(result, x => x.SequenceEqual([2, 0, 2]));
        Assert.Contains(result, x => x.SequenceEqual([2, 1, 1]));
        Assert.Contains(result, x => x.SequenceEqual([2, 2, 0]));
        Assert.Contains(result, x => x.SequenceEqual([3, 0, 1]));
        Assert.Contains(result, x => x.SequenceEqual([3, 1, 0]));
        Assert.Contains(result, x => x.SequenceEqual([4, 0, 0]));
    }

    [Fact]
    public void WeakCompositions_AllCompositionsSumToTotal()
    {
        var total = 5;
        var parts = 3;
        var result = Combinatorics.WeakCompositions(total, parts).ToArray();

        foreach (var composition in result)
        {
            Assert.Equal(total, composition.Sum());
        }
    }

    [Fact]
    public void WeakCompositions_AllCompositionsHaveExactParts()
    {
        var parts = 4;
        var result = Combinatorics.WeakCompositions(6, parts).ToArray();

        foreach (var composition in result)
        {
            Assert.Equal(parts, composition.Length);
        }
    }

    [Fact]
    public void WeakCompositions_1Part_ReturnsSingleComposition()
    {
        var result = Combinatorics.WeakCompositions(5, 1).ToArray();

        var composition = Assert.Single(result);
        Assert.True(composition.SequenceEqual([5]));
    }
}
