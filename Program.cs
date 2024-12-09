var solutions = new AdventSolutions();
var day = solutions.GetDay(2024, 9);

await day.DownloadInputAsync();

day.Part1();
day.Part2();
