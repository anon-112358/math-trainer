using CliFx.Binding;
using math_trainer.Equation;
using math_trainer.Misc;
using static math_trainer.Config.ConfigCommand;

namespace math_trainer.Practice
{
    public partial class SimplePractice
    {
        [Command("add", Description = "Practice addition.")]
        public partial class AdditionPractice : AbstractPractice
        {
            public override Operator? Op { get; } = Operator.Plus;
            public override int? NumQ { get; set; } = Data.AdditionNumQ;
            public override bool IsDoubles { get; set; } = Data.AdditionIsDoubles;
            public override decimal Margin { get; set; } = Data.AdditionMargin;
            public override int? MinVal { get; set; } = Data.AdditionMinVal;
            public override int? MaxVal { get; set; } = Data.AdditionMaxVal;
            public override int? MinA { get; set; } = Data.AdditionMinA;
            public override int? MaxA { get; set; } = Data.AdditionMaxA;
            public override int? MinB { get; set; } = Data.AdditionMinB;
            public override int? MaxB { get; set; } = Data.AdditionMaxB;
            public override int? MinAnswer { get; set; } = Data.AdditionMinAnswer;
            public override int? MaxAnswer { get; set; } = Data.AdditionMaxAnswer;
        }

        [Command("sub", Description = "Practice subtraction.")]
        public partial class SubtractionPractice : AbstractPractice
        {
            public override Operator? Op { get; } = Operator.Minus;
            public override int? NumQ { get; set; } = Data.SubtractionNumQ;
            public override bool IsDoubles { get; set; } = Data.SubtractionIsDoubles;
            public override decimal Margin { get; set; } = Data.SubtractionMargin;
            public override int? MinVal { get; set; } = Data.SubtractionMinVal;
            public override int? MaxVal { get; set; } = Data.SubtractionMaxVal;
            public override int? MinA { get; set; } = Data.SubtractionMinA;
            public override int? MaxA { get; set; } = Data.SubtractionMaxA;
            public override int? MinB { get; set; } = Data.SubtractionMinB;
            public override int? MaxB { get; set; } = Data.SubtractionMaxB;
            public override int? MinAnswer { get; set; } = Data.SubtractionMinAnswer;
            public override int? MaxAnswer { get; set; } = Data.SubtractionMaxAnswer;
        }

        [Command("mul", Description = "Practice multiplication.")]
        public partial class MultiplicationPractice : AbstractPractice
        {
            public override Operator? Op { get; } = Operator.Times;
            public override int? NumQ { get; set; } = Data.MultiplicationNumQ;
            public override bool IsDoubles { get; set; } = Data.MultiplicationIsDoubles;
            public override decimal Margin { get; set; } = Data.MultiplicationMargin;
            public override int? MinVal { get; set; } = Data.MultiplicationMinVal;
            public override int? MaxVal { get; set; } = Data.MultiplicationMaxVal;
            public override int? MinA { get; set; } = Data.MultiplicationMinA;
            public override int? MaxA { get; set; } = Data.MultiplicationMaxA;
            public override int? MinB { get; set; } = Data.MultiplicationMinB;
            public override int? MaxB { get; set; } = Data.MultiplicationMaxB;
            public override int? MinAnswer { get; set; } = Data.MultiplicationMinAnswer;
            public override int? MaxAnswer { get; set; } = Data.MultiplicationMaxAnswer;
        }

        [Command("div", Description = "Practice divisions.")]
        public partial class DivisionPractice : AbstractPractice
        {
            public override Operator? Op { get; } = Operator.DividedBy;
            public override int? NumQ { get; set; } = Data.DivisionNumQ;
            public override bool IsDoubles { get; set; } = Data.DivisionIsDoubles;
            public override decimal Margin { get; set; } = Data.DivisionMargin;
            public override int? MinVal { get; set; } = Data.DivisionMinVal;
            public override int? MaxVal { get; set; } = Data.DivisionMaxVal;
            public override int? MinA { get; set; } = Data.DivisionMinA;
            public override int? MaxA { get; set; } = Data.DivisionMaxA;
            public override int? MinB { get; set; } = Data.DivisionMinB;
            public override int? MaxB { get; set; } = Data.DivisionMaxB;
            public override int? MinAnswer { get; set; } = Data.DivisionMinAnswer;
            public override int? MaxAnswer { get; set; } = Data.DivisionMaxAnswer;

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
                if (MinVal == null || MaxVal == null)
                    throw new InvalidDataException();

                int min = (int)MinVal;
                int max = (int)MaxVal;

                bool divisibilityCondition(decimal a, decimal b) => (b == 0m || a % b != 0m);

                bool nonTrivialityCondition(decimal a, decimal b)
                {
                    decimal[] trivialAnswers = [a, -a, 0m, 1m, -1m];
                    var answer = a / (b == 0 ? 1 : b);
                    return trivialAnswers.Contains(answer);
                }

                bool rangeCondition(decimal a, decimal b)
                {
                    var answer = new Operation(
                        a,
                        Op ?? throw new InvalidDataException(),
                        b
                    ).Solve();
                    var result = MaxAnswer == null || MaxAnswer >= answer;
                    if (a < MinA || b < MinB || a > MaxA || b > MaxB)
                        return true;
                    return !(result && (MinAnswer == null || MinAnswer <= answer));
                }
                bool combinedCondition(decimal a, decimal b) =>
                    (!AreUndivisiblesAllowed && divisibilityCondition(a, b))
                    || (!AreTrivialNumbersAllowed && nonTrivialityCondition(a, b))
                    || rangeCondition(a, b);

                var (a, b) = Utils.FindNumbersThatAreFalse(
                    combinedCondition,
                    () => Utils.GetNum(min, max + 1)
                );

                return new Operation(a, Op ?? throw new InvalidDataException(), b);
            }
        }
    }
}
