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
                    Name = "Ferrari",
                    Country = "IT"
                },
                new Team()
                {
                    Name = "Williams",
                    Country = "GB"
                },
                new Team()
                {
                    Name = "Jerigo",
                    Country = "IL"
                },
            };
        }
    }
}
