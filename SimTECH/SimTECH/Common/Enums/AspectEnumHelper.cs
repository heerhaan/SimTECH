using SimTECH.Data.Models;

namespace SimTECH.Common.Enums;

public enum Aspect
{
    None = 0,
    Skill,
    Age,
    BaseCar,
    Engine,
    Aero,
    Chassis,
    Powertrain,
    Reliability,
    Attack,
    Defense,
}

public static class AspectEnumHelper
{
    public static readonly Aspect[] GetRangeableAspects =
        [
            Aspect.Skill,
            Aspect.Age,
            Aspect.BaseCar,
            Aspect.Engine,
            Aspect.Aero,
            Aspect.Chassis,
            Aspect.Powertrain,
            Aspect.Reliability,
            Aspect.Attack,
            Aspect.Defense,
        ];

    public static string GetReadableAspect(this Aspect aspect)
    {
        return aspect switch
        {
            Aspect.Skill => "Skill",
            Aspect.Age => "Age",
            Aspect.BaseCar => "Car",
            Aspect.Engine => "Engine",
            Aspect.Aero => "Aero",
            Aspect.Chassis => "Chassis",
            Aspect.Powertrain => "Powertrain",
            Aspect.Reliability => "Reliability",
            Aspect.Attack => "Attack",
            Aspect.Defense => "Defense",
            _ => "Unknown"
        };
    }

    public static string GetShortAspectText(this Aspect aspect)
    {
        return aspect switch
        {
            Aspect.Skill => "SKL",
            Aspect.Age => "AGE",
            Aspect.BaseCar => "CAR",
            Aspect.Engine => "ENG",
            Aspect.Aero => "AER",
            Aspect.Chassis => "CHA",
            Aspect.Powertrain => "POW",
            Aspect.Reliability => "REL",
            Aspect.Attack => "ATT",
            Aspect.Defense => "DEF",
            _ => "IDK"
        };
    }

    public static int GetAspectDriverValue(this Aspect aspect, SeasonDriver driver)
    {
        return aspect switch
        {
            Aspect.Skill => driver.Skill,
            Aspect.Reliability => driver.Reliability,
            Aspect.Attack => driver.Attack,
            Aspect.Defense => driver.Defense,
            _ => 0,
        };
    }

    public static int GetAspectTeamValue(this Aspect aspect, SeasonTeam team)
    {
        return aspect switch
        {
            Aspect.BaseCar => team.BaseValue,
            Aspect.Reliability => team.Reliability,
            Aspect.Aero => team.Aero,
            Aspect.Chassis => team.Chassis,
            Aspect.Powertrain => team.Powertrain,
            _ => 0,
        };
    }

    public static int GetAspectEngineValue(this Aspect aspect, SeasonEngine engine)
    {
        return aspect switch
        {
            Aspect.Engine => engine.Power,
            Aspect.Reliability => engine.Reliability,
            _ => 0,
        };
    }
}
