using SimTECH.Common.Enums;
using SimTECH.PageModels.Entrants.Drivers;

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

public static class ExtendDriver
{
    public static DriverListItem MapToListItem(this Driver driver)
    {
        return new DriverListItem()
        {
            Id = driver.Id,
            State = driver.State,
            FirstName = driver.FirstName,
            LastName = driver.LastName,
            Abbreviation = driver.Abbreviation,
            DateOfBirth = driver.DateOfBirth,
            Country = driver.Country,
            Biography = driver.Biography,
            Mark = driver.Mark,
            StrategyPreference = driver.StrategyPreference,
        };
    }
}
