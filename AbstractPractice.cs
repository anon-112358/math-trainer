using CliFx;
using CliFx.Binding;
using CliFx.Infrastructure;
using math_trainer.Equation;
using Spectre.Console;
using static Spectre.Console.AnsiConsole;

namespace math_trainer
{
    public abstract partial class AbstractPractice : ICommand
    {
        [CommandOption("questions", 'q', Description = "Number of questions.")]
        public int? NumQ { get; set; } = null;

        [CommandOption("min", 'i', Description = "Minimum value of elements.")]
        public int MinVal { get; set; } = -10;

        [CommandOption("max", 'a', Description = "Maximum value of elements.")]
        public int MaxVal { get; set; } = 30;

        [CommandOption("decimals", 'd', Description = "Whether or not to include decimals.")]
        public bool IsDoubles { get; set; } = false;

        public ValueTask ExecuteAsync(IConsole console)
        {
            Utils.SetupConsole(Practice);
            return default;
        }

        public abstract decimal CalculateAnswer(EquationBinaryBranch equation);
        public abstract EquationBinaryBranch CreateEquation();

        public void Practice()
        {
            for (int i = 0; NumQ == null || (i < NumQ); i++)
            {
                var equation = CreateEquation();
                var answer = CalculateAnswer(equation);
                string question = equation.ToString();
                //    $"[#CDD6F4]{(numA):0.##}[/][#FAB387] + [/][#CDD6F4]{(numB):0.##}[/]";

                List<char> keysPressed = [];

                while (true)
                {
                    Clear();
                    Utils.PrintToTopRight($"#{i + 1} ", "#6C7086");
                    MarkupLine(question + '\n');
                    AnsiConsole.Write(String.Join("", keysPressed));

                    var press = System.Console.ReadKey().KeyChar;

                    if (Utils.Numbers.Contains(press))
                        keysPressed.Add(press);
                    else if (press == '\b' && keysPressed.Count > 0)
                    {
                        keysPressed.RemoveAt(keysPressed.Count - 1);
                    }

                    decimal? userAnswer = null;

                    try
                    {
                        userAnswer = Decimal.Parse(new String([.. keysPressed]));
                    }
                    catch { }

                    if (answer == userAnswer)
                        break;
                }
            }
        }
    }
}
