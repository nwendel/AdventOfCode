using System.Net.Http.Headers;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace AdventOfCode.Common.Framework;

public class AocClient
{
    private readonly HttpClient _httpClient;

    public AocClient()
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets(Assembly.GetExecutingAssembly())
            .Build();

        var cookie = configuration["session"]?.Trim();
        if (cookie == null)
        {
            throw new NoSessionException();
        }

        var handler = new HttpClientHandler { UseCookies = false };
        _httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://adventofcode.com/")
        };
        _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("NiklasAdventOfCodeHelper", "2025.12.01"));
        _httpClient.DefaultRequestHeaders.Add("cookie", $"session={cookie}");
    }

    public Task<string> DownloadInputAsync(Day day)
        => DownloadAsync($"/{day.Year}/day/{day.Number}/input");

    public Task<string> DownloadDescriptionAsync(Day day)
        => DownloadAsync($"/{day.Year}/day/{day.Number}");

    private async Task<string> DownloadAsync(string path)
    {
        Console.WriteLine($"Downloading {path}");

        var response = await _httpClient.GetAsync(path);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return content;
    }
}
