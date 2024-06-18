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
    MaintainPosition = 512,
}

// Add Formation Lap?
// FastestLap relatively unused?

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
        RacerEvent.MaintainPosition,
        RacerEvent.Death,
        RacerEvent.FastestLap,
    ];

    public static string GetShortText(this RacerEvent racerEvent)
    {
        return racerEvent switch
        {
            RacerEvent.DriverDnf => "Crash",
            RacerEvent.CarDnf => "Mechanical",
            RacerEvent.EngineDnf => "Engine",
            RacerEvent.Mistake => "Mistake",
            RacerEvent.Pitstop => "Pitstop",
            RacerEvent.Swap => "Swap",
            RacerEvent.MaintainPosition => "Stay",
            RacerEvent.Death => "Injury",
            RacerEvent.FastestLap => "Fastest",
            _ => "Huh?"
        };
    }

    public static string GetEventIcon(this RacerEvent racerEvent)
    {
        return racerEvent switch
        {
            RacerEvent.DriverDnf => IconCollection.HelmetOff,
            RacerEvent.CarDnf => IconCollection.CarCrash,
            RacerEvent.EngineDnf => IconCollection.EngineOff,
            RacerEvent.Mistake => Icons.Material.Filled.ErrorOutline,
            RacerEvent.Pitstop => Icons.Material.Filled.LocalGasStation,
            RacerEvent.Swap => Icons.Material.Filled.SwapVert,
            RacerEvent.MaintainPosition => Icons.Material.Filled.SwapVert,
            RacerEvent.Death => IconCollection.Skull,
            RacerEvent.FastestLap => Icons.Material.Filled.Timer,
            _ => Icons.Material.Filled.QuestionMark
        };
    }

    public static Color GetSignalColour(this RacerEvent racerEvent)
    {
        return racerEvent switch
        {
            RacerEvent.DriverDnf or RacerEvent.CarDnf or RacerEvent.EngineDnf => Color.Error,
            RacerEvent.Mistake => Color.Warning,
            RacerEvent.Pitstop => Color.Success,
            RacerEvent.Swap => Color.Info,
            RacerEvent.MaintainPosition => Color.Warning,
            RacerEvent.Death => Color.Inherit,
            RacerEvent.FastestLap => Color.Primary,
            _ => Color.Default,
        };
    }
}
