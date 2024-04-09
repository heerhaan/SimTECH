namespace SimTECH.PageModels.Common;

public struct CarParts(int aero, int chassis, int powertrain)
{
    public int Aero { get; set; } = aero;
    public int Chassis { get; set; } = chassis;
    public int Powertrain { get; set; } = powertrain;

    public override readonly string ToString() => $"{Aero} / {Chassis} / {Powertrain}";
}
