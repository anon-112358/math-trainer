using System.Numerics;

namespace math_trainer.Equation
{
    public record struct Operation<T>(T A, Operator Op, T B) : INode
        where T : INumber<T>
    {
        public readonly T Solve()
        {
            T val =
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

        readonly Operation<decimal> INode.GetOperation()
        {
            return new Operation<decimal>(Convert.ToDecimal(A), Op, Convert.ToDecimal(B));
        }

        readonly bool INode.IsOperation()
        {
            return true;
        }
    }
}
