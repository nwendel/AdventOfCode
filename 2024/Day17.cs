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
        var a = Input.Blocks[0].Lines[0].ExtractNumbers()[0];
        var b = Input.Blocks[0].Lines[1].ExtractNumbers()[0];
        var c = Input.Blocks[0].Lines[2].ExtractNumbers()[0];

        var program = Input.Blocks[1].Lines[0].ExtractNumbers();

        return Dfs(0, 0).Min();


        List<long> Dfs(long curVal, int depth)
        {
            List<long> res = new();
            if (depth > program.Length) return res;
            var tmp = curVal << 3;
            for (int i = 0; i < 8; i++)
            {
                var tmpRes = RunProgram(tmp + i);
                if (tmpRes.SequenceEqual(program.TakeLast(depth + 1)))
                {
                    if (depth + 1 == program.Length) res.Add(tmp + i);
                    res.AddRange(Dfs(tmp + i, depth + 1));
                }
            }

            return res;
        }

        List<long> RunProgram(long regA)
        {
            long regB = 0;
            long regC = 0;
            List<long> output = new();
            int pc = 0;
            while (pc < program.Length)
            {
                long combo = (program[pc + 1]) switch
                {
                    0 => 0,
                    1 => 1,
                    2 => 2,
                    3 => 3,
                    4 => regA,
                    5 => regB,
                    6 => regC,
                    _ => long.MinValue
                };

                long literal = program[pc + 1];
                long res = 0;
                bool jumped = false;
                switch (program[pc])
                {
                    case 0:
                        res = (long)(regA / Math.Pow(2, combo));
                        regA = res;
                        break;
                    case 1:
                        res = regB ^ literal;
                        regB = res;
                        break;
                    case 2:
                        res = combo % 8;
                        regB = res;
                        break;
                    case 3:
                        if (regA != 0)
                        {
                            pc = (int)literal;
                            jumped = true;
                        }
                        break;
                    case 4:
                        res = regB ^ regC;
                        regB = res;
                        break;
                    case 5:
                        output.Add(combo % 8);
                        break;
                    case 6:
                        res = (long)(regA / Math.Pow(2, combo));
                        regB = res;
                        break;
                    case 7:
                        res = (long)(regA / Math.Pow(2, combo));
                        regC = res;
                        break;
                    default: break;
                }
                if (!jumped) pc += 2;
                if (output.Count > program.Length) break;
            }

            return output;
        }

    }

    protected object InternalPart3()
    {
        var program = Input.Blocks[1].Lines[0].ExtractNumbers();

        var increment = 1L; // (long)Math.Pow(8, program.Length - 1);

        Console.WriteLine("2,4,1,1,7,5,1,5,0,3,4,3,5,5,3,0");

        for (var originalA = 1L; true; originalA += increment)
        {
            var a = originalA;
            var b = Input.Blocks[0].Lines[1].ExtractNumbers()[0];
            var c = Input.Blocks[0].Lines[2].ExtractNumbers()[0];
            var ix = 0;

            var pix = 0;
            // var states = new HashSet<(int, int, long, long, long)>();
            var output = new List<long>();

            Console.Write(a);
            Console.Write(": ");

            while (ix < program.Length)
            {
                /*
                if (states.Contains((ix, pix, a, b, c)))
                {
                    break;
                }

                states.Add((ix, pix, a, b, c));
                */

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
                        Console.Write(output.Last());
                        Console.Write(" ");

                        if (output[pix] == program[pix])
                        {
                            pix += 1;
                            // increment = (long)Math.Pow(8, pix);
                            if (pix == 5)
                            {
                                Console.ReadLine();
                                // 6
                                // 14
                                // 332
                                // 31157 <-- but this seems wrong since it generates 5 digits?
                            }
                        }

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

            Console.WriteLine();
        }

        throw new UnreachableException();
    }
}
