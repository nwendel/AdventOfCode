var solutions = new AdventSolutions();
var day = solutions.GetDay(2024, 8);

await day.DownloadInputAsync();

day.Part1();
day.Part2();
