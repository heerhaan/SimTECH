using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    public class LeagueService
    {
        private readonly List<League> _leagues = new()
        {
            new League
            {
                Id = 1,
                Name = "Formula 0",
                State = State.Active
            }
        };

        public List<League> GetTestData()
        {
            return _leagues;
        }

        public void CreateLeague(League league)
        {
            _leagues.Add(league);
        }
    }
}
