namespace math_trainer.Equation
{
    public record struct Operation(decimal A, Operator Op, decimal B) : INode
    {
        // todo make lazy
        public readonly decimal Solve()
        {
            return Op switch
            {
                Operator.Plus => A + B,
                Operator.Minus => A - B,
                Operator.Times => A * B,
                Operator.DividedBy => A / B,
                _ => throw new Exception(),
            };
        }

        public override readonly string ToString()
        {
            var StrA = A.ToString();
            var StrB = B.ToString();
            var StrOp = Op.ToSymbol();

            if (B < 0 && Op == Operator.Minus)
            {
                StrB = Math.Abs(B).ToString();
                StrOp = Operator.Plus.ToSymbol();
            }
            else if (B < 0 && (Op == Operator.DividedBy || Op == Operator.Times))
            {
                StrB = $"({StrB})";
            }
            else if (B < 0 && (Op == Operator.Plus))
            {
                StrB = Math.Abs(B).ToString();
                StrOp = Operator.Minus.ToSymbol();
            }

            return $"[{Constants.NumMarkupColor}]{StrA}[/] "
                + $"[{Op.ToColor()}]{StrOp}[/] "
                + $"[{Constants.NumMarkupColor}]{StrB}[/]";
        }
    }
}
