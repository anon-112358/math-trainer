using Spectre.Console;

namespace math_trainer
{
    public static class Utils
    {
        private static readonly Random Gen = new();

        public static int GetNum(int min, int max)
        {
            return Gen.Next(min, max);
        }

        public static double GetNum(int min, int max, int cases)
        {
            return Convert.ToDouble((Gen.Next(min, max) + Gen.NextDouble()).ToString($"C{cases}"));
        }

        public static void SetupConsole(Action program)
        {
            AnsiConsole.Cursor.Hide();
            if (AnsiConsole.Profile.Capabilities.AlternateBuffer)
            {
                try
                {
                    Console.Write("\x1b[?1049h");
                    AnsiConsole.Clear();
                    program();
                }
                finally
                {
                    Console.Write("\x1b[?1049l");
                }
            }
            else
            {
                program();
            }
            AnsiConsole.Cursor.Show();
        }
    }
}
