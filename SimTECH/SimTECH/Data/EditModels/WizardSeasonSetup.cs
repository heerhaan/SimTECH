using SimTECH.Data.Models;
using SimTECH.Extensions;

namespace SimTECH.Data.EditModels
{
    public class WizardSeasonSetup
    {
        public WizardSeasonSetup()
        {
            PointAllotments = new();
            Races = new();
            SeasonDrivers = new();
            SeasonTeams = new();
            SeasonEngines = new();
        }
        public WizardSeasonSetup(Season season)
        {
            Year = 1 + season.Year;
            MaximumDriversInRace = season.MaximumDriversInRace;
            QualifyingAmountQ2 = season.QualifyingAmountQ2;
            QualifyingAmountQ3 = season.QualifyingAmountQ3;
            QualifyingRNG = season.QualifyingRNG;
            RunAmountSession= season.RunAmountSession;
            GridBonus = season.GridBonus;
            PitMinimum = season.PitMinimum;
            PitMaximum = season.PitMaximum;
            PointsPole = season.PointsPole;
            PointsFastestLap = season.PointsFastestLap;

            PointAllotments = season.PointAllotments?
                .Select(e => new PointAllotment { Points = e.Points, Position = e.Position }).ToList()
                ?? new();
            Races = season.Races?
                .Select(e => new Race
                {
                    Round = e.Round,
                    Name = e.Name,
                    Weather = EnumHelper.GetRandomWeather(),
                    Stints = e.Stints,
                    TrackId = e.TrackId
                }).ToList()
                ?? new();

            SeasonDrivers = new();
            SeasonTeams = new();
            SeasonEngines = new();

            LeagueId = season.LeagueId;
        }

        public int Year { get; set; }

        public int MaximumDriversInRace { get; set; }
        public int QualifyingAmountQ2 { get; set; }
        public int QualifyingAmountQ3 { get; set; }
        public int QualifyingRNG { get; set; }
        public int RunAmountSession { get; set; }
        public int GridBonus { get; set; }
        public int PitMinimum { get; set; }
        public int PitMaximum { get; set; }

        public int PointsPole { get; set; }
        public int PointsFastestLap { get; set; }

        public List<PointAllotment> PointAllotments { get; set; }
        public List<Race> Races { get; set; }
        public List<SeasonDriver> SeasonDrivers { get; set; }
        public List<SeasonTeam> SeasonTeams { get; set; }
        public List<SeasonEngine> SeasonEngines { get; set; }

        public long LeagueId { get; set; }
    }
}
