var solutions = new AdventSolutions();
var day = solutions.GetDay(2016, 5);

await day.DownloadInputAsync();

day.Part1();
day.Part2();
