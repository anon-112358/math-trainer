namespace math_trainer.Equation
{
    public enum Operator
    {
        // MDAS
        Times = 0,
        Divided = 1,
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
                Operator.Divided => '/',
                Operator.Minus => '-',
                Operator.Plus => '+',
                _ => op.ToString().ToCharArray()[0],
            };
        }
    }
}
