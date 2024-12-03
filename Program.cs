var solutions = new AdventSolutions();
var day = solutions.GetDay(2024, 3);

await day.DownloadInputAsync();

day.Part1();
day.Part2();
