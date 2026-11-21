using math_trainer.Misc;

namespace math_trainer.Equation
{
    public enum Operator
    {
        // [PE]MDAS
        Times = 0,
        DividedBy = 1,
        Minus = 2,
        Plus = 3,
    }

    public static class OperatorExtensions
    {
        public static string ToSymbol(this Operator op)
        {
            return op switch
            {
                Operator.Times => "*",
                Operator.DividedBy => "/",
                Operator.Minus => "-",
                Operator.Plus => "+",
                _ => op.ToString(),
            };
        }

        public static Operator ToOpposite(this Operator op)
        {
            return op switch
            {
                Operator.Times => Operator.DividedBy,
                Operator.DividedBy => Operator.Times,
                Operator.Minus => Operator.Plus,
                Operator.Plus => Operator.Minus,
                _ => throw new Exception(),
            };
        }

        public static string ToColor(this Operator op)
        {
            return op switch
            {
                Operator.Plus => Constants.AddMarkupColor,
                Operator.Minus => Constants.SubMarkupColor,
                Operator.Times => Constants.MulMarkupColor,
                Operator.DividedBy => Constants.DivMarkupColor,
                _ => "#FFFFFF",
            };
        }
    }
}
