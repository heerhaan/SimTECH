using SimTECH.Common.Enums;
using SimTECH.Extensions;

namespace SimTECH.Data.Models;

public sealed class LapScore : ModelBase
{
    public int Order { get; set; }
    public int Score { get; set; }
    public string? TyreColour { get; set; }
    public RacerEvent RacerEvents { get; set; }

    public long ResultId { get; set; }
    public Result Result { get; set; }
}

public static class ExtendLapScore
{
    public static IEnumerable<RacerEvent> ListOfDubiousEvents(this LapScore lapScore)
    {
        // TODO: For now also exclude fastest lap, as it looks like it only happens on the opening lap really
        //       So make sure its correct and such
        return lapScore.RacerEvents
            .GetFlagged()
            .Select(e => (e as RacerEvent?).GetValueOrDefault())
            .Where(e => e != RacerEvent.Unknown
                && e != RacerEvent.Racing
                && e != RacerEvent.FastestLap)
            .ToList();
    }

    public static bool HasDnfed(this LapScore lapScore) =>
               lapScore.RacerEvents.HasFlag(RacerEvent.DriverDnf)
            || lapScore.RacerEvents.HasFlag(RacerEvent.CarDnf)
            || lapScore.RacerEvents.HasFlag(RacerEvent.EngineDnf);
}
