using System.Linq.Expressions;
using Microsoft.Z3;

namespace AdventOfCode.Common;

public class EquationSolver
{
    public static void Solve(List<Equation> equations, EquationOptimizer? optimizer = null)
    {
        using var context = new Context();
        using var z3Optimizer = context.MkOptimize();

        var variableMap = new Dictionary<EquationVariable, IntExpr>();
        var variableList = new List<EquationVariable>();

        // First pass: collect all variables from equations
        foreach (var equation in equations)
        {
            CollectVariables(equation.Expression.Body, variableMap, variableList, context);
        }

        // Also collect variables from optimization expression if provided
        if (optimizer != null)
        {
            CollectVariables(optimizer.Expression.Body, variableMap, variableList, context);
        }

        // Second pass: translate equations to Z3 constraints
        foreach (var equation in equations)
        {
            var constraint = TranslateExpression(equation.Expression.Body, variableMap, context);
            z3Optimizer.Add(constraint);
        }

        // Set optimization goal if provided
        if (optimizer != null)
        {
            var optimizationExpr = TranslateOptimizationExpression(optimizer.Expression.Body, variableMap, context);
            if (optimizer.Goal == EquationOptimizationGoal.Minimize)
            {
                z3Optimizer.MkMinimize(optimizationExpr);
            }
            else
            {
                z3Optimizer.MkMaximize(optimizationExpr);
            }
        }

        // Solve
        var status = z3Optimizer.Check();
        if (status != Status.SATISFIABLE)
        {
            throw new InvalidOperationException("No solution found");
        }

        // Extract results and set values on variables
        var model = z3Optimizer.Model;

        foreach (var (variable, z3Var) in variableMap)
        {
            var value = model.Eval(z3Var, true);
            if (value is IntNum intNum)
            {
                variable.Value = intNum.Int64;
            }
            else
            {
                throw new InvalidOperationException($"Variable has non-integer value");
            }
        }
    }

    private static void CollectVariables(
        Expression expression,
        Dictionary<EquationVariable, IntExpr> variableMap,
        List<EquationVariable> variableList,
        Context context)
    {
        switch (expression)
        {
            case ConstantExpression constantExpr when constantExpr.Value is EquationVariable variable:
                if (!variableMap.ContainsKey(variable))
                {
                    var z3Var = context.MkIntConst($"var_{variableList.Count}");
                    variableMap[variable] = z3Var;
                    variableList.Add(variable);
                }
                break;

            case ConstantExpression constantExpr when constantExpr.Value is EquationVariable[] variables:
                foreach (var variable in variables)
                {
                    if (variable != null && !variableMap.ContainsKey(variable))
                    {
                        var z3Var = context.MkIntConst($"var_{variableList.Count}");
                        variableMap[variable] = z3Var;
                        variableList.Add(variable);
                    }
                }
                break;

            case MemberExpression memberExpr:
                // Try to evaluate member expression to see if it contains variables
                try
                {
                    var compiledGetter = Expression.Lambda<Func<object>>(
                        Expression.Convert(memberExpr, typeof(object))).Compile();
                    var value = compiledGetter();

                    if (value is EquationVariable variable && !variableMap.ContainsKey(variable))
                    {
                        var z3Var = context.MkIntConst($"var_{variableList.Count}");
                        variableMap[variable] = z3Var;
                        variableList.Add(variable);
                    }
                    else if (value is EquationVariable[] variables)
                    {
                        foreach (var v in variables)
                        {
                            if (v != null && !variableMap.ContainsKey(v))
                            {
                                var z3Var = context.MkIntConst($"var_{variableList.Count}");
                                variableMap[v] = z3Var;
                                variableList.Add(v);
                            }
                        }
                    }
                }
                catch
                {
                    // If we can't evaluate it, it's probably not a variable
                }
                break;

            case BinaryExpression binaryExpr:
                CollectVariables(binaryExpr.Left, variableMap, variableList, context);
                CollectVariables(binaryExpr.Right, variableMap, variableList, context);
                break;

            case MethodCallExpression methodCall:
                foreach (var arg in methodCall.Arguments)
                {
                    CollectVariables(arg, variableMap, variableList, context);
                }
                if (methodCall.Object != null)
                {
                    CollectVariables(methodCall.Object, variableMap, variableList, context);
                }
                break;
        }
    }

