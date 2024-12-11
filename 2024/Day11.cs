namespace AdventOfCode._2024;

public class Day11 : AdventBase
{
    protected override object InternalPart1()
        => Solve(25);

    protected override object InternalPart2()
        => Solve(75);

    private long Solve(int iterations)
    {
        var stones = Input.Text().ExtractNumbers();

        var stoneCounts = new Counter<long, long>(stones);

        for (var blink = 0; blink < iterations; blink++)
        {
            var n = new Counter<long, long>();

            foreach (var stoneCount in stoneCounts)
            {
                if (stoneCount.Key == 0)
                {
                    n[1] += stoneCount.Value;
                }
                else if (stoneCount.Key.ToString().Length % 2 == 0)
                {
                    var text = stoneCount.Key.ToString();
                    var ix = text.Length / 2;
                    var left = long.Parse(text[..ix]);
                    var right = long.Parse(text[ix..]);
                    n[left] += stoneCount.Value;
                    n[right] += stoneCount.Value;
                }
                else
                {
                    n[stoneCount.Key * 2024] += stoneCount.Value;
                }
            }

            stoneCounts = n;
        }

        return stoneCounts.Sum(x => x.Value);
    }
}
