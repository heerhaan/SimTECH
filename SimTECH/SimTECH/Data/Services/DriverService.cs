using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    public class DriverService
    {
        public IEnumerable<Driver> GetTestNames()
        {
            return new List<Driver>
            {
                new Driver()
                {
                    Name = "Max Verstappen",
                    Age = 25,
                    Role = "Chad"
                },
                new Driver()
                {
                    Name = "Sharl Eclair",
                    Age = 25,
                    Role = "Virgin"
                },
                new Driver()
                {
                    Name = "Nico Hulkenberg",
                    Age = 85,
                    Role = "Eternal"
                }
            };
        }
    }
}
