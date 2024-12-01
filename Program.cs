var solutions = new AdventSolutions();
var day = solutions.GetDay(2015, 2);

await day.DownloadInputAsync();

day.Part1();
day.Part2();
