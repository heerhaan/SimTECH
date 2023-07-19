namespace SimTECH
{
    public class SimConfig
    {
        public const string SectionKey = "SimConfig";

        public int CalculationDistance { get; set; } = 10;

        public int DisqualifyChance { get; set; } = 100;
        public int FatalityChance { get; set; } = 250;

        public int SafetyCarChance { get; set; } = 3;
        public int SafetyCarGap { get; set; } = 25;
        public int SafetyCarReturnChance { get; set; } = 2;
        public int SafetyPitstopSubtracter { get; set; } = 200;

        public int MistakeAmountRolls { get; set; } = 1;
        public int MistakeLowerValue { get; set; } = 150;
        public int MistakeUpperValue { get; set; } = 300;

        public int BattleRng { get; set; } = 5;
        public int CarDriverStatusModifier { get; set; } = 4;
        public int MinimumTyreLife { get; set; } = -25;

        public double GapMarge { get; set; } = 0.04;

        public bool PersonalNumbersEnabled { get; set; }
        public bool TooMuchInfo { get; set; }
        public bool AllowMultiLeagueEntry { get; set; }
        //public bool FormationLapEnabled { get; set; }
    }
}
