using System.Linq.Expressions;

namespace AdventOfCode.Common;

public class Equation
{
    public Equation(Expression<Func<bool>> expression)
    {
        Expression = expression;
    }

    public Expression<Func<bool>> Expression { get; }
}
