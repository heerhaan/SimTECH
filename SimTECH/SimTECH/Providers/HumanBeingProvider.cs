using MudBlazor;
using SimTECH.Common.Enums;
using SimTECH.Constants;
using SimTECH.Data.Models;
using SimTECH.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SimTECH.Providers;

public class HumanBeingProvider
{
    private readonly IWebHostEnvironment _env;
    private readonly string[] dataFiles;

    public HumanBeingProvider(IWebHostEnvironment env)
    {
        _env = env;
        dataFiles = Directory.GetFiles(Path.Combine(_env.WebRootPath, "json"));
    }

    public LanguageInfo[] GetLanguageHumanInfo()
    {
        var pathNameLength = dataFiles[0].Length;

        return dataFiles
            .Select(e => new LanguageInfo
            {
                Country = Enum.TryParse<Country>(e.Substring(startIndex: pathNameLength - 7, 2), out var cuntLang)
                    ? cuntLang
                    : Globals.DefaultCountry,
                LanguageType = e.Substring(startIndex: pathNameLength - 10, 2),
                Path = e,
            })
            .DistinctBy(e => e.Country)
            .ToArray();
    }

    public List<HumanBeing> GetHumanBeings(int amount, LanguageInfo[] langs, Gender gender, DateRange dateRange)
    {
        var humanBeans = new List<HumanBeing>();
        var nameSet = new List<(string, Country, Gender)>();
        var familySet = new List<(string, Country)>();

        foreach (var lang in langs)
        {
            var dataString = File.ReadAllText(lang.Path);
            var databank = JsonSerializer.Deserialize<PersonDatabank>(dataString);

            if (databank == null)
                continue;

            if (gender == Gender.Male)
            {
                nameSet.AddRange(databank.Male.Select(e => (e, lang.Country, Gender.Male)));
            }
            else if (gender == Gender.Female)
            {
                nameSet.AddRange(databank.Female.Select(e => (e, lang.Country, Gender.Female)));
            }
            else
            {
                nameSet.AddRange(databank.Male.Select(e => (e, lang.Country, Gender.Male)));
                nameSet.AddRange(databank.Female.Select(e => (e, lang.Country, Gender.Female)));
            }

            familySet.AddRange(
                databank.Family.Where(e => e.Length >= 3).Select(e => (e, lang.Country)));
        }

        for (int i = 0; i < amount; i++)
        {
            var humanName = nameSet.TakeRandomItem();

            humanBeans.Add(new HumanBeing
            {
                FirstName = humanName.Item1,
                LastName = familySet.Where(e => e.Item2 == humanName.Item2).ToArray().TakeRandomItem().Item1,
                Gender = humanName.Item3,
                Nationality = humanName.Item2,
                Dob = GetRandomDate(dateRange),
            });
        }

        return humanBeans.ToList();
    }

    private static DateTime GetRandomDate(DateRange dateRange)
    {
        var timeSpan = dateRange.End.GetValueOrDefault() - dateRange.Start.GetValueOrDefault();
        var addedDate = new TimeSpan(NumberHelper.RandomInt((int)timeSpan.TotalDays), 0, 0, 0);
        return dateRange.Start.GetValueOrDefault().Add(addedDate).Date;
    }
}

internal class PersonDatabank
{
    [JsonPropertyName("male")]
    public string[] Male { get; set; }

    [JsonPropertyName("female")]
    public string[] Female { get; set; }

    [JsonPropertyName("family")]
    public string[] Family { get; set; }
}

public class LanguageInfo
{
    public Country Country { get; set; }

    public string LanguageType { get; set; }

    public string Path { get; set; }
}

public class HumanBeing
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public Gender Gender { get; set; }

    public Country Nationality { get; set; }

    public DateTime Dob { get; set; }

    public string FullName => $"{FirstName} {LastName}";

    public Driver ToDriver => new()
    {
        FirstName = FirstName,
        LastName = LastName,
        Abbreviation = LastName[..3].ToUpper(),
        DateOfBirth = Dob,
        Country = Nationality,
        Biography = string.Empty,
        State = State.Active,
    };
}
