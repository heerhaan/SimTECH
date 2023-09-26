using SimTECH.Constants;

namespace SimTECH.PageModels.SeasonModels
{
    public class PowerRankingItem
    {
        public string DriverName { get; set; }
        public int DriverNumber { get; set; }
        public Country Nationality { get; set; }

        public string TeamName { get; set; }
        public string Colour { get; set; } = Generals.DefaultColour;
        public string Accent { get; set; } = Generals.DefaultAccent;

        public string Engine { get; set; }

        public string Manufacturer { get; set; }

        public int QualyPower { get; set; }
        public int RacePower { get; set; }
        public int Aero { get; set; }
        public int Chassis { get; set; }
        public int Powertrain { get; set; }
        public int DriverRel { get; set; }
        public int CarRel { get; set; }
        public int EngineRel { get; set; }
        public int LifeBonus { get; set; }
        public int WearMin { get; set; }
        public int WearMax { get; set; }
        public int RngMin { get; set; }
        public int RngMax { get; set; }

        public double CarPartAvg { get; set; }
        public double RelAvg { get; set; }
    }
}
