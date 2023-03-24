using SimTECH.Data.Models;

namespace SimTECH.PageModels
{
    public class RaceModel
    {
        public string Name { get; set; }
        public Country Country { get; set; }
        public Weather Weather { get; set; }

        public int AmountRuns { get; set; }

        public List<RaceDriver> RaceDrivers { get; set; }
    }

    public class RaceDriver
    {
        public string FullName { get; set; }
        public int Number { get; set; }
        public TeamRole Role { get; set; }
        public Country Nationality { get; set; }
        public string TeamName { get; set; }
        public string Colour { get; set; }
        public string Accent { get; set; }

        public int Power { get; set; }

        public Result Result { get; set; }//overwegend weg ermee?

        public TraitResultEffect TraitEffect { get; set; }
        //score? position?
        public List<int> RunResults { get; set; } = new();
        public int[] RunVals { get; set; }

        // Properties underneath are optionable, depending on circumstances
        public int Position { get; set; }
        public int Score { get; set; }

        public int MaxScore => RunVals.Max();
    }
}
