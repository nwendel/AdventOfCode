namespace AdventOfCode._2024;

public class Day11 : AdventBase
{
    protected override object InternalPart1()
    {
        var stones = Input.Lines[0].ExtractNumbers();

        for (var ix2 = 0; ix2 < 25; ix2++)
        {
            var n = new List<long>();
            for (var ix = 0; ix < stones.Length; ix++)
            {
                var stone = stones[ix];
                if (stone == 0)
                {
                    n.Add(1);
                }
                else if (stone.ToString().Length % 2 == 0)
                {
                    string stoneStr = stone.ToString();
                    int mid = stoneStr.Length / 2;
                    int left = int.Parse(stoneStr.Substring(0, mid));
                    int right = int.Parse(stoneStr.Substring(mid));
                    n.Add(left);
                    n.Add(right);
                }
                else
                {
                    n.Add(stone * 2024);
                }
            }
            stones = n.ToArray();
        }

        return stones.Count();
    }

    protected override object InternalPart2()
    {
        var s = Input.Lines[0].ExtractNumbers();

        var stones = new Dictionary<long, long>();
        foreach (var stone in s)
        {
            stones.AddOrUpdate(stone, 1);
        }


        for (var ix2 = 0; ix2 < 75; ix2++)
        {
            var n = new Dictionary<long, long>();
            foreach (var stone in stones)
            {
                if (stone.Key == 0)
                {
                    n.AddOrUpdate(1, stone.Value);
                }
                else if (stone.Key.ToString().Length % 2 == 0)
                {
                    string stoneStr = stone.Key.ToString();
                    int mid = stoneStr.Length / 2;
                    int left = int.Parse(stoneStr.Substring(0, mid));
                    int right = int.Parse(stoneStr.Substring(mid));

                    n.AddOrUpdate(left, stone.Value);
                    n.AddOrUpdate(right, stone.Value);
                }
                else
                {
                    n.AddOrUpdate(stone.Key * 2024, stone.Value);
                }
            }
            stones = n;
        }

        return stones.Sum(x => x.Value);
    }
}
