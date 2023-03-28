using SimTECH.Data.Models;

namespace SimTECH.PageModels
{
    public class RaceModel
    {
        public string Name { get; set; }
        public Country Country { get; set; }
        public Weather Weather { get; set; }

        public int AmountRuns { get; set; }

        public List<RaceDriver> RaceDrivers { get; set; }
    }
}
