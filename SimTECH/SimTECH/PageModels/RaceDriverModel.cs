using SimTECH.Data.Models;

namespace SimTECH.PageModels
{
    public abstract class DriverBase
    {
        public long ResultId { get; set; }

        public string FullName { get; set; }
        public int Number { get; set; }
        public TeamRole Role { get; set; }
        public Country Nationality { get; set; }

        public string TeamName { get; set; }
        public string Colour { get; set; }
        public string Accent { get; set; }

        public int Power { get; set; }

        public TraitEffect TraitEffect { get; set; }
    }

    public class RaceDriver
    {
        public long ResultId { get; set; }
        public string FullName { get; set; }
        public int Number { get; set; }
        public TeamRole Role { get; set; }
        public Country Nationality { get; set; }
        public string TeamName { get; set; }
        public string Colour { get; set; }
        public string Accent { get; set; }

        public int Power { get; set; }

        public Result Result { get; set; }//overwegend weg ermee?

        public TraitEffect TraitEffect { get; set; }
        //score? position?
        public int[] RunValues { get; set; }

        // Properties underneath are optionable, depending on circumstances
        public int Position { get; set; }
        public int Score { get; set; }

        public int MaxScore => RunValues.Max();
    }

    public class PracticeDriver : DriverBase
    {

    }

    public class QualifyingDriver : DriverBase
    {

    }
}
