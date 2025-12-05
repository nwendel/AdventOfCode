namespace AdventOfCode.Common.Framework;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class SlowAttribute : Attribute
{
    public bool Part1 { get; init; }

    public bool Part2 { get; init; }
}
