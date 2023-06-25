using SimTECH.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimTECH.Data.Models
{
    public sealed class LapScore
    {
        public long Id { get; set; }
        public int Order { get; set; }
        public int Score { get; set; }
        public RacerEvent RacerEvents { get; set; }

        public long ResultId { get; set; }
        public Result Result { get; set; }

        [NotMapped]
        public string TyreColour { get; set; }
    }

    public static class ExtendLapScore
    {
        public static IEnumerable<Enum> ListOfDubiousEvents(this LapScore lapScore) =>
            lapScore.RacerEvents.GetFlagged().Where(e => e.ToString() != "Unknown" && e.ToString() != "Racing" && e.ToString() != "Caution");

        public static bool HasDnfed(this LapScore lapScore) =>
                lapScore.RacerEvents.HasFlag(RacerEvent.DriverDnf)
                || lapScore.RacerEvents.HasFlag(RacerEvent.CarDnf)
                || lapScore.RacerEvents.HasFlag(RacerEvent.EngineDnf);

        public static string DetermineLapColour(this LapScore lapScore)
        {
            if (lapScore.HasDnfed())
                return "red";

            if (lapScore.RacerEvents.HasFlag(RacerEvent.Caution))
                return "yellow";

            return "green";
        }
    }
}
