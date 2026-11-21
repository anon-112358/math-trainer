using System.ComponentModel;
using CliFx.Binding;
using math_trainer.Equation;
using math_trainer.Misc;

namespace math_trainer.Practice
{
    public partial class SimplePractice
    {
        [Command("add", Description = "Practice addition.")]
        public partial class AdditionPractice : AbstractPractice
        {
            public override Operator Op { get; } = Operator.Plus;
            public override int MinVal { get; set; } = -10;
            public override int MaxVal { get; set; } = 50;
        }

        [Command("sub", Description = "Practice subtraction.")]
        public partial class SubtractionPractice : AbstractPractice
        {
            public override Operator Op { get; } = Operator.Minus;
            public override int MinVal { get; set; } = -50;
            public override int MaxVal { get; set; } = 99;
        }

        [Command("mul", Description = "Practice multiplication.")]
        public partial class MultiplicationPractice : AbstractPractice
        {
            public override Operator Op { get; } = Operator.Times;
            public override decimal Margin { get; set; } = 0.05m;
            public override int MinVal { get; set; } = -20;
            public override int MaxVal { get; set; } = 50;
        }

        [Command("div", Description = "Practice divisions.")]
        public partial class DivisionPractice : AbstractPractice
        {
            public override Operator Op { get; } = Operator.DividedBy;
            public override decimal Margin { get; set; } = 0.05m;
            public override int MinVal { get; set; } = 0;
            public override int MaxVal { get; set; } = 100;

            [CommandOption(
                "no_divisibility",
                'd',
                Description = "Allow non-divisible pairs of numbers."
            )]
            public bool AreUndivisiblesAllowed { get; set; } = false;

            [CommandOption("trivial", 't', Description = "Allow trivial answers.")]
            public bool AreTrivialNumbersAllowed { get; set; } = false;

            public override Operation CreateEquation()
            {
                if (AreUndivisiblesAllowed)
                    return base.CreateEquation();

                bool baseCondition(decimal a, decimal b) => (b == 0m || a % b != 0m);

                bool nonTrivialCondition(decimal a, decimal b)
                {
                    decimal[] trivialAnswers = [a, -a, 0m, 1m, -1m];
                    var answer = a / (b == 0 ? 1 : b);
                    return baseCondition(a, b) || trivialAnswers.Contains(answer);
                }

                bool ConstrainCondition(decimal a, decimal b)
                {
                    var answer = new Operation(a, Op, b).Solve();
                    var result = MaxAnswer == null || MaxAnswer >= answer;
                    return !(result && (MinAnswer == null || MinAnswer <= answer));
                }

                Func<decimal, decimal, bool> Condition = AreTrivialNumbersAllowed
                    ? baseCondition
                    : nonTrivialCondition;

                var _condition = Condition;
                Condition = (a, b) => _condition(a, b) || ConstrainCondition(a, b);

                var (a, b) = Utils.FindNumbersThatAreFalse(
                    Condition,
                    () => Utils.GetNum(MinVal, MaxVal + 1)
                );

                return new Operation(a, Op, b);
            }
        }
    }
}
