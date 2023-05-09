namespace SimTECH
{
    public class SimConfig
    {
        public const string SectionKey = "SimConfig";

        public int FatalityChance { get; set; } = 250;
        public int DisqualifyChance { get; set; } = 100;
        public double GapMarge { get; set; } = 0.05;
        public bool PersonalNumbers { get; set; }

        // MistakeAmountRolls refers to the consequent rolls that need to be done before a driver makes a mistake, mistakes are accounted for in each stint for each driver
        public int MistakeAmountRolls { get; set; } = 1;
        public int MistakeLowerValue { get; set; } = -300;
        public int MistakeUpperValue { get; set; } = -150;

        // Pace modifier a #1 driver gets and a #2 driver loses
        public int CarDriverStatusModifier { get; set; } = 4;

        public int CalculationDistance { get; set; } = 10;
    }
}
