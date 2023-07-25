using SimTECH.Data.Models;

namespace SimTECH.PageModels
{
    public class RaweCeekModel
    {
        public Race Race { get; set; }
        public Climate Climate { get; set; }
        public Season Season { get; set; }

        public LeagueOptions LeagueOptions { get; set; }

        public List<RaweCeekDriver> RaweCeekDrivers { get; set; }
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

        public int? Penalty { get; set; }
        public string? Reasons { get; set; }

        public Result Result { get; set; }
        public List<LapScore> LapScores { get; set; }

        public string FullName => FirstName + " " + LastName;
    }

    // Underneath the legacy one
    public class RaceWeekModel
    {
        public Race Race { get; set; }
        public int MaximumInRace { get; set; }
        public int PracticeCompletedCount { get; set; }

        public List<RaceWeekDriver> RaceWeekDrivers { get; set; }
        public List<Trait>? TrackTraits { get; set; }
    }

    public class RaceWeekDriver
    {
        // Properties related to the driver of this result
        public long SeasonDriverId { get; set; }
        public string FullName { get; set; }
        public int Number { get; set; }
        public TeamRole Role { get; set; }
        public Country Country { get; set; }

        // Properties related to the team of this result
        public long SeasonTeamId { get; set; }
        public string TeamName { get; set; }
        public string Colour { get; set; }
        public string Accent { get; set; }
        public string ManufacturerName { get; set; }
        public string ManufacturerColour { get; set; }
        public string ManufacturerAccent { get; set; }

        // Properties related to the result
        public long ResultId { get; set; }
        public int Grid { get; set; }
        public int Position { get; set; }
        public RaceStatus Status { get; set; }
        public Tyre Tyre { get; set; }

        // Whether the driver has a penalty for this race
        public int? Penalty { get; set; }
        public string? Reasons { get; set; }
    }
}
