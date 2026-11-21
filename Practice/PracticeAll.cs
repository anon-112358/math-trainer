using CliFx.Binding;
using math_trainer.Equation;
using math_trainer.Misc;
using static math_trainer.Practice.SimplePractice;

namespace math_trainer.Practice
{
    [Command("all", Description = "Practice all types of basic operations.")]
    public partial class PracticeAll : AbstractPractice
    {
        public override int? MinVal { get; set; } = null;
        public override int? MaxVal { get; set; } = null;
        public override Operator? Op { get; } = null;

        // from simplepractice division
        [CommandOption(
            "no_divisibility",
            'd',
            Description = "Allow non-divisible pairs of numbers in division mode."
        )]
        public bool AreUndivisiblesAllowed { get; set; } = false;

        [CommandOption("trivial", 't', Description = "Allow trivial division answers.")]
        public bool AreTrivialNumbersAllowed { get; set; } = false;

        // specific to here
        [CommandOption("random", 'r', Description = "Have the type of equation change randomly.")]
        public bool IsRandom { get; set; } = false;

        private int index = 0;
        private AbstractPractice[]? instances;

        AbstractPractice[] GetInstances()
        {
            AbstractPractice[] practiceInstances =
            [
                new AdditionPractice(),
                new SubtractionPractice(),
                new MultiplicationPractice(),
                new DivisionPractice(),
            ];
            List<AbstractPractice> product = [];
            foreach (var chosenIns in practiceInstances)
            {
                var chosenType = chosenIns.GetType();
                var thisProperties = this.GetType().GetProperties();
                var chosenProperties = chosenType.GetProperties();
                foreach (var thisProp in thisProperties)
                {
                    var prop = chosenProperties.FirstOrDefault(p =>
                        p.Name == thisProp.Name && p.PropertyType == thisProp.PropertyType
                    );
                    if (prop == null || !prop.CanWrite || thisProp.GetValue(this) == null)
                        continue;
                    if (
                        thisProp.IsDefined(typeof(NoOverwriteAttribute), true)
                        || prop.IsDefined(typeof(NoOverwriteAttribute), true)
                    )
                        continue;

                    prop.SetValue(chosenIns, thisProp.GetValue(this));
                }
                product.Add(chosenIns);
            }
            return [.. product];
        }

        public override Operation CreateEquation()
        {
            instances ??= GetInstances();
            return instances[
                IsRandom ? Utils.GetNum(0, instances.Length) : index++ % instances.Length
            ]
                .CreateEquation();
        }
    }
}
