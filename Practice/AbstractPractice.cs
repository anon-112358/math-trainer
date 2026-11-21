using System.Globalization;
using CliFx;
using CliFx.Binding;
using CliFx.Infrastructure;
using math_trainer.Equation;
using math_trainer.Misc;
using Spectre.Console;
using static Spectre.Console.AnsiConsole;

namespace math_trainer.Practice
{
    public abstract partial class AbstractPractice : ICommand
    {
        [CommandOption("questions", 'q', Description = "Number of questions.")]
        public virtual int? NumQ { get; set; } = null;

        // todo implement
        [CommandOption("decimals", 'c', Description = "Whether or not to include decimals.")]
        public virtual bool IsDoubles { get; set; } = false;

        [CommandOption("error", 'e', Description = "Allowed margin of error.")]
        public virtual decimal Margin { get; set; } = 0m;

        [CommandOption("min", 'i', Description = "Minimum value of elements.")]
        public abstract int MinVal { get; set; }

        [CommandOption("max", 'a', Description = "Maximum value of elements.")]
        public abstract int MaxVal { get; set; }

        // todo implement
        [CommandOption("minA", 'f', Description = "Minimum value of elements.")]
        public virtual int MinA { get; set; }

        // todo implement
        [CommandOption("maxA", 'g', Description = "Maximum value of elements.")]
        public virtual int MaxA { get; set; }

        // todo implement
        [CommandOption("minB", 'l', Description = "Minimum value of elements.")]
        public virtual int MinB { get; set; }

        // todo implement
        [CommandOption("maxB", 'm', Description = "Maximum value of elements.")]
        public virtual int MaxB { get; set; }

        [CommandOption("min_answer", 'n', Description = "Minimum value of answer.")]
        public virtual int? MinAnswer { get; set; } = null;

        [CommandOption("max_answer", 'x', Description = "Maximum value of answer.")]
        public virtual int? MaxAnswer { get; set; } = null;

        public ValueTask ExecuteAsync(IConsole console)
        {
            Utils.SetupConsole(Practice);
            return default;
        }

        public abstract Operator Op { get; }

        public virtual Operation CreateEquation()
        {
            var (a, b) = ConstrainAnswer();
            return new Operation(a, Op, b);
        }

        private Tuple<decimal, decimal> ConstrainAnswer()
        {
            if (MinAnswer == null && MaxAnswer == null)
                return new(Utils.GetNum(MinVal, MaxVal + 1), Utils.GetNum(MinVal, MaxVal + 1));
            else
                return Utils.FindNumbersThatAreFalse(
                    boolFactory: (a, b) =>
                    {
                        if (Op == Operator.DividedBy && b == 0)
                            return true;
                        var answer = new Operation(a, Op, b).Solve();
                        return (
                            // i like it ok
                            !(MaxAnswer == null && MinAnswer == null)
                            && !(MinAnswer == null && answer <= MaxAnswer)
                            && !(answer >= MinAnswer && MaxAnswer == null)
                            && !(answer >= MinAnswer && answer <= MaxAnswer)
                            && (
                                (MinAnswer == null && answer > MaxAnswer)
                                || (answer >= MinAnswer && answer > MaxAnswer)
                                || (answer < MinAnswer && MaxAnswer == null)
                                || (answer < MinAnswer && answer <= MaxAnswer)
                                || (answer < MinAnswer && answer > MaxAnswer)
                            )
                        );
                    },
                    decimalFactory: () => Utils.GetNum(MinVal, MaxVal + 1)
                );
        }

        public virtual bool IsAnswerCorrect(decimal correct, decimal? user)
        {
            if (Margin == 0m)
                return correct == user;
            else
            {
                var diff = correct - user;
                return diff < Margin && diff > -Margin;
            }
        }

        public void Practice()
        {
            for (int i = 0; NumQ == null || (i < NumQ); i++)
            {
                var equation = CreateEquation();
                var answer = equation.Solve();
                string question = equation.ToString();

                List<char> keysPressed = [];

                while (true)
                {
                    Clear();
                    Utils.PrintToTopRight($"#{i + 1} ", "#6C7086");
                    MarkupLine(question + '\n');
                    AnsiConsole.Write(String.Join("", keysPressed));

                    var press = System.Console.ReadKey(intercept: true);

                    decimal userAnswer = decimal.MaxValue / 2;
                    if (
                        Utils.Numbers.Contains(press.KeyChar)
                        && (
                            Decimal.TryParse(
                                new String([.. keysPressed]) + press.KeyChar,
                                CultureInfo.InvariantCulture,
                                out userAnswer
                            )
                            || press.KeyChar == '-'
                        )
                    )
                        keysPressed.Add(press.KeyChar);
                    else if (press.KeyChar == '\b' && keysPressed.Count > 0)
                    {
                        keysPressed.RemoveAt(keysPressed.Count - 1);
                    }
                    else if (
                        press.Modifiers == ConsoleModifiers.Shift
                        && (press.KeyChar == 'Q' || press.KeyChar == 'q')
                    )
                        return;
                    else if (
                        press.Modifiers == ConsoleModifiers.None
                        && (press.KeyChar == 'Q' || press.KeyChar == 'q')
                    )
                        break;

                    if (IsAnswerCorrect(answer, userAnswer))
                        break;
                }
            }
        }
    }
}
