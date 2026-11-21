#pragma warning disable IDE1006

using static math_trainer.Misc.Utils;

namespace math_trainer.Config
{
    public record class ConfigData()
    {
        public int Id { get; set; }

        [Setting]
        public string test { get; set; } = "";

        [Setting]
        public decimal testNum { get; set; } = 0;
        // todo add settings for all defaults/magic numbers
    }
}
#pragma warning restore IDE1006
