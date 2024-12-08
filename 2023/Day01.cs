namespace AdventOfCode._2023;

public class Day01 : AdventBase
{
    protected override object InternalPart1()
        => Input.Lines
            .Select(x => int.Parse($"{x.First(char.IsDigit)}{x.Last(char.IsDigit)}"))
            .Sum();

    protected override object InternalPart2()
        => Input.Lines
            .Select(x => ReplaceText(x))
            .Select(x => int.Parse($"{x.First(char.IsDigit)}{x.Last(char.IsDigit)}"))
            .Sum();

    private static string ReplaceText(string text)
        => text
            .Replace("zero", "zero0zero")
            .Replace("one", "one1one")
            .Replace("two", "two2two")
            .Replace("three", "three3three")
            .Replace("four", "four4four")
            .Replace("five", "five5five")
            .Replace("six", "six6six")
            .Replace("seven", "seven7seven")
            .Replace("eight", "eight8eight")
            .Replace("nine", "nine9nine");
}
