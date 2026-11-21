using System.Text;
using CliFx;
using CliFx.Binding;
using CliFx.Infrastructure;
using math_trainer.Misc;

namespace math_trainer.Config
{
    [Command("config", Description = "Change program settings.")]
    public partial class Config : ICommand
    {
        public static ConfigData Data { get; set; } = Load();

        private static ConfigData Load()
        {
            using var db = Utils.DatabaseContext.SetupDb();
            return db.ConfigData.FirstOrDefault() ?? new ConfigData();
        }

        [Command("config set", Description = "Set a setting.")]
        public partial class Set : ICommand
        {
            [CommandParameter(0, Name = "Key", Description = "Setting to change")]
            public required string KeyToChange { get; set; }

            [CommandParameter(1, Name = "Value", Description = "Value to assign to key")]
            public required string ValToChange { get; set; }

            public ValueTask ExecuteAsync(IConsole _)
            {
                var prop =
                    Data.GetType().GetProperty(KeyToChange)
                    ?? throw new CommandException(
                        "Invalid option. Valid options:\n"
                            + ConfigList.GetFormattedSettings(withStart: false, withValues: false)
                    );

                prop.SetValue(Data, ValToChange);

                using var db = Utils.DatabaseContext.SetupDb();
                db.ConfigData.RemoveRange(db.ConfigData);
                db.Add(Data);

                Console.WriteLine($"Successfully changed '{KeyToChange}' to '{ValToChange}'.");

                db.SaveChanges();

                return default;
            }
        }

        public ValueTask ExecuteAsync(IConsole _)
        {
            throw new CommandException("Please select a command.", showHelp: true);
        }

        [Command("config list", Description = "List settings.")]
        public partial class ConfigList : ICommand
        {
            public ValueTask ExecuteAsync(IConsole _)
            {
                Console.WriteLine(GetFormattedSettings());
                return default;
            }

            public static string GetFormattedSettings(bool withStart = true, bool withValues = true)
            {
                var data = Data;
                var dataType = data.GetType();
                var builder = new StringBuilder();

                if (withStart)
                    builder.AppendLine("Values:");

                foreach (var prop in dataType.GetProperties())
                {
                    if (prop.Name != "Id")
                        builder.AppendLine(
                            $"  - {prop.Name}" + (withValues ? $": {prop.GetValue(data)}" : "")
                        );
                }

                return builder.ToString();
            }
        }
    }
}
