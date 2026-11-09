using System.Numerics;
using CliFx;
using CliFx.Binding;
using CliFx.Infrastructure;
using Spectre.Console;
using static Spectre.Console.AnsiConsole;

namespace math_trainer
{
    [Command("add")]
    public partial class AdditionPractice : ICommand
    {
        [CommandOption("questions", 'q', Description = "Number of questions.")]
        public int NumQ { get; set; } = 5;

        [CommandOption("min", 'i', Description = "Minimum value of elements.")]
        public int MinVal { get; set; } = -10;

        [CommandOption("max", 'a', Description = "Maximum value of elements.")]
        public int MaxVal { get; set; } = 30;

        [CommandOption("decimals", 'd', Description = "Include decimals.")]
        public bool IsDoubles { get; set; } = false;

        public ValueTask ExecuteAsync(IConsole console)
        {
            Utils.SetupConsole(Practice);
            return default;
        }

        public void Practice()
        {
            foreach (var i in Enumerable.Range(0, NumQ))
            {
                decimal numA = Utils.GetNum(MinVal, MaxVal + 1);
                decimal numB = Utils.GetNum(MinVal, MaxVal + 1);

                if (IsDoubles)
                {
                    numA += Utils.GetNum(0, 100) / 100m;
                    numB += Utils.GetNum(0, 100) / 100m;
                }

                var answer = numA + numB;

                string question =
                    $"[#CDD6F4]{(numA):0.##}[/][#FAB387] + [/][#CDD6F4]{(numB):0.##}[/]";

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
