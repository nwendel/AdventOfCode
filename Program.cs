AdventSolutions solutions = new();
var day = solutions.GetDay(2025, 2);

await day.DownloadInputAsync();

day.Part1();
day.Part2();
