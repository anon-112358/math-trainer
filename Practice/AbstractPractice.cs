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
        [AttributeUsage(AttributeTargets.Property)]
        public class NoOverwriteAttribute : Attribute { }

        [NoOverwrite]
        public abstract Operator? Op { get; }

        [CommandOption("questions", 'q', Description = "Number of questions.")]
        public virtual int? NumQ { get; set; } = null;

        [CommandOption("decimals", 'c', Description = "Whether or not to include decimals.")]
        public virtual bool IsDoubles { get; set; } = false;

        [CommandOption("error", 'e', Description = "Allowed margin of error.")]
        public virtual decimal Margin { get; set; } = 0m;

        [CommandOption("min", 'i', Description = "Minimum value of elements.")]
        public abstract int? MinVal { get; set; }

        [CommandOption("max", 'a', Description = "Maximum value of elements.")]
        public abstract int? MaxVal { get; set; }

        [CommandOption("minA", 'f', Description = "Minimum value of the first element.")]
        public virtual int? MinA { get; set; } = null;

        [CommandOption("maxA", 'g', Description = "Maximum value of the first element.")]
        public virtual int? MaxA { get; set; } = null;

        [CommandOption("minB", 'l', Description = "Minimum value of the second element.")]
        public virtual int? MinB { get; set; } = null;

        [CommandOption("maxB", 'm', Description = "Maximum value of the second element.")]
        public virtual int? MaxB { get; set; } = null;

        [CommandOption("min_answer", 'n', Description = "Minimum value of answer.")]
        public virtual int? MinAnswer { get; set; } = null;

        [CommandOption("max_answer", 'x', Description = "Maximum value of answer.")]
        public virtual int? MaxAnswer { get; set; } = null;

        public ValueTask ExecuteAsync(IConsole console)
        {
            Utils.SetupConsole(Practice);
            return default;
        }

        public virtual Operation CreateEquation()
        {
            var (a, b) = ConstrainAnswer();
            return new Operation(a, Op ?? throw new InvalidDataException(), b);
        }

        private Tuple<decimal, decimal> ConstrainAnswer()
        {
            if (MinVal == null || MaxVal == null)
                throw new InvalidDataException();

            int min = (int)MinVal;
            int max = (int)MaxVal;

            if (MinAnswer == null && MaxAnswer == null)
                return new(Utils.GetNum(min, max + 1), Utils.GetNum(min, max + 1));
            else
                return Utils.FindNumbersThatAreFalse(
                    boolFactory: (a, b) =>
                    {
                        if (Op == Operator.DividedBy && b == 0)
                            return true;
                        if (a < MinA || b < MinB || a > MaxA || b > MaxB)
                            return true;
                        var answer = new Operation(
                            a,
                            Op ?? throw new InvalidDataException(),
                            b
                        ).Solve();
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
                    decimalFactory: () =>
                        Utils.GetNum(min, max + 1)
                        + (IsDoubles ? (decimal)Utils.Gen.NextDouble() : 0m)
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

                    var isSuccess = Decimal.TryParse(
                        s: new String(value: [.. keysPressed]) + press.KeyChar,
                        provider: CultureInfo.InvariantCulture,
                        result: out decimal userAnswer
                    );
                    if (
                        Utils.Numbers.Contains(press.KeyChar)
                        && (isSuccess || (press.KeyChar == '-' && keysPressed.Count == 0))
                    )
                        keysPressed.Add(press.KeyChar);
                    else if (press.KeyChar == '\b' && keysPressed.Count > 0)
                    {
                        keysPressed.RemoveAt(keysPressed.Count - 1);
                    }
                    else if ((press.KeyChar == 'Q' || press.KeyChar == 'q'))
                    {
                        if (press.Modifiers == ConsoleModifiers.Shift)
                            return;
                        else if (
                            press.Modifiers == ConsoleModifiers.None
                            && (press.KeyChar == 'Q' || press.KeyChar == 'q')
                        )
                            break;
                    }
                    if (isSuccess && IsAnswerCorrect(answer, userAnswer))
                        break;
                }
            }
        }
    }
}
