namespace AdventOfCode2._2023_01;

public class Solver_2023_01 : Solver
{
    protected override Result SolvePart1Core(Input input)
    {
        var result = input.Lines
            .Select(x => x.Digits())
            .Select(x => $"{x.First()}{x.Last()}")
            .Select(x => int.Parse(x))
            .Sum();

        return result;
    }

    protected override Result SolvePart2Core(Input input)
    {
        var modifiedInput = input
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

        var result = SolvePart1Core(modifiedInput);

        return result;
    }
}
