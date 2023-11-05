namespace SimTECH.Common.Enums;

public enum CategoryIncident
{
    Driver, Car, Engine, Disqualified, Lethal
}

public static class CategoryIncidentEnumHelper
{
    public static readonly Dictionary<CategoryIncident, string> GetIncidentCategories = new()
        {
            { CategoryIncident.Driver, "Driver" },
            { CategoryIncident.Car, "Car" },
            { CategoryIncident.Engine, "Engine" },
            { CategoryIncident.Disqualified, "Disqualified" },
            { CategoryIncident.Lethal, "Injury" },
        };
}
