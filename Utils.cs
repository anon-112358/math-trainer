using Spectre.Console;

namespace math_trainer
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

        public static decimal PowerOf(decimal num, int power)
        {
            for (int i = 0; i < power; i++)
            {
                num *= num;
            }
            return num;
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
            void RestoreTerminal()
            {
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
