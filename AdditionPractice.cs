using CliFx;
using CliFx.Binding;
using CliFx.Infrastructure;
using math_trainer.Equation;
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
                // temporary testing code
                var tree = new EquationBinaryBranch(
                    new EquationBinaryBranch(
                        new EquationBinaryBranch(
                            new EquationBinaryBranch(
                                new Operation<decimal>(19, Operator.Plus, 2),
                                new EquationBinaryBranch(
                                    new Operation<decimal>(4, Operator.Minus, 12),
                                    new Operation<decimal>(1, Operator.Plus, 1),
                                    Operator.Times
                                ),
                                Operator.Divided
                            ),
                            new EquationBinaryBranch(
                                new EquationBinaryBranch(
                                    new Operation<decimal>(3, Operator.Plus, 6),
                                    new EquationBinaryBranch(
                                        new Operation<decimal>(17, Operator.Minus, 16),
                                        new EquationBinaryBranch(
                                            new Operation<decimal>(18, Operator.Plus, 15),
                                            new EquationBinaryBranch(
                                                new Operation<decimal>(20, Operator.Plus, 3),
                                                new Operation<decimal>(9, Operator.Plus, 18),
                                                Operator.Times
                                            ),
                                            Operator.Divided
                                        ),
                                        Operator.Times
                                    ),
                                    Operator.Times
                                ),
                                new EquationBinaryBranch(
                                    new Operation<decimal>(7, Operator.Plus, 15),
                                    new EquationBinaryBranch(
                                        new Operation<decimal>(17, Operator.Plus, 3),
                                        new Operation<decimal>(11, Operator.Divided, 5),
                                        Operator.Divided
                                    ),
                                    Operator.Minus
                                ),
                                Operator.Plus
                            ),
                            Operator.Plus
                        ),
                        new EquationBinaryBranch(
                            new EquationBinaryBranch(
                                new EquationBinaryBranch(
                                    new EquationBinaryBranch(
                                        new Operation<decimal>(3, Operator.Minus, 18),
                                        new Operation<decimal>(5, Operator.Times, 1),
                                        Operator.Plus
                                    ),
                                    new EquationBinaryBranch(
                                        new Operation<decimal>(10, Operator.Plus, 15),
                                        new EquationBinaryBranch(
                                            new EquationBinaryBranch(
                                                new Operation<decimal>(9, Operator.Minus, 2),
                                                new Operation<decimal>(9, Operator.Times, 5),
                                                Operator.Minus
                                            ),
                                            new Operation<decimal>(15, Operator.Divided, 2),
                                            Operator.Times
                                        ),
                                        Operator.Divided
                                    ),
                                    Operator.Times
                                ),
                                new EquationBinaryBranch(
                                    new EquationBinaryBranch(
                                        new Operation<decimal>(16, Operator.Divided, 2),
                                        new Operation<decimal>(20, Operator.Divided, 6),
                                        Operator.Divided
                                    ),
                                    new EquationBinaryBranch(
                                        new Operation<decimal>(10, Operator.Plus, 9),
                                        new Operation<decimal>(6, Operator.Divided, 14),
                                        Operator.Plus
                                    ),
                                    Operator.Plus
                                ),
                                Operator.Divided
                            ),
                            new EquationBinaryBranch(
                                new EquationBinaryBranch(
                                    new EquationBinaryBranch(
                                        new EquationBinaryBranch(
                                            new Operation<decimal>(7, Operator.Plus, 12),
                                            new Operation<decimal>(6, Operator.Times, 19),
                                            Operator.Minus
                                        ),
                                        new EquationBinaryBranch(
                                            new Operation<decimal>(11, Operator.Plus, 15),
                                            new Operation<decimal>(3, Operator.Minus, 13),
                                            Operator.Plus
                                        ),
                                        Operator.Times
                                    ),
                                    new EquationBinaryBranch(
                                        new Operation<decimal>(2, Operator.Plus, 8),
                                        new Operation<decimal>(11, Operator.Divided, 10),
                                        Operator.Plus
                                    ),
                                    Operator.Times
                                ),
                                new EquationBinaryBranch(
                                    new Operation<decimal>(14, Operator.Plus, 7),
                                    new EquationBinaryBranch(
                                        new Operation<decimal>(13, Operator.Divided, 7),
                                        new Operation<decimal>(17, Operator.Times, 8),
                                        Operator.Minus
                                    ),
                                    Operator.Divided
                                ),
                                Operator.Minus
                            ),
                            Operator.Minus
                        ),
                        Operator.Times
                    ),
                    new EquationBinaryBranch(
                        new EquationBinaryBranch(
                            new EquationBinaryBranch(
                                new Operation<decimal>(16, Operator.Divided, 18),
                                new EquationBinaryBranch(
                                    new Operation<decimal>(11, Operator.Minus, 5),
                                    new Operation<decimal>(2, Operator.Minus, 20),
                                    Operator.Minus
                                ),
                                Operator.Plus
                            ),
                            new EquationBinaryBranch(
                                new EquationBinaryBranch(
                                    new Operation<decimal>(6, Operator.Plus, 1),
                                    new Operation<decimal>(3, Operator.Minus, 5),
                                    Operator.Plus
                                ),
                                new Operation<decimal>(14, Operator.Times, 13),
                                Operator.Minus
                            ),
                            Operator.Divided
                        ),
                        new EquationBinaryBranch(
                            new EquationBinaryBranch(
                                new EquationBinaryBranch(
                                    new EquationBinaryBranch(
                                        new Operation<decimal>(14, Operator.Minus, 11),
                                        new Operation<decimal>(18, Operator.Times, 3),
                                        Operator.Times
                                    ),
                                    new EquationBinaryBranch(
                                        new Operation<decimal>(14, Operator.Times, 13),
                                        new Operation<decimal>(17, Operator.Divided, 4),
                                        Operator.Times
                                    ),
                                    Operator.Minus
                                ),
                                new EquationBinaryBranch(
                                    new Operation<decimal>(10, Operator.Divided, 19),
                                    new Operation<decimal>(2, Operator.Divided, 12),
                                    Operator.Minus
                                ),
                                Operator.Divided
                            ),
                            new EquationBinaryBranch(
                                new EquationBinaryBranch(
                                    new EquationBinaryBranch(
                                        new Operation<decimal>(10, Operator.Plus, 1),
                                        new Operation<decimal>(16, Operator.Times, 13),
                                        Operator.Divided
                                    ),
                                    new Operation<decimal>(16, Operator.Divided, 6),
                                    Operator.Divided
                                ),
                                new EquationBinaryBranch(
                                    new Operation<decimal>(10, Operator.Times, 19),
                                    new EquationBinaryBranch(
                                        new EquationBinaryBranch(
                                            new Operation<decimal>(7, Operator.Divided, 6),
                                            new Operation<decimal>(16, Operator.Divided, 1),
                                            Operator.Plus
                                        ),
                                        new EquationBinaryBranch(
                                            new Operation<decimal>(1, Operator.Divided, 12),
                                            new Operation<decimal>(3, Operator.Times, 5),
                                            Operator.Times
                                        ),
                                        Operator.Times
                                    ),
                                    Operator.Times
                                ),
                                Operator.Times
                            ),
                            Operator.Times
                        ),
                        Operator.Minus
                    ),
                    Operator.Times
                );
                MarkupLine(EquationBinaryBranch.Format(tree).ToString());
                Thread.Sleep(5000);
                Environment.Exit(0);

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
