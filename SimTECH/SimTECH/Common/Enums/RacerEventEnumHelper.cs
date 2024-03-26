using MudBlazor;
using SimTECH.Constants;

namespace SimTECH.Common.Enums;

[Flags]
public enum RacerEvent
{
    Unknown = 0,
    Racing = 1,
    DriverDnf = 2,
    CarDnf = 4,
    EngineDnf = 8,
    Mistake = 16,
    Pitstop = 32,
    Swap = 64,
    Death = 128,
    FastestLap = 256,
}

public static class RacerEventEnumHelper
{
    public static readonly RacerEvent[] RacerEvents =
    [
        RacerEvent.Racing,
        RacerEvent.DriverDnf,
        RacerEvent.CarDnf,
        RacerEvent.EngineDnf,
        RacerEvent.Mistake,
        RacerEvent.Pitstop,
        RacerEvent.Swap,
        RacerEvent.Death,
        RacerEvent.FastestLap,
    ];

    public static string RacerEventIcon(this RacerEvent racerEvent) => racerEvent switch
    {
        RacerEvent.DriverDnf => IconCollection.HelmetOff,
        RacerEvent.CarDnf => IconCollection.CarCrash,
        RacerEvent.EngineDnf => IconCollection.EngineOff,
        RacerEvent.Mistake => Icons.Material.Filled.ErrorOutline,
        RacerEvent.Pitstop => Icons.Material.Filled.LocalGasStation,
        RacerEvent.Swap => Icons.Material.Filled.SwapVert,
        RacerEvent.Death => IconCollection.Skull,
        RacerEvent.FastestLap => Icons.Material.Filled.Timer,
        _ => Icons.Material.Filled.QuestionMark
    };
}
