namespace AdventOfCode._2024;

public class Day09 : AdventBase
{
    protected override object InternalPart1()
    {
        var diskMap = Input.Text();

        var blocks = new List<int?>();

        for (var ix = 0; ix < diskMap.Length; ix++)
        {
            var length = diskMap[ix] - '0';
            int? id = ix % 2 == 0
                ? ix / 2
                : null;
            blocks.AddRange(Enumerable.Repeat(id, length));
        }

        while (true)
        {
            var freeIx = blocks.IndexOf(null);
            var fileIx = blocks.FindLastIndex(x => x.HasValue);

            if (freeIx > fileIx)
            {
                break;
            }

            blocks[freeIx] = blocks[fileIx];
            blocks[fileIx] = null;
        }

        var checksum = 0L;
        for (var ix = 0; ix < blocks.Count; ix++)
        {
            checksum += (blocks[ix] ?? 0) * ix;
        }

        return checksum;
    }

    protected override object InternalPart2()
    {
        var diskMap = Input.Text();

        var files = new List<(int Id, int Position, int Length)>();
        var free = new List<(int Position, int Length)>();

        var blocks = new List<int?>();

        var position = 0;
        for (var ix = 0; ix < diskMap.Length; ix++)
        {
            var length = diskMap[ix] - '0';
            int? id = ix % 2 == 0
                ? ix / 2
                : null;

            blocks.AddRange(Enumerable.Repeat(id, length));
            if (ix % 2 == 0)
            {
                files.Add((Id: ix / 2, position, length));
            }
            else
            {
                free.Add((position, length));
            }

            position += length;
        }

        files.Reverse();
        foreach (var file in files)
        {
            var to = free.FirstOrDefault(x => x.Length >= file.Length);
            if (to == default || to.Position > file.Position)
            {
                continue;
            }

            for (var ix = 0; ix < file.Length; ix++)
            {
                blocks[to.Position + ix] = file.Id;
                blocks[file.Position + ix] = null;
            }

            free[free.IndexOf(to)] = to with { Length = to.Length - file.Length, Position = to.Position + file.Length };
        }

        var checksum = 0L;
        for (var ix = 0; ix < blocks.Count; ix++)
        {
            checksum += (blocks[ix] ?? 0) * ix;
        }

        return checksum;
    }
}
