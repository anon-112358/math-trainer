using CliFx;
using CliFx.Binding;
using CliFx.Infrastructure;
using Spectre.Console;

namespace math_trainer
{
    [Command("add")]
    public partial class AdditionPractice : ICommand
    {
        [CommandParameter(0, Description = "Number of questions")]
        public double NumQ { get; set; } = 20;

        public ValueTask ExecuteAsync(IConsole console)
        {
            Utils.SetupConsole(Practice);
            return default;
        }

        public static void Practice() { }
    }
}
