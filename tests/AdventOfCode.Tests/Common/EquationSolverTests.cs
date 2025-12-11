using AdventOfCode.Common;

namespace AdventOfCode.Tests.Common;

public class EquationSolverTests
{
    [Fact]
    public void SimpleTwoVariableTwoEquations()
    {
        // x + y = 5
        // 2x + y = 8
        // Solution: x = 3, y = 2
        var x = EquationVariable.Create();
        var y = EquationVariable.Create();

        var equations = new List<Equation>
        {
            new(() => x >= EquationConstant.Zero),
            new(() => y >= EquationConstant.Zero),
            new(() => x * new EquationConstant(1) + y * new EquationConstant(1) == new EquationConstant(5)),
            new(() => x * new EquationConstant(2) + y * new EquationConstant(1) == new EquationConstant(8))
        };

        var result = EquationSolver.Solve(equations);

        Assert.Equal(3, result[0]);
        Assert.Equal(2, result[1]);
        Assert.Equal(5, result.Sum());
    }

    [Fact]
    public void SimpleThreeButtonsThreeCounters()
    {
        // Button 0 affects counters 0,1
        // Button 1 affects counters 1,2
        // Button 2 affects counters 0,2
        // Targets: [5, 3, 4]
        // Solution: b0 + b2 = 5, b0 + b1 = 3, b1 + b2 = 4
        // From eq1: b0 = 5 - b2
        // From eq2: b1 = 3 - b0 = 3 - (5 - b2) = b2 - 2
        // From eq3: (b2 - 2) + b2 = 4 => 2*b2 = 6 => b2 = 3
        // Then: b0 = 5 - 3 = 2, b1 = 3 - 2 = 1
        var b0 = EquationVariable.Create();
        var b1 = EquationVariable.Create();
        var b2 = EquationVariable.Create();

        var equations = new List<Equation>
        {
            new(() => b0 >= EquationConstant.Zero),
            new(() => b1 >= EquationConstant.Zero),
            new(() => b2 >= EquationConstant.Zero),
            // Counter 0: b0 + b2 = 5
            new(() => b0 * new EquationConstant(1) + b2 * new EquationConstant(1) == new EquationConstant(5)),
            // Counter 1: b0 + b1 = 3
            new(() => b0 * new EquationConstant(1) + b1 * new EquationConstant(1) == new EquationConstant(3)),
            // Counter 2: b1 + b2 = 4
            new(() => b1 * new EquationConstant(1) + b2 * new EquationConstant(1) == new EquationConstant(4))
        };

        var result = EquationSolver.Solve(equations);

        Assert.Equal(2, result[0]);
        Assert.Equal(1, result[1]);
        Assert.Equal(3, result[2]);
        Assert.Equal(6, result.Sum());
    }

    [Fact]
    public void UnderdeterminedSystemWithOptimization()
    {
        // x + y = 10
        // With 2 variables and 1 equation, infinitely many solutions
        // Minimizing sum should give us a valid solution
        var variables = EquationVariable.Create(2);
        var x = variables[0];
        var y = variables[1];

        var equations = new List<Equation>
        {
            new(() => x >= EquationConstant.Zero),
            new(() => y >= EquationConstant.Zero),
            new(() => x * new EquationConstant(1) + y * new EquationConstant(1) == new EquationConstant(10))
        };

        var result = EquationSolver.Solve(equations, () => variables.Sum(), EquationOptimizationGoal.Minimize);

        Assert.Equal(10, result.Sum());
        Assert.True(result[0] >= 0);
        Assert.True(result[1] >= 0);
        Assert.Equal(10, result[0] + result[1]);
    }
}
