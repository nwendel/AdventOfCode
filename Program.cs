var solutions = new AdventSolutions();
var day = solutions.GetDay(2015, 4);

await day.DownloadInputAsync();

day.Part1();
day.Part2();
