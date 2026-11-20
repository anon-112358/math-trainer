using CliFx.Binding;
using math_trainer.Equation;

namespace math_trainer
{
    public partial class SimplePractice
    {
        [Command("sub")]
        public partial class SubtractionPractice : AbstractPractice
        {
            public override Operator Op { get; } = Operator.Minus;
        }

        [Command("add")]
        public partial class AdditionPractice : AbstractPractice
        {
            public override Operator Op { get; } = Operator.Plus;
        }

        [Command("mul")]
        public partial class MultiplicationPractice : AbstractPractice
        {
            public override Operator Op { get; } = Operator.Times;
        }

        [Command("div")]
        public partial class DivisionPractice : AbstractPractice
        {
            public override Operator Op { get; } = Operator.DividedBy;
        }
    }
}
