using SimTECH.Data.Models;

namespace SimTECH.PageModels
{
    public abstract class SessionBase
    {
        public long RaceId { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
        public Weather Weather { get; set; }
        public bool IsFinished { get; set; }

        public int AmountRuns { get; set; }//Can be used for both runs in qualy and stints in race
        public double GapMarge { get; set; }
    }

    public class RaceModel : SessionBase
    {
        public long TrackId { get; set; }
        public double TrackLength { get; set; }
        public int Round { get; set; }
        public int RaceLength { get; set; }

        public List<RaceDriver> RaceDrivers { get; set; }

        public Season Season { get; set; }//pick values intead of whole object?
        public LeagueOptions LeagueOptions { get; set; }

        // Values read from the configuration
        public int FatalityOdds { get; set; }
        public int DisqualifyOdds { get; set; }
        public int MistakeRolls { get; set; }
        public int MistakeMinCost { get; set; }
        public int MistakeMaxCost { get; set; }

        public int QualifyingBonus(int grid) => (RaceDrivers.Count * Season.GridBonus) - ((grid - 1) * Season.GridBonus);

        public Race ToFinishedRace()// arguably it might be easier to just store a Race entity object
        {
            return new Race
            {
                Id = RaceId,
                Round = Round,
                Name = Name,
                Weather = Weather,
                RaceLength = RaceLength,
                State = State.Closed,
                SeasonId = Season.Id,
                TrackId = TrackId
            };
        }
    }

    public class QualifyingModel : SessionBase
    {
        public int MaximumRaceDrivers { get; set; }
        public int QualyRng { get; set; }
        public int QualyAmountQ2 { get; set; }
        public int QualyAmountQ3 { get; set; }

        public List<QualifyingDriver> QualifyingDrivers { get; set; }
    }

    public class PracticeModel : SessionBase
    {
        public int PracticeRng { get; set; }

        public List<PracticeDriver> PracticeDrivers { get; set; }
    }
}
