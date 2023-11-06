namespace SimTECH
{
    public class SimConfig
    {
        public const string SectionKey = "SimConfig";

        public int CalculationDistance { get; set; } = 10;

        public int DisqualifyChance { get; set; } = 100;
        public int FatalityChance { get; set; } = 250;

        public int SafetyCarChance { get; set; } = 5;
        public int SafetyCarGap { get; set; } = 25;
        public int SafetyCarReturnChance { get; set; } = 2;

        public int BattleRng { get; set; } = 5;
        public int CarDriverStatusModifier { get; set; } = 4;
        public int MinimumTyreLife { get; set; } = -25;

        public double GapMarge { get; set; } = 0.08;

        public bool AllowMultiLeagueEntry { get; set; }
    }
}
