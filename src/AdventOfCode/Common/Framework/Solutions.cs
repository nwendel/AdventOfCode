using AngleSharp;
using FluentRegistration;
using Microsoft.Extensions.DependencyInjection;
using MarkdownConverter = Html2Markdown.Converter;

namespace AdventOfCode.Common.Framework;

public partial class Solutions
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Lazy<AocClient> _aocClient = new(() => new AocClient());
    private readonly MarkdownConverter _markdownConverter = new();

    public Solutions()
    {
        var services = new ServiceCollection();

        services.Register(r => r
            .FromThisAssembly()
            .Where(c => c.AssignableTo<ISolver>())
            .WithServices.Service<ISolver>()
            .Lifetime.Singleton());

        _serviceProvider = services.BuildServiceProvider();
    }

    public async Task Solve(Day day)
    {
        var solvers = _serviceProvider.GetServices<ISolver>();
        var solver = solvers.Single(x => x.Day == day);

        await DownloadInput(day);
        var input = await GetInput(day);

        Console.WriteLine($"Solving {day.Year}-{day.Number:00}");

        Solve(solver, 1, x => x.SolvePart1(input));
        Solve(solver, 2, x => x.SolvePart2(input));
    }

    public async Task Verify(Day day)
    {
        var solvers = _serviceProvider.GetServices<ISolver>();
        var solver = solvers.Single(x => x.Day == day);

        await DownloadInput(day);
        var input = await GetInput(day);

        await DownloadDescription(day);
        var answers = await ParseAnswers(day);

        Console.WriteLine($"Verifying {day.Year}-{day.Number:00}");

        Verify(solver, 1, x => x.SolvePart1(input), answers.Part1);
        Verify(solver, 2, x => x.SolvePart2(input), answers.Part2);
    }

    public async Task VerifyAll()
    {
        var solvers = _serviceProvider.GetServices<ISolver>()
            .OrderBy(x => x.Day.Year)
            .ThenBy(x => x.Day.Number)
            .ToList();

        foreach (var solver in solvers)
        {
            await Verify(solver.Day);
            await Task.Delay(1000);
        }
    }

    private async Task DownloadInput(Day day)
    {
        var path = GetInputPath(day);
        if (File.Exists(path))
        {
            Console.WriteLine("Input already exists");
            return;
        }

        var input = await _aocClient.Value.DownloadInputAsync(day);

        Directory.CreateDirectory($"{Path.GetDirectoryName(path)}");
        await File.WriteAllTextAsync(path, input);
    }

    private async Task DownloadDescription(Day day)
    {
        var htmlPath = GetDescriptionPath(day, DescriptionFormat.Html);
        var markdownPath = GetDescriptionPath(day, DescriptionFormat.Markdown);

        if (File.Exists(htmlPath) && File.Exists(markdownPath))
        {
            Console.WriteLine("Description html and markdown already exists");
            return;
        }

        if (!File.Exists(htmlPath))
        {
            var htmlDescription = await _aocClient.Value.DownloadDescriptionAsync(day);

            Directory.CreateDirectory($"{Path.GetDirectoryName(htmlPath)}");
            await File.WriteAllTextAsync(htmlPath, htmlDescription);
        }

        if (!File.Exists(markdownPath))
        {
            var htmlDescription = await File.ReadAllTextAsync(htmlPath);

            var context = BrowsingContext.New(Configuration.Default);
            var document = await context.OpenAsync(req => req.Content(htmlDescription));
            var mainElement = document.QuerySelector("main");
            var mainContent = mainElement?.InnerHtml;

            if (mainContent == null)
            {
                throw new Exception("Could not find main element in description html");
            }

            // TODO: This conversion still needs improvement
            var markdownDescription = _markdownConverter.Convert(mainContent);

            await File.WriteAllTextAsync(markdownPath, markdownDescription);
        }
    }

    private static async Task<Input> GetInput(Day day)
    {
        var path = GetInputPath(day);
        if (!File.Exists(path))
        {
            throw new NoInputFoundException();
        }

        var input = await File.ReadAllTextAsync(path);
        if (input[^1] == '\n')
        {
            input = input[..^1];
        }

        return new(input);
    }

    private static async Task<Answers> ParseAnswers(Day day)
    {
        var path = GetDescriptionPath(day, DescriptionFormat.Html);
        var description = await File.ReadAllTextAsync(path);

        var matches = AnswersRegex().Matches(description);

        var answers = matches.Count switch
        {
            0 => new Answers(null, null),
            1 => new Answers(matches[0].Groups[1].Value, null),
            2 => new Answers(matches[0].Groups[1].Value, matches[1].Groups[1].Value),
            _ => throw new InvalidOperationException("Too many answers"),
        };

        return answers;
    }

    private static string GetInputPath(Day day)
        => $"../../../{day.Year}/{day.Number:00}/{day.Year}_{day.Number:00}_Input.txt";

    private static string GetDescriptionPath(Day day, DescriptionFormat format)
        => $"../../../{day.Year}/{day.Number:00}/{day.Year}_{day.Number:00}_Description.{format.ToFileExtension()}";

    private static void Solve(ISolver solver, int part, Func<ISolver, object> solve)
    {
        try
        {
            var result = solve(solver);

            Console.WriteLine($"Part {part}: {result}");
        }
        catch (NotImplementedException)
        {
            Console.WriteLine($"Part {part}: Not implemented");
        }
    }

    private static void Verify(ISolver solver, int part, Func<ISolver, object> solve, string? expected)
    {
        if (expected == null)
        {
            Console.WriteLine($"Part {part}: Unsolved");
            return;
        }

        try
        {
            var result = solve(solver);

            if ($"{result}" == expected)
            {
                Console.WriteLine($"Part {part}: Correct");
            }
            else
            {
                Console.WriteLine($"Part {part}: Wrong");
            }
        }
        catch (NotImplementedException)
        {
            Console.WriteLine($"Part {part}: Not implemented");
        }
    }

    [GeneratedRegex(""""Your puzzle answer was <code>(.+?)</code>"""")]
    private static partial Regex AnswersRegex();
}
