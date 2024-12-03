using System.Text;

namespace AdventOfCode._2016;

public class Day04 : AdventBase
{
    protected override object InternalPart1()
    {
        var sum = 0L;

        foreach (var line in Input.Lines)
        {
            var name = line[..line.LastIndexOf('-')].Replace("-", string.Empty);
            var sector = Math.Abs(line.ExtractNumbers().Single());
            var checksum = line[(line.LastIndexOf('[') + 1)..^1];

            var calculated = name
                .GroupBy(k => k)
                .OrderByDescending(x => x.Count())
                .ThenBy(x => x.Key)
                .Take(5)
                .Select(x => x.Key)
                .ToArray();
            var check = new string(calculated);

            if (check == checksum)
            {
                sum += sector;
            }
        }

        return sum;
    }

    protected override object InternalPart2()
    {
        const string alphabet = "abcdefghijklmnopqrstuvwxyz";

        foreach (var line in Input.Lines)
        {
            var name = line[..line.LastIndexOf('-')].Replace("-", string.Empty);
            var sector = Math.Abs(line.ExtractNumbers().Single());
            var checksum = line[(line.LastIndexOf('[') + 1)..^1];

            var calculated = name
                .GroupBy(k => k)
                .OrderByDescending(x => x.Count())
                .ThenBy(x => x.Key)
                .Take(5)
                .Select(x => x.Key)
                .ToArray();
            var check = new string(calculated);

            if (check == checksum)
            {
                var builder = new StringBuilder();
                foreach (var c in name)
                {
                    var index = alphabet.IndexOf(c);
                    var newIndex = (index + (int)sector) % alphabet.Length;
                    builder.Append(alphabet[newIndex]);
                }

                if (builder.ToString() == "northpoleobjectstorage")
                {
                    return sector;
                }
            }
        }

        throw new UnreachableException();
    }
}
