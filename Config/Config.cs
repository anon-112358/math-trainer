#pragma warning disable EF1002

using System.Reflection;
using System.Text;
using CliFx;
using CliFx.Binding;
using CliFx.Infrastructure;
using math_trainer.Misc;
using Microsoft.EntityFrameworkCore;

namespace math_trainer.Config
{
    [Command("config", Description = "Change program settings.")]
    public partial class Config : ICommand
    {
        public static ConfigData Data { get; set; } = Load();

        private static ConfigData Load()
        {
            using var db = Utils.DatabaseContext.SetupDb();

            ConfigData? data;

            try
            {
                data = db.ConfigData.FirstOrDefault() ?? new ConfigData();
            }
            catch
            {
                db.Database.ExecuteSqlRaw($"DROP TABLE IF EXISTS {typeof(ConfigData).Name}");
                Utils.DatabaseContext.SetupDb().Dispose();
                data = new ConfigData();
            }

            return data!;
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
                var prop = Data.GetType()
                    .GetProperty(
                        KeyToChange,
                        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance
                    );

                if (prop == null || !prop.IsDefined(typeof(Utils.SettingAttribute), true))
                    throw new CommandException(
                        "Invalid option. Valid options:\n"
                            + ConfigList.GetFormattedSettings(withStart: false, withValues: false)
                    );

                object? converted;
                try
                {
                    converted = Convert.ChangeType(ValToChange, prop.PropertyType);
                }
                catch
                {
                    throw new CommandException(
                        $"Invalid value for '{KeyToChange}'. Correct type: {prop.PropertyType.Name}"
                    );
                }

                prop.SetValue(Data, converted);

                using var db = Utils.DatabaseContext.SetupDb();
                db.ConfigData.RemoveRange(db.ConfigData);
                db.Add(Data);

                db.SaveChanges();

                Console.WriteLine($"Successfully changed '{KeyToChange}' to '{ValToChange}'.");

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
                    if (prop.IsDefined(typeof(Utils.SettingAttribute), true))
                        builder.AppendLine(
                            $"  - {prop.Name}" + (withValues ? $": {prop.GetValue(data)}" : "")
                        );
                }

                return builder.ToString();
            }
        }
    }
}
