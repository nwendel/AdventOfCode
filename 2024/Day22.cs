namespace AdventOfCode._2024;

public class Day22 : AdventBase
{
    protected override object InternalPart1()
    {
        var secrets = Input.Lines.Select(x => int.Parse(x)).ToArray();
        var sum = 0L;

        foreach (var secret in secrets)
        {
            var secret2000 = GenerateSecretNumber(secret, 2000);
            sum += secret2000;
        }

        return sum;
    }

    private static long GenerateSecretNumber(long secret, int count)
    {
        for (int i = 0; i < count; i++)
        {
            secret = MixAndPrune(secret, secret * 64);
            secret = MixAndPrune(secret, secret / 32);
            secret = MixAndPrune(secret, secret * 2048);
        }

        return secret;
    }

    private static long MixAndPrune(long secret, long value)
    {
        secret ^= value;
        secret %= 16777216L;
        return secret;
    }

    protected override object InternalPart2()
    {
        var initialSecrets = Input.Lines.Select(x => long.Parse(x)).ToArray();
        var bestSequence = FindBestSequence(initialSecrets, 2000);
        return bestSequence;
    }

    private long FindBestSequence(long[] initialSecrets, int iterations)
    {
        var sequences = new HashSet<string>();
        var maxBananas = 0L;

        var memos = new Dictionary<(long, string), long>();

        foreach (var secret in initialSecrets)
        {
            var prices = GeneratePrices(secret, iterations);
            var changes = prices
                .SlidingWindow(2)
                .Select(x => x[1] - x[0])
                .ToArray();

            var sequences2 = changes
                .SlidingWindow(4)
                .Distinct()
                .Select(x => string.Join(',', x))
                .ToArray();
            foreach (var s2 in sequences2)
            {
                sequences.Add(s2);
            }

            var secretMemos = changes
                .SlidingWindowIx(4)
                .Select(x => (Price: prices[x.Item1 + 4], Sequence: string.Join(',', x.Item2)))
                .ToArray();
            foreach (var (price, sequence) in secretMemos)
            {
                memos.TryAdd((secret, sequence), price);
            }
        }

        for (var ix = 0; ix < sequences.Count; ix++)
        {
            var sequence = sequences.ElementAt(ix);

            var bananas = 0L;

            foreach (var secret in initialSecrets)
            {

                if (memos.TryGetValue((secret, sequence), out var price))
                {
                    bananas += price;
                }
            }

            maxBananas = Math.Max(maxBananas, bananas);

            if (ix % 100 == 0)
            {
                Console.WriteLine($"{DateTime.Now} {ix} {maxBananas}");
            }
        }

        return maxBananas;
    }

    private static List<int> GeneratePrices(long secret, int iterations)
    {
        var prices = new List<int>
        {
            (int)(secret % 10)
        };

        for (int i = 0; i < iterations; i++)
        {
            secret = MixAndPrune(secret, secret * 64);
            secret = MixAndPrune(secret, secret / 32);
            secret = MixAndPrune(secret, secret * 2048);

            prices.Add((int)(secret % 10));
        }

        return prices;
    }
}
