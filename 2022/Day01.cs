﻿namespace AdventOfCode._2022;

public class Day01 : AdventBase
{
    protected override object InternalPart1()
        => Input.Blocks
            .Select(x => x.Lines
                .Select(int.Parse)
                .Sum())
            .Max();

    protected override object InternalPart2()
        => Input.Blocks
            .Select(x => x.Lines
                .Select(int.Parse)
                .Sum())
            .OrderByDescending(x => x)
            .Take(3)
            .Sum();
}
