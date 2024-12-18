namespace AdventOfCode._2024;

public class Day13 : AdventBase
{
    protected override object InternalPart1()
    {
        var machines = Input.Blocks
            .Select(x => new Machine
            {
                AX = x.Lines[0].ExtractNumbers()[0],
                AY = x.Lines[0].ExtractNumbers()[1],
                BX = x.Lines[1].ExtractNumbers()[0],
                BY = x.Lines[1].ExtractNumbers()[1],
                PrizeX = x.Lines[2].ExtractNumbers()[0],
                PrizeY = x.Lines[2].ExtractNumbers()[1]
            })
            .ToArray();

        var tokens = 0L;

        foreach (var machine in machines)
        {
            var minTokens = int.MaxValue;

            for (int aPresses = 0; aPresses <= 100; aPresses++)
            {
                for (int bPresses = 0; bPresses <= 100; bPresses++)
                {
                    if (aPresses * machine.AX + bPresses * machine.BX == machine.PrizeX &&
                        aPresses * machine.AY + bPresses * machine.BY == machine.PrizeY)
                    {
                        var currentTokens = aPresses * 3 + bPresses * 1;
                        if (currentTokens < minTokens)
                        {
                            minTokens = currentTokens;
                        }
                    }
                }
            }

            if (minTokens != int.MaxValue)
            {
                tokens += minTokens;
            }
        }

        return tokens;
    }

    protected override object InternalPart2()
    {
        var machines = Input.Blocks
            .Select(x => new Machine
            {
                AX = x.Lines[0].ExtractNumbers()[0],
                AY = x.Lines[0].ExtractNumbers()[1],
                BX = x.Lines[1].ExtractNumbers()[0],
                BY = x.Lines[1].ExtractNumbers()[1],
                PrizeX = x.Lines[2].ExtractNumbers()[0] + 10000000000000,
                PrizeY = x.Lines[2].ExtractNumbers()[1] + 10000000000000,
            })
            .ToArray();

        var tokens = 0L;

        foreach (var machine in machines)
        {
            var aPresses = (machine.PrizeX * machine.BY - machine.PrizeY * machine.BX) / (machine.AX * machine.BY - machine.AY * machine.BX);
            var bPresses = (machine.PrizeY - machine.AY * aPresses) / machine.BY;

            if (aPresses * (machine.AX * machine.BY - machine.AY * machine.BX) != machine.PrizeX * machine.BY - machine.PrizeY * machine.BX ||
                bPresses * machine.BY != machine.PrizeY - machine.AY * aPresses)
            {
                continue;
            }

            tokens += aPresses * 3 + bPresses;
        }

        return tokens;
    }

    private record Machine
    {
        public long AX { get; set; }

        public long AY { get; set; }

        public long BX { get; set; }

        public long BY { get; set; }

        public long PrizeX { get; set; }

        public long PrizeY { get; set; }
    }
}
