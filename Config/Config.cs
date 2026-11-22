#pragma warning disable EF1002, IDE1006

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
    public partial class ConfigCommand : ICommand
    {
        private static Lazy<ConfigData> _data { get; set; } = new(Load());
        public static ConfigData Data { get; set; } = _data.Value;

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
                            + ConfigList.GetFormattedSettings(
                                withExtraStuff: false,
                                withValues: false
                            )
                    );

                object? converted = InputConverter(ValToChange, prop.PropertyType);
                bool isStringNull = false;

                if (
                    String.Equals(
                        ValToChange.ToLower(),
                        "null",
                        StringComparison.InvariantCultureIgnoreCase
                    ) && Utils.IsNullable(prop.PropertyType)
                )
                {
                    isStringNull = true;
                    converted = null;
                }

                if (converted == null && !isStringNull)
                    throw new CommandException(
                        $"Invalid value for '{KeyToChange}'. Correct type:"
                            + $" {(Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType).Name}"
                    );

                prop.SetValue(Data, converted);

                using var db = Utils.DatabaseContext.SetupDb();
                db.Update(Data);

                db.SaveChanges();

                Console.WriteLine($"Successfully changed '{KeyToChange}' to '{ValToChange}'.");

                return default;
            }
        }

        private static object? InputConverter(string input, Type outputType)
        {
            Type[] numberTypes =
            [
                typeof(int),
                typeof(Nullable<int>),
                typeof(decimal),
                typeof(Nullable<decimal>),
            ];
            Type[] safeTypes = [typeof(String)];

            string[] trueyStrings = ["yes", "y", "true", "t", "1", "on", "enable", "enabled"];
            string[] falseyStrings = ["no", "n", "false", "f", "0", "off", "disable", "disabled"];

            var toCheck = input.Trim().ToLower();

            try
            {
                if (numberTypes.Contains(outputType))
                {
                    Type numType = Nullable.GetUnderlyingType(outputType) ?? outputType;
                    return Convert.ChangeType(input, numType);
                }
                else if (outputType == typeof(bool) || outputType == typeof(Nullable<bool>))
                    return trueyStrings.Contains(toCheck) ? true
                        : falseyStrings.Contains(toCheck) ? false
                        : null;
                else if (safeTypes.Contains(outputType))
                    return Convert.ChangeType(input, outputType);
                else
                    return null;
            }
            catch (Exception ex) when (ex is FormatException or OverflowException)
            {
                return null;
            }
        }

        public ValueTask ExecuteAsync(IConsole _)
        {
            throw new CommandException("Please select a command.", showHelp: true);
        }

        [Command("config list", Description = "List settings.")]
        public partial class ConfigList : ICommand
        {
            [CommandOption(
                "search",
                's',
                Description = "Only include entries that include the input to this."
            )]
            public string UserQuery { get; set; } = "";

            public ValueTask ExecuteAsync(IConsole _)
            {
                Console.WriteLine(GetFormattedSettings(query: UserQuery));
                return default;
            }

            public static string GetFormattedSettings(
                bool withExtraStuff = true,
                bool withValues = true,
                string query = ""
            )
            {
                var data = Data;
                var dataType = data.GetType();
                var builder = new StringBuilder();

                if (withExtraStuff)
                    builder.AppendLine("Values:");

                foreach (var prop in dataType.GetProperties())
                {
                    if (
                        prop.IsDefined(typeof(Utils.SettingAttribute), true)
                        && (
                            query == ""
                            || prop.Name.Contains(query, StringComparison.OrdinalIgnoreCase)
                        )
                    )
                        builder.AppendLine(
                            $"  - {prop.Name}"
                                + (Utils.IsNullable(prop.PropertyType) ? " (nullable)" : "")
                                + (withValues ? $": {prop.GetValue(data) ?? "null"}" : "")
                        );
                }
                if (withExtraStuff)
                    builder.AppendLine(
                        "Tip: You can set nullable values to null by typing null into the value field."
                    );

                return builder.ToString();
            }
        }
    }
}
