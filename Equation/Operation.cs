namespace math_trainer.Equation
{
    public record struct Operation(decimal A, Operator Op, decimal B) : INode
    {
        // todo make lazy
        public readonly decimal Solve()
        {
            decimal val =
                Op == Operator.Plus ? A + B
                : Op == Operator.Minus ? A - B
                : Op == Operator.Times ? A * B
                : A / B;
            return val;
        }

        public override readonly string ToString()
        {
            return $"[{Constants.NumMarkupColor}]{A}[/]"
                + $" [{Op.ToColor()}]{Op.ToSymbol()}[/]"
                + $" [{Constants.NumMarkupColor}]{B}[/]";
        }

        readonly EquationBinaryBranch INode.GetBranch()
        {
            throw new Exception();
        }

        readonly Operation INode.GetOperation()
        {
            return new Operation(Convert.ToDecimal(A), Op, Convert.ToDecimal(B));
        }

        readonly bool INode.IsOperation()
        {
            return true;
        }
    }
}
