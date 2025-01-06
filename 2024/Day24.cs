namespace AdventOfCode._2024;

public class Day24 : AdventBase
{
    protected override object InternalPart1()
    {
        var wires = Input.Blocks[0].Lines
            .Select(x => x.Split(": "))
            .ToDictionary(x => x[0], x => int.Parse(x[1]));
        var gates = Input.Blocks[1].Lines
            .Select(x => x.Split(' '))
            .ToList();

        while (true)
        {
            if (!gates.Select(x => x[4]).Any(x => x.StartsWith("z")))
            {
                break;
            }


            var gate = gates.First(x => wires.ContainsKey(x[0]) && wires.ContainsKey(x[2]));
            gates.Remove(gate);

            var input1 = wires[gate[0]];
            var input2 = wires[gate[2]];

            var output = gate[1] switch
            {
                "AND" => input1 & input2,
                "OR" => input1 | input2,
                "XOR" => (input1 ^ input2),
            };

            wires[gate[4]] = output;
        }

        var zwires = wires
            .Where(x => x.Key.StartsWith("z"))
            .OrderByDescending(x => x.Key)
            .Select(x => (char)(x.Value + '0'))
            .ToArray();
        var value = new string(zwires);

        return Convert.ToInt64(value, 2);
    }

    protected override object InternalPart2()
    {
        var wires = Input.Blocks[0].Lines
            .Select(x => x.Split(": "))
            .ToDictionary(x => x[0], x => int.Parse(x[1]));

        var gates = Input.Blocks[1].Lines
            .Select(x => x.Split(' '))
            .ToList();

        var nxz = gates
            .Where(x => x[4].StartsWith('z') && x[4] != "z45" && x[1] != "XOR")
            .ToList();
        var xnz = gates
            .Where(x => !x[0].StartsWith("x") && !x[2].StartsWith("x") && !x[4].StartsWith('z') && x[1] == "XOR")
            .ToList();

        foreach (var gate in xnz)
        {
            var b = nxz.First(x => x[4] == FirstZThatUsesOutput(gates, gate[4]));
            (b[4], gate[4]) = (gate[4], b[4]);
        }

        var carry = (GetWiresValue(wires, 'x') + GetWiresValue(wires, 'y') ^ Calculate(gates, wires))
            .CountTrailingZeroBits()
            .ToString();
        var result = nxz
            .Concat(xnz)
            .Concat(gates.Where(x => x[0].EndsWith(carry) && x[2].EndsWith(carry)))
            .Select(x => x[4])
            .OrderBy(x => x)
            .ToArray();

        return string.Join(',', result);
    }

    private static string? FirstZThatUsesOutput(List<string[]> gates, string output)
    {
        var x = gates
            .Where(x => x[0] == output || x[2] == output)
            .ToList();
        var zGate = x.FirstOrDefault(x => x[4].StartsWith('z'));
        if (zGate != null)
        {
            return "z" + (int.Parse(zGate[4][1..]) - 1).ToString("D2");
        }

        return x
            .Select(x => FirstZThatUsesOutput(gates, x[4]))
            .FirstOrDefault();
    }

    private static long Calculate(List<string[]> gates, Dictionary<string, int> wires)
    {
        var exclude = new HashSet<string[]>();

        while (exclude.Count != gates.Count)
        {
            var available = gates
                .Where(a => !exclude.Contains(a) && gates.All(b => (a[0] != b[4] && a[2] != b[4]) || exclude.Contains(b)))
                .ToList();
            foreach (var a in available)
            {
                var v1 = wires.GetValueOrDefault(a[0], 0);
                var v2 = wires.GetValueOrDefault(a[2], 0);
                wires[a[4]] = a[1] switch
                {
                    "AND" => v1 & v2,
                    "OR" => v1 | v2,
                    "XOR" => v1 ^ v2,
                };
            }
            exclude.UnionWith(available);
        }

        return GetWiresValue(wires, 'z');
    }

    private static long GetWiresValue(Dictionary<string, int> wires, char type)
    {
        var value = wires
            .Where(x => x.Key.StartsWith(type))
            .OrderByDescending(x => x.Key)
            .Select(x => (char)(x.Value + '0'))
            .ToArray();
        return Convert.ToInt64(new string(value), 2);
    }
}
