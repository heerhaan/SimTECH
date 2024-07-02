using SimTECH.Common.Enums;

namespace SimTECH.Data.Models;

public class Driver : ModelState
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Abbreviation { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Country Country { get; set; }
    public string? Biography { get; set; }
    public bool Mark { get; set; }
    public StrategyPreference StrategyPreference { get; set; }

    public IList<SeasonDriver> SeasonDrivers { get; set; }
    public IList<DriverTrait> DriverTraits { get; set; }
    public IList<Contract> Contracts { get; set; }

    public string FullName => $"{FirstName} {LastName}";
}
