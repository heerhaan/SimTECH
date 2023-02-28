namespace SimTECH.Data.Models
{
    public class Team
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string Biography { get; set; } = default!;
        public State State { get; set; }

        //public IList<Contract> Contracts { get; set; }
        public IList<SeasonTeam> SeasonTeams { get; set; } = null!;
        public IList<TeamTrait> TeamTraits { get; set; } = null!;
    }
}
