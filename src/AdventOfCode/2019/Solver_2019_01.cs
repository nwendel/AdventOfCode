namespace AdventOfCode._2019_01;

public class Solver_2019_01 : Solver<long[]>
{
    protected override long[] ParseInput(Input input)
    {
        var parsedInput = input.Lines
            .Select(line => line.ToLong())
            .ToArray();

        return parsedInput;
    }

    protected override Result SolvePart1Core(long[] input)
    {
        var result = input.Sum(x => x / 3 - 2);

        return result;
    }

    protected override Result SolvePart2Core(long[] input)
    {
        var result = input.Sum(x => FuelForMass(x));

        return result;

        static long FuelForMass(long mass)
        {
            var fuel = 0L;

            while (true)
            {
                mass = mass / 3 - 2;
                if (mass <= 0)
                {
                    break;
                }

                fuel += mass;
            }

            return fuel;
        }
    }
}
