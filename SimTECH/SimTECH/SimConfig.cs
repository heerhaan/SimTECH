namespace SimTECH;

public class SimConfig
{
    public const string SectionKey = "SimConfig";

    public int CalculationDistance { get; set; } = 10;

    public int CautionPace { get; set; } = 50;

    public int CautionTyreWearDivider { get; set; } = 3;

    public double GapMarge { get; set; } = 0.08;
}
