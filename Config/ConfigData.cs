#pragma warning disable IDE1006

namespace math_trainer.Config
{
    public record class ConfigData()
    {
        public int Id { get; set; }

        public string? test { get; set; }
        // todo add settings for all defaults/magic numbers
    }
}
#pragma warning restore IDE1006
