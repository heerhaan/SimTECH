using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    public class TeamService
    {
        private readonly List<Team> _teams = new()
        {
            new Team()
            {
                Id = 1,
                Name = "Test",
                Country = Country.BB,
                Biography = "I barely exist",
                State = State.Concept
            }
        };

        public List<Team> GetTestNames()
        {
            return _teams;
        }

        public void CreateTeam(Team team)
        {
            _teams.Add(team);
        }
    }
}
