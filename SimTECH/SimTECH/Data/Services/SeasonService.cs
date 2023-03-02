using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    public class SeasonService
    {
        private readonly List<Season> _seasons = new()
        {
            new Season()
            {
                Id = 1,
                State = State.Active,
                Year = 2023,

                MaximumDriversInRace = 20,
                QualifyingAmountQ2 = 16,
                QualifyingAmountQ3 = 10,
                QualifyingRNG = 20,
                RunAmountSession = 2,
                GridBonus = 3,
                PitMinimum = -50,
                PitMaximum = -20,

                PointsPole = 1,
                PointsFastestLap = 1,
            }
        };

        public List<Season> GetTestData()
        {
            return _seasons;
        }

        public void CreateSeason(Season season)
        {
            _seasons.Add(season);
        }
    }
}
