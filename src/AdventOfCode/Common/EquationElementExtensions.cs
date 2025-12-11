namespace AdventOfCode.Common;

public static class EquationElementExtensions
{
    extension(IEquationElement[] self)
    {
        public static IEquationElement operator *(IEquationElement[] left, IEquationElement[] right)
        {
            throw new NotImplementedException("This operator is only for expression tree analysis");
        }

        public IEquationElement Sum()
        {
            throw new NotImplementedException("This method is only for expression tree analysis");
        }
    }

    extension(IEquationElement)
    {
        public static bool operator >(IEquationElement left, IEquationElement right)
        {
            throw new NotImplementedException("This operator is only for expression tree analysis");
        }

        public static bool operator <(IEquationElement left, IEquationElement right)
        {
            throw new NotImplementedException("This operator is only for expression tree analysis");
        }

        public static bool operator >=(IEquationElement left, IEquationElement right)
        {
            throw new NotImplementedException("This operator is only for expression tree analysis");
        }

        public static bool operator <=(IEquationElement left, IEquationElement right)
        {
            throw new NotImplementedException("This operator is only for expression tree analysis");
        }

        public static IEquationElement operator *(IEquationElement left, IEquationElement right)
        {
            throw new NotImplementedException("This operator is only for expression tree analysis");
        }

        public static IEquationElement operator +(IEquationElement left, IEquationElement right)
        {
            throw new NotImplementedException("This operator is only for expression tree analysis");
        }

        public static IEquationElement operator -(IEquationElement left, IEquationElement right)
        {
            throw new NotImplementedException("This operator is only for expression tree analysis");
        }
    }
}
