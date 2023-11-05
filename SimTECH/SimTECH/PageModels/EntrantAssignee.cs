using SimTECH.Common.Enums;
using SimTECH.Data.Models;

namespace SimTECH.PageModels
{
    public class EntrantAssignee
    {
        public EntrantAssignee(Driver driver)
        {
            Id = driver.Id;
            Name = driver.FullName;
            Country = driver.Country;
            Entrant = Entrant.Driver;

            if (driver.DriverTraits?.Any() == true)
            {
                foreach (var trait in driver.DriverTraits)
                    ExistingTraitIds.Add(trait.TraitId);
            }
        }
        public EntrantAssignee(Team team)
        {
            Id = team.Id;
            Name = team.Name;
            Country = team.Country;
            Entrant = Entrant.Team;

            if (team.TeamTraits?.Any() == true)
            {
                foreach (var trait in team.TeamTraits)
                    ExistingTraitIds.Add(trait.TraitId);
            }
        }
        public EntrantAssignee(Track track)
        {
            Id = track.Id;
            Name = track.Name;
            Country = track.Country;
            Entrant = Entrant.Track;

            if (track.TrackTraits?.Any() == true)
            {
                foreach (var trait in track.TrackTraits)
                    ExistingTraitIds.Add(trait.TraitId);
            }
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
        public Entrant Entrant { get; set; }

        public List<long> ExistingTraitIds { get; set; } = new();
        public List<long> AssignedTraitIds { get; set; } = new();
    }
}
