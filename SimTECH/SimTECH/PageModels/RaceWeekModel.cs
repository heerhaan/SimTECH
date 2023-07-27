using SimTECH.Data.Models;
using SimTECH.PageModels.Racing;

namespace SimTECH.PageModels
{
    public class RaweCeekModel
    {
        public Race Race { get; set; }
        public Climate Climate { get; set; }
        public Season Season { get; set; }

        public LeagueOptions LeagueOptions { get; set; }

        public List<RaweCeekDriver> RaweCeekDrivers { get; set; }

        public List<long> ConsumablePenalties { get; set; } = new();
    }

    public class RaweCeekDriver
    {
        public long SeasonDriverId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Country Nationality { get; set; }
        public int Number { get; set; }
        public TeamRole Role { get; set; }

        public long SeasonTeamId { get; set; }
        public string TeamName { get; set; }
        public string Colour { get; set; }
        public string Accent { get; set; }

        public long ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public string ManufacturerColour { get; set; }
        public string ManufacturerAccent { get; set; }

        public int QualyPower { get; set; }
        public int RacePower { get; set; }

        public int Attack { get; set; }
        public int Defense { get; set; }
        public int DriverReliability { get; set; }
        public int CarReliability { get; set; }
        public int EngineReliability { get; set; }
        public int WearMinMod { get; set; }
        public int WearMaxMod { get; set; }
        public int RngMinMod { get; set; }
        public int RngMaxMod { get; set; }
        public int LifeBonus { get; set; }
        public bool HasFastestLap { get; set; }

        public long? PenaltyId { get; set; }
        public int? Penalty { get; set; }
        public string? Reasons { get; set; }

        public long ResultId { get; set; }
        public int Grid { get; set; }
        public int Position { get; set; }
        public int Score { get; set; }
        public RaceStatus Status { get; set; }
        //public int Setup { get; set; }
        public int TyreLife { get; set; }
        public bool FastestLap { get; set; }
        public int Overtaken { get; set; }
        public int Defended { get; set; }
        public long? IncidentId { get; set; }
        public Tyre Tyre { get; set; }

        public string FullName => FirstName + " " + LastName;
    }

    public static class ExtendRaweCeekDriver
    {
        public static SessionDriver MapToSessionDriver(this RaweCeekDriver driver, int amountRuns)
        {
            return new SessionDriver
            {
                SeasonDriverId = driver.SeasonDriverId,
                ResultId = driver.ResultId,
                FirstName = driver.FirstName,
                LastName = driver.LastName,
                Nationality = driver.Nationality,
                Number = driver.Number,
                TeamName = driver.TeamName,
                Colour = driver.Colour,
                Accent = driver.Accent,
                Power = driver.QualyPower,
                Scores = new int[amountRuns]
            };
        }
    }
}
