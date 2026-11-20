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

            public override INode CreateEquation()
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

            [CommandOption(
                "divisible",
                'v',
                Description = "Allow only divisible pairs of numbers."
            )]
            public bool DivisiblesOnly { get; set; } = false;

            public override INode CreateEquation()
            {
                if (!DivisiblesOnly)
                    return base.CreateEquation();

                var a = 0m;
                var b = 0m;

                while ((b == 0 || a % b != 0))
                {
                    a = Utils.GetNum(MinVal, MaxVal + 1);
                    b = Utils.GetNum(MinVal, MaxVal + 1);
                }

                return new Operation(a, Op, b);
            }
        }
    }
}
