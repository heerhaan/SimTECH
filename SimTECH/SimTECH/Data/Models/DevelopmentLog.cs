using SimTECH.Common.Enums;

namespace SimTECH.Data.Models
{
    public sealed class DevelopmentLog : ModelBase
    {
        public long EntrantId { get; set; }
        public Entrant EntrantGroup { get; set; }
        public Aspect DevelopedAspect { get; set; }

        public int Initial { get; set; }
        public int Change { get; set; }

        public int AfterRound { get; set; }

        public long SeasonId { get; set; }
        public Season Season { get; set; }
    }
}
