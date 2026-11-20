using CliFx;
using CliFx.Binding;
using CliFx.Infrastructure;
using math_trainer.Equation;
using Spectre.Console;
using static Spectre.Console.AnsiConsole;

namespace math_trainer
{
    [Command("add")]
    public partial class AdditionPractice : AbstractPractice
    {
        public override EquationBinaryBranch CreateEquation() { }

        public override decimal CalculateAnswer(EquationBinaryBranch equation) { }
    }
}
