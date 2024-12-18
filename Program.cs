var solutions = new AdventSolutions();
var day = solutions.GetDay(2024, 18);

await day.DownloadInputAsync();

day.Part1();
day.Part2();
