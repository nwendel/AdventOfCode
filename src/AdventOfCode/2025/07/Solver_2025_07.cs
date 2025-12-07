namespace AdventOfCode._2025._07;

public class Solver_2025_07 : Solver<ParsedInput>
{
    protected override ParsedInput ParseInput(Input input)
    {
        var matrix = input.ToMatrix();
        var start = matrix.Locate('S');

        return new(matrix, start);
    }

    protected override Result SolvePart1Core(ParsedInput input)
    {
        var splits = 0L;
        var visited = new HashSet<Position2>();
        var queue = new Queue<Position2>();

        queue.Enqueue(input.Start);
        visited.Add(input.Start);

        while (queue.Count > 0)
        {
            var next = queue.Dequeue().Move(Direction4.South);
            if (!input.Matrix.Contains(next))
            {
                continue;
            }

            var cell = input.Matrix[next];
            if (cell == '^')
            {
                if (visited.Add(next))
                {
                    splits += 1;

                    foreach (var neighbor in next.EnumerateMoves([Direction4.West, Direction4.East]))
                    {
                        if (input.Matrix.Contains(neighbor) && visited.Add(neighbor))
                        {
                            queue.Enqueue(neighbor);
                        }
                    }
                }
            }
            else
            {
                if (visited.Add(next))
                {
                    queue.Enqueue(next);
                }
            }
        }

        return splits;
    }

    protected override Result SolvePart2Core(ParsedInput input)
    {
        var memo = new Memoize<Position2, long>((position, memo) => CountPaths(input.Matrix, position, memo));
        var result = memo.Get(input.Start);

        return result;
    }

    private static long CountPaths(Matrix2<char> matrix, Position2 current, Memoize<Position2, long> memo)
    {
        var next = current.Move(Direction4.South);
        if (!matrix.Contains(next))
        {
            return 1;
        }

        var cell = matrix[next];

        if (cell == '^')
        {
            var left = next.Move(Direction4.West);
            var right = next.Move(Direction4.East);

            return memo.Get(left) + memo.Get(right);
        }
        else
        {
            return memo.Get(next);
        }
    }
}

public record ParsedInput(
    Matrix2<char> Matrix,
    Position2 Start);
