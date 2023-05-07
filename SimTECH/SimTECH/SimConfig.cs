namespace SimTECH
{
    public class SimConfig
    {
        public const string SectionKey = "SimConfig";

        public int FatalityChance { get; set; } = 250;
        public int DisqualifyChance { get; set; } = 100;
        public double GapMarge { get; set; } = 0.05;
        public bool PersonalNumbers { get; set; } = false;

        // MistakeAmountRolls refers to the consequent rolls that need to be done before a driver makes a mistake, mistakes are accounted for in each stint for each driver
        public int MistakeAmountRolls { get; set; } = 1;
        public int MistakeLowerValue { get; set; } = -200;
        public int MistakeUpperValue { get; set; } = -50;

        // Applies to the upper RNG value of a stint
        public int RainAdditionalRNG { get; set; } = 10;
        public int StormAdditionalRNG { get; set; } = 20;
        public int RainDriverReliabilityModifier { get; set; } = -2;
        public int StormDriverReliabilityModifier { get; set; } = -4;

        public double SunnyEngineMultiplier { get; set; } = 0.9;
        public double OvercastEngineMultiplier { get; set; } = 1.1;
        public double WetEngineMultiplier { get; set; } = 0.75;

        // Pace modifier a #1 driver gets and a #2 driver loses
        public int CarDriverStatusModifier { get; set; } = 4;
    }
}
