using static math_trainer.Misc.Utils;

namespace math_trainer.Config
{
    public record class ConfigData()
    {
        public int Id { get; set; }

        // div

        [Setting]
        public int? DivisionNumQ { get; set; } = null;

        [Setting]
        public bool DivisionIsDoubles { get; set; } = false;

        [Setting]
        public decimal DivisionMargin { get; set; } = 0.05m;

        [Setting]
        public int DivisionMinVal { get; set; } = 0;

        [Setting]
        public int? DivisionMaxVal { get; set; } = 100;

        [Setting]
        public int? DivisionMinA { get; set; } = null;

        [Setting]
        public int? DivisionMaxA { get; set; } = null;

        [Setting]
        public int? DivisionMinB { get; set; } = null;

        [Setting]
        public int? DivisionMaxB { get; set; } = null;

        [Setting]
        public int? DivisionMinAnswer { get; set; } = null;

        [Setting]
        public int? DivisionMaxAnswer { get; set; } = null;

        [Setting]
        public bool DivisionAreUndivisiblesAllowed { get; set; } = false;

        [Setting]
        public bool DivisionAreTrivialNumbersAllowed { get; set; } = false;

        // mult

        [Setting]
        public int? MultiplicationNumQ { get; set; } = null;

        [Setting]
        public bool MultiplicationIsDoubles { get; set; } = false;

        [Setting]
        public decimal MultiplicationMargin { get; set; } = 0m;

        [Setting]
        public int? MultiplicationMinVal { get; set; } = -20;

        [Setting]
        public int? MultiplicationMaxVal { get; set; } = 50;

        [Setting]
        public int? MultiplicationMinA { get; set; } = null;

        [Setting]
        public int? MultiplicationMaxA { get; set; } = null;

        [Setting]
        public int? MultiplicationMinB { get; set; } = null;

        [Setting]
        public int? MultiplicationMaxB { get; set; } = null;

        [Setting]
        public int? MultiplicationMinAnswer { get; set; } = null;

        [Setting]
        public int? MultiplicationMaxAnswer { get; set; } = null;

        // add

        [Setting]
        public int? AdditionNumQ { get; set; } = null;

        [Setting]
        public bool AdditionIsDoubles { get; set; } = false;

        [Setting]
        public decimal AdditionMargin { get; set; } = 0m;

        [Setting]
        public int? AdditionMinVal { get; set; } = -10;

        [Setting]
        public int? AdditionMaxVal { get; set; } = 50;

        [Setting]
        public int? AdditionMinA { get; set; } = null;

        [Setting]
        public int? AdditionMaxA { get; set; } = null;

        [Setting]
        public int? AdditionMinB { get; set; } = null;

        [Setting]
        public int? AdditionMaxB { get; set; } = null;

        [Setting]
        public int? AdditionMinAnswer { get; set; } = null;

        [Setting]
        public int? AdditionMaxAnswer { get; set; } = null;

        // sub

        [Setting]
        public int? SubtractionNumQ { get; set; } = null;

        [Setting]
        public bool SubtractionIsDoubles { get; set; } = false;

        [Setting]
        public decimal SubtractionMargin { get; set; } = 0m;

        [Setting]
        public int? SubtractionMinVal { get; set; } = -50;

        [Setting]
        public int? SubtractionMaxVal { get; set; } = 99;

        [Setting]
        public int? SubtractionMinA { get; set; } = null;

        [Setting]
        public int? SubtractionMaxA { get; set; } = null;

        [Setting]
        public int? SubtractionMinB { get; set; } = null;

        [Setting]
        public int? SubtractionMaxB { get; set; } = null;

        [Setting]
        public int? SubtractionMinAnswer { get; set; } = null;

        [Setting]
        public int? SubtractionMaxAnswer { get; set; } = null;
    }
}
