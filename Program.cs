var solutions = new AdventSolutions();
var day = solutions.GetDay(2020, 1);

await day.DownloadInputAsync();

day.Part1();
day.Part2();
