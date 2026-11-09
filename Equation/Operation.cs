using System.Numerics;

namespace math_trainer.Equation
{
    public record struct Operation<T>(T A, Operator Op, T B)
        where T : INumber<T>
    {
        public readonly T Solve()
        {
            T? val =
                Op == Operator.Plus ? A + B
                : Op == Operator.Minus ? A - B
                : Op == Operator.Times ? A * B
                : Op == Operator.Divided ? A / B
                : default;
            if (val == default)
                throw new Exception();
            return val!;
        }
    }
}
