using SimTECH.Common.Enums;

namespace SimTECH.PageModels.Entrants.Drivers;

public class DriverListItem
{
    public long Id { get; set; }
    public State State { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Abbreviation { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Country Country { get; set; }
    public string? Biography { get; set; }
    public bool Mark { get; set; }
    public StrategyPreference StrategyPreference { get; set; }

    public string? ActiveInLeague { get; set; }

    public string FullName => $"{FirstName} {LastName}";
}