    private static BoolExpr TranslateExpression(
        Expression expression,
        Dictionary<EquationVariable, IntExpr> variableMap,
        Context context)
    {
        switch (expression)
        {
            case BinaryExpression { NodeType: ExpressionType.Equal } binaryExpr:
                var leftEq = TranslateArithmeticExpression(binaryExpr.Left, variableMap, context);
                var rightEq = TranslateArithmeticExpression(binaryExpr.Right, variableMap, context);
                return context.MkEq(leftEq, rightEq);

            case BinaryExpression { NodeType: ExpressionType.GreaterThan } binaryExpr:
                var leftGt = TranslateArithmeticExpression(binaryExpr.Left, variableMap, context);
                var rightGt = TranslateArithmeticExpression(binaryExpr.Right, variableMap, context);
                return context.MkGt(leftGt, rightGt);

            case BinaryExpression { NodeType: ExpressionType.GreaterThanOrEqual } binaryExpr:
                var leftGe = TranslateArithmeticExpression(binaryExpr.Left, variableMap, context);
                var rightGe = TranslateArithmeticExpression(binaryExpr.Right, variableMap, context);
                return context.MkGe(leftGe, rightGe);

            case BinaryExpression { NodeType: ExpressionType.LessThan } binaryExpr:
                var leftLt = TranslateArithmeticExpression(binaryExpr.Left, variableMap, context);
                var rightLt = TranslateArithmeticExpression(binaryExpr.Right, variableMap, context);
                return context.MkLt(leftLt, rightLt);

            case BinaryExpression { NodeType: ExpressionType.LessThanOrEqual } binaryExpr:
                var leftLe = TranslateArithmeticExpression(binaryExpr.Left, variableMap, context);
                var rightLe = TranslateArithmeticExpression(binaryExpr.Right, variableMap, context);
                return context.MkLe(leftLe, rightLe);

            default:
                throw new NotSupportedException($"Expression type {expression.NodeType} is not supported");
        }
    }

    private static ArithExpr TranslateOptimizationExpression(
        Expression expression,
        Dictionary<EquationVariable, IntExpr> variableMap,
        Context context)
    {
        switch (expression)
        {
            case MethodCallExpression methodCall when methodCall.Method.Name == "Sum":
                // Handle variables.Sum() - sum of all variables in the array
                var targetExpr = methodCall.Object ?? (methodCall.Arguments.Count > 0 ? methodCall.Arguments[0] : null);
                if (targetExpr != null)
                {
                    var variables = GetVariableArrayFromExpression(targetExpr);
                    if (variables != null)
                    {
                        var terms = variables
                            .Where(v => v != null)
                            .Select(v => variableMap[v])
                            .ToArray();
                        return terms.Length == 1 ? terms[0] : context.MkAdd(terms);
                    }
                }
                throw new NotSupportedException($"Could not extract variables from Sum() call: {expression}");

            default:
                // Fall back to arithmetic expression translation
                return TranslateArithmeticExpression(expression, variableMap, context);
        }
    }

    private static EquationVariable[]? GetVariableArrayFromExpression(Expression expression)
    {
        try
        {
            return expression switch
            {
                ConstantExpression { Value: EquationVariable[] vars } => vars,
                MemberExpression member => Expression.Lambda<Func<EquationVariable[]>>(member).Compile()(),
                _ => null
            };
        }
        catch
        {
            return null;
        }
    }

