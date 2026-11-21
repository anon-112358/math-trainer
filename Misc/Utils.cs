using Spectre.Console;

namespace math_trainer.Misc
{
    public static class Utils
    {
        private static readonly Random Gen = new();
        private static string TopRightStr = "";

        public static int GetNum(int min, int max)
        {
            return Gen.Next(min, max);
        }

        public static void SaveCursor(Action action)
        {
            Console.Write("\x1b[s");
            action();
            Console.Write("\x1b[u");
        }

        public static void PrintToTopRight(string str, string color = "default")
        {
            TopRightStr = str;
            SaveCursor(() =>
            {
                Console.SetCursorPosition(Console.WindowWidth - str.Length, 0);
                AnsiConsole.Markup($"[{color}]{Markup.Escape(str)}[/]");
            });
        }

        public static void ClearTopRight()
        {
            SaveCursor(() =>
            {
                Console.SetCursorPosition(Console.WindowWidth - TopRightStr.Length, 0);
                Console.Write(new string(' ', TopRightStr.Length));
            });
            TopRightStr = "";
        }

        public static void SetupConsole(Action program)
        {
            bool hasBuffer = AnsiConsole.Profile.Capabilities.AlternateBuffer;

            bool hasRestored = false;
            void RestoreTerminal()
            {
                if (hasRestored)
                    return;

                hasRestored = true;
                if (hasBuffer)
                    Console.Write("\x1b[?1049l");

                Console.Write("\x1b[?25h");
            }

            AppDomain.CurrentDomain.ProcessExit += (_, _) => RestoreTerminal();
            Console.CancelKeyPress += (_, _) => RestoreTerminal();

            try
            {
                if (hasBuffer)
                {
                    Console.Write("\x1b[?1049h");
                    AnsiConsole.Clear();
                }

                program();
            }
            finally
            {
                RestoreTerminal();
            }
        }

        public static Tuple<decimal, decimal> FindNumbersThatAreFalse(
            Func<decimal, decimal, bool> boolFactory,
            Func<decimal> decimalFactory
        )
        {
            var tryCount = 0;

            var a = decimalFactory();
            var b = decimalFactory();

            while (boolFactory(a, b))
            {
                a = decimalFactory();
                b = decimalFactory();

                if (tryCount >= 20000)
                    throw new InvalidOperationException("No valid answers found for params.");

                tryCount += 1;
            }

            return new(a, b);
        }

        public static readonly HashSet<char> Numbers =
        [
            '1',
            '2',
            '3',
            '4',
            '5',
            '6',
            '7',
            '8',
            '9',
            '0',
            '.',
            '-',
        ];
    }
}
