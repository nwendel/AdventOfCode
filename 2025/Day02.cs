namespace AdventOfCode._2025;

internal class Day02 : AdventBase
{
    protected override object InternalPart1()
    {
        var result = 0L;

        var parts = Input.Text.Split(',', StringSplitOptions.RemoveEmptyEntries);

        foreach (var range in parts)
        {
            var startEnd = range
                .Split('-')
                .Select(x => long.Parse(x))
                .ToArray();

            for (var ix = startEnd[0]; ix <= startEnd[1]; ix++)
            {
                var id = $"{ix}";

                if (id.Length % 2 == 0)
                {
                    var m = id.Length / 2;
                    if (id[..m] == id[m..])
                    {
                        result += ix;
                    }
                }
            }
        }

        return result;
    }

    protected override object InternalPart2()
    {
        var result = 0L;

        var parts = Input.Text.Split(',', StringSplitOptions.RemoveEmptyEntries);

        foreach (var range in parts)
        {
            var startEnd = range
                .Split('-')
                .Select(x => long.Parse(x))
                .ToArray();

            for (var ix = startEnd[0]; ix <= startEnd[1]; ix++)
            {
                var id = $"{ix}";

                for (var checkLength = 1; checkLength <= id.Length / 2; checkLength++)
                {
                    var check = id[..checkLength];
                    var fullCheck = string.Concat(Enumerable.Repeat(check, id.Length / checkLength));

                    if (fullCheck == id)
                    {
                        result += ix;
                        break;
                    }
                }
            }
        }

        return result;
    }
}
