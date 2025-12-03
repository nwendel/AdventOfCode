AdventSolutions solutions = new();
var day = solutions.GetDay(2025, 3);

await day.DownloadInputAsync();

day.Part1();
day.Part2();
