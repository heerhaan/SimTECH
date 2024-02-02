namespace SimTECH.Common.Enums;

public enum IncidentCategory
{
    Driver, Car, Engine, Disqualified, Lethal
}

public static class IncidentCategoryEnumHelper
{
    public static readonly Dictionary<IncidentCategory, string> GetIncidentCategories = new()
        {
            { IncidentCategory.Driver, "Driver" },
            { IncidentCategory.Car, "Car" },
            { IncidentCategory.Engine, "Engine" },
            { IncidentCategory.Disqualified, "Disqualified" },
            { IncidentCategory.Lethal, "Injury" },
        };
}
