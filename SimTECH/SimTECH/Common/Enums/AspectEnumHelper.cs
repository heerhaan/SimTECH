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
    public static readonly Aspect[] GetRangeableAspects = new Aspect[]
        {
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
        };

    public static string ReadableAspect(this Aspect aspect) => aspect switch
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

    public static string ShortReadableAspect(this Aspect aspect) => aspect switch
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
