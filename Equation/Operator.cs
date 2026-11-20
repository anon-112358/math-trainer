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
        public static char ToSymbol(this Operator op)
        {
            return op switch
            {
                Operator.Times => '*',
                Operator.DividedBy => '/',
                Operator.Minus => '-',
                Operator.Plus => '+',
                _ => op.ToString().ToCharArray()[0],
            };
        }

        public static string ToColor(this Operator op)
        {
            return new Dictionary<Operator, string>()
            {
                [Operator.Plus] = Constants.AddMarkupColor,
                [Operator.Minus] = Constants.SubMarkupColor,
                [Operator.Times] = Constants.MulMarkupColor,
                [Operator.DividedBy] = Constants.DivMarkupColor,
            }[op];
        }
    }
}
