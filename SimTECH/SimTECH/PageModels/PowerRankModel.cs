namespace SimTECH.PageModels
{
    public class PowerRankModel
    {
        public string DriverName { get; set; }
        public int DriverNumber { get; set; }
        public Country Nationality { get; set; }

        public string TeamName { get; set; }
        public string Colour { get; set; } = "var(--mud-palette-background)";
        public string Accent { get; set; } = "var(--mud-palette-text-primary)";
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
        public int WearMax { get; set; }
        public int WearMin { get; set; }

        public TraitEffect? TraitEffect { get; set; }// if we still want to display this somehow in the PWR
    }
}