    private static ArithExpr TranslateArithmeticExpression(
        Expression expression,
        Dictionary<EquationVariable, IntExpr> variableMap,
        Context context)
    {
        switch (expression)
        {
            case ConstantExpression constantExpr when constantExpr.Value is EquationVariable variable:
                return variableMap[variable];

            case ConstantExpression constantExpr when constantExpr.Value is EquationConstant constant:
                return context.MkInt(constant.Value);

            case NewExpression newExpr when newExpr.Type == typeof(EquationConstant):
                // Handle new EquationConstant(value) expressions
                var compiledNew = Expression.Lambda<Func<EquationConstant>>(newExpr).Compile();
                var newConstant = compiledNew();
                return context.MkInt(newConstant.Value);

            case MemberExpression memberExpr:
                // Handle member access - could be a closure variable or static field  
                try
                {
                    var compiledGetter = Expression.Lambda<Func<object>>(
                        Expression.Convert(memberExpr, typeof(object))).Compile();
                    var value = compiledGetter();

                    if (value == null)
                    {
                        throw new NotSupportedException($"Member access '{memberExpr}' evaluated to null. Expression: {memberExpr}");
                    }

                    if (value is EquationVariable variable)
                    {
                        return variableMap[variable];
                    }

                    if (value is EquationConstant equationConstant)
                    {
                        return context.MkInt(equationConstant.Value);
                    }

                    // Arrays shouldn't reach here - they should be handled by op_Multiply
                    // This error means the expression tree structure isn't what we expected
                    throw new NotSupportedException(
                        $"Member access '{memberExpr}' returned unsupported type: {value.GetType().Name}. " +
                        $"Full member: {memberExpr}. " +
                        $"This suggests the expression tree structure is different than expected. " +
                        $"Arrays should only appear as arguments to op_Multiply.");
                }
                catch (Exception ex) when (ex is not NotSupportedException)
                {
                    throw new NotSupportedException($"Failed to evaluate member access '{memberExpr}': {ex.Message}", ex);
                }

            case BinaryExpression { NodeType: ExpressionType.Add } binaryExpr:
                var leftAdd = TranslateArithmeticExpression(binaryExpr.Left, variableMap, context);
                var rightAdd = TranslateArithmeticExpression(binaryExpr.Right, variableMap, context);
                return context.MkAdd(leftAdd, rightAdd);

            case BinaryExpression { NodeType: ExpressionType.Subtract } binaryExpr:
                var leftSub = TranslateArithmeticExpression(binaryExpr.Left, variableMap, context);
                var rightSub = TranslateArithmeticExpression(binaryExpr.Right, variableMap, context);
                return context.MkSub(leftSub, rightSub);

            case BinaryExpression { NodeType: ExpressionType.Multiply } binaryExpr:
                // Check if this is array multiplication (extension operator creates MethodBinaryExpression)
                var leftArg = binaryExpr.Left;
                var rightArg = binaryExpr.Right;

                // Helper to get array value from expression
                EquationVariable[]? GetVariableArray(Expression expr)
                {
                    try
                    {
                        return expr switch
                        {
                            ConstantExpression { Value: EquationVariable[] vars } => vars,
                            MemberExpression member => Expression.Lambda<Func<EquationVariable[]>>(member).Compile()(),
                            _ => null
                        };
                    }
                    catch
                    {
                        return null;
                    }
                }

                EquationConstant[]? GetConstantArray(Expression expr)
                {
                    try
                    {
                        return expr switch
                        {
                            ConstantExpression { Value: EquationConstant[] consts } => consts,
                            MemberExpression member => Expression.Lambda<Func<EquationConstant[]>>(member).Compile()(),
                            _ => null
                        };
                    }
                    catch
                    {
                        return null;
                    }
                }

                var leftVars = GetVariableArray(leftArg);
                var rightConsts = GetConstantArray(rightArg);

                // Check if this is array multiplication
                if (leftVars != null && rightConsts != null)
                {
                    // Element-wise multiply and sum: sum(leftVars[i] * rightConsts[i])
                    var terms = new List<ArithExpr>();
                    for (var i = 0; i < leftVars.Length; i++)
                    {
                        if (leftVars[i] != null)
                        {
                            var varExpr = variableMap[leftVars[i]];
                            var constExpr = context.MkInt(rightConsts[i].Value);
                            terms.Add(context.MkMul(varExpr, constExpr));
                        }
                    }
                    return terms.Count == 1 ? terms[0] : context.MkAdd(terms.ToArray());
                }

                // Regular scalar multiplication
                var leftMul = TranslateArithmeticExpression(leftArg, variableMap, context);
                var rightMul = TranslateArithmeticExpression(rightArg, variableMap, context);
                return context.MkMul(leftMul, rightMul);

            case MethodCallExpression methodCall when methodCall.Method.Name == "op_Multiply":
                // This case is not actually used - extension operators create BinaryExpression with NodeType.Multiply
                // Keeping it here for documentation purposes
                throw new NotSupportedException($"MethodCallExpression for op_Multiply should not occur - extension operators create BinaryExpression");

            default:
                throw new NotSupportedException($"Arithmetic expression type {expression.NodeType} is not supported");
        }
    }
}
