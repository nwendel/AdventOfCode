namespace AdventOfCode._2024;

public class Day17 : AdventBase
{
    protected override object InternalPart1()
    {
        var a = Input.Blocks[0].Lines[0].ExtractNumbers()[0];
        var b = Input.Blocks[0].Lines[1].ExtractNumbers()[0];
        var c = Input.Blocks[0].Lines[2].ExtractNumbers()[0];

        var program = Input.Blocks[1].Lines[0].ExtractNumbers();
        var ix = 0;

        var output = new List<long>();

        while (ix < program.Length)
        {
            var opcode = program[ix];
            var operand = program[ix + 1];

            switch (opcode)
            {
                case 0: // adv
                    a /= (long)Math.Pow(2, GetValue(operand, a, b, c));
                    break;
                case 1: // bxl
                    b ^= operand;
                    break;
                case 2: // bst
                    b = GetValue(operand, a, b, c) % 8;
                    break;
                case 3: // jnz
                    if (a != 0)
                    {
                        ix = (int)operand - 2;
                    }
                    break;
                case 4: // bxc
                    b ^= c;
                    break;
                case 5: // out
                    output.Add(GetValue(operand, a, b, c) % 8);
                    break;
                case 6: // bdv
                    b = a / (long)Math.Pow(2, GetValue(operand, a, b, c));
                    break;
                case 7: // cdv
                    c = a / (long)Math.Pow(2, GetValue(operand, a, b, c));
                    break;
            }

            ix += 2;
        }

        return string.Join(',', output);
    }

    private long GetValue(long operand, long a, long b, long c)
    {
        return operand switch
        {
            0 => 0,
            1 => 1,
            2 => 2,
            3 => 3,
            4 => a,
            5 => b,
            6 => c,
        };
    }

    protected override object InternalPart2()
    {
        var b = Input.Blocks[0].Lines[1].ExtractNumbers()[0];
        var c = Input.Blocks[0].Lines[2].ExtractNumbers()[0];

        var program = Input.Blocks[1].Lines[0].ExtractNumbers();
        var ix = 0;

        var increment = (long)Math.Pow(8, program.Length - 1);

        for (var originalA = 1L; true; originalA += increment)
        {
            var a = originalA;
            var pix = 0;

            var states = new HashSet<(int, int, long, long, long)>();

            while (ix < program.Length)
            {
                if (states.Contains((ix, pix, a, b, c)))
                {
                    break;
                }

                states.Add((ix, pix, a, b, c));

                var opcode = program[ix];
                var operand = program[ix + 1];
                var failed = false;

                switch (opcode)
                {
                    case 0: // adv
                        a /= (long)Math.Pow(2, GetValue(operand, a, b, c));
                        break;
                    case 1: // bxl
                        b ^= operand;
                        break;
                    case 2: // bst
                        b = GetValue(operand, a, b, c) % 8;
                        break;
                    case 3: // jnz
                        if (a != 0)
                        {
                            ix = (int)operand - 2;
                        }
                        break;
                    case 4: // bxc
                        b ^= c;
                        break;
                    case 5: // out
                        if ((GetValue(operand, a, b, c) % 8) != program[pix])
                        {
                            failed = true;
                        }
                        else
                        {
                            pix += 1;
                            increment /= 8;
                        }
                        break;
                    case 6: // bdv
                        b = a / (long)Math.Pow(2, GetValue(operand, a, b, c));
                        break;
                    case 7: // cdv
                        c = a / (long)Math.Pow(2, GetValue(operand, a, b, c));
                        break;
                }

                if (failed)
                {
                    continue;
                }

                ix += 2;
            }

            if (pix == program.Length)
            {
                return originalA;
            }
        }

        throw new UnreachableException();
    }
}
