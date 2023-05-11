using SimTECH.Data.Models;

namespace SimTECH.PageModels
{
    public class TraitAssigner
    {
        public TraitAssigner(Driver driver)
        {
            Entrant = Entrant.Driver;
        }
        public TraitAssigner(Team team)
        {
            Entrant = Entrant.Team;
        }
        public TraitAssigner(Track track)
        {
            Entrant = Entrant.Track;
        }

        public long Id { get; set; }

        public Entrant Entrant { get; set; }

        public List<long> ExistingTraitIds { get; set; } = new();
        public List<long> AssignedTraitIds { get; set; } = new();
    }
}
