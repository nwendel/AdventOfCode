using System.Linq.Expressions;

namespace AdventOfCode.Common;

public class EquationOptimizer
{
    public EquationOptimizer(Expression<Func<IEquationElement>> expression, EquationOptimizationGoal goal)
    {
        Expression = expression;
        Goal = goal;
    }

    public Expression<Func<IEquationElement>> Expression { get; }

    public EquationOptimizationGoal Goal { get; }
}
