using SimTECH.Common.Enums;
using SimTECH.Extensions;

namespace SimTECH.Data.Models
{
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
        public static IEnumerable<Enum> ListOfDubiousEvents(this LapScore lapScore) =>
            lapScore.RacerEvents.GetFlagged().Where(e => e.ToString() != "Unknown" && e.ToString() != "Racing");

        public static bool HasDnfed(this LapScore lapScore) =>
                   lapScore.RacerEvents.HasFlag(RacerEvent.DriverDnf)
                || lapScore.RacerEvents.HasFlag(RacerEvent.CarDnf)
                || lapScore.RacerEvents.HasFlag(RacerEvent.EngineDnf);
    }
}
