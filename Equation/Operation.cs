using System.Numerics;

namespace math_trainer.Equation
{
    public record struct Operation(decimal A, Operator Op, decimal B) : INode
    {
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
            return $" {A} {OperatorExtensions.ToSymbol(Op)} {B} ";
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
