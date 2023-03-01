using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    public class TeamService
    {
        public IEnumerable<Team> GetTestNames()
        {
            return new List<Team>
            {
                new Team()
                {
                    Id = 1,
                    Name = "Ferrari",
                    Country = Country.IT,
                    Biography = "grande!",
                    State = State.Active
                },
                new Team()
                {
                    Id = 2,
                    Name = "Williams",
                    Country = Country.GB,
                    State = State.Active
                },
                new Team()
                {
                    Id = 3,
                    Name = "Jerigo",
                    Country = Country.IL,
                    State = State.Active
                },
            };
        }
    }
}
