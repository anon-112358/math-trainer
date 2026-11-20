using System.ComponentModel;
using CliFx.Binding;
using math_trainer.Equation;

namespace math_trainer
{
    public partial class SimplePractice
    {
        [Command("add", Description = "Practice addition.")]
        public partial class AdditionPractice : AbstractPractice
        {
            public override Operator Op { get; } = Operator.Plus;
        }

        [Command("sub", Description = "Practice subtraction.")]
        public partial class SubtractionPractice : AbstractPractice
        {
            public override Operator Op { get; } = Operator.Minus;

            public override Operation CreateEquation()
            {
                var b = Utils.GetNum(MinVal, MaxVal + 1);
                b = b < 0 ? 0 : b;
                return new Operation(Utils.GetNum(MinVal, MaxVal + 1), Op, b);
            }
        }

        [Command("mul", Description = "Practice multiplication.")]
        public partial class MultiplicationPractice : AbstractPractice
        {
            public override Operator Op { get; } = Operator.Times;
            public override decimal Margin { get; set; } = 0.05m;
        }

        [Command("div", Description = "Practice divisions.")]
        public partial class DivisionPractice : AbstractPractice
        {
            public override Operator Op { get; } = Operator.DividedBy;
            public override decimal Margin { get; set; } = 0.05m;
            readonly decimal[] trivialAnswers = [0m, 1m, -1m];

            [CommandOption("divisible", 'v', Description = "Allow non-divisible pairs of numbers.")]
            public bool AreUndivisblesAllowed { get; set; } = false;

            [CommandOption("trivial", 't', Description = "Allow trivial answers.")]
            public bool AreTrivialNumbersAllowed { get; set; } = false;

            public override Operation CreateEquation()
            {
                var tryCount = 0;
                if (AreUndivisblesAllowed)
                    return base.CreateEquation();

                var a = 0m;
                var b = 0m;

                bool baseCondition(decimal a, decimal b) => (b == 0m || a % b != 0m);

                bool nonTrivialCondition(decimal a, decimal b)
                {
                    decimal[] _trivialAnswers = [a, b, -a, -b];
                    var answer = a / (b == 0 ? 1 : b);
                    return baseCondition(a, b)
                        || trivialAnswers.Contains(answer)
                        || _trivialAnswers.Contains(answer);
                }

                Func<decimal, decimal, bool> Condition = AreTrivialNumbersAllowed
                    ? baseCondition
                    : nonTrivialCondition;

                while (Condition(a, b))
                {
                    a = Utils.GetNum(MinVal, MaxVal + 1);
                    b = Utils.GetNum(MinVal, MaxVal + 1);

                    if (tryCount >= 1000)
                        throw new InvalidOperationException("No valid answers found for params.");

                    tryCount += 1;
                }

                return new Operation(a, Op, b);
            }
        }
    }
}
