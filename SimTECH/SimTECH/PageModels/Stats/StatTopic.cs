using MudBlazor;

namespace SimTECH.PageModels.Stats;

public class StatTopic
{
    public StatTopic(string title, string description, Type statType)
    {
        Title = title;
        Description = description;
        StatisticType = statType;
    }
    public StatTopic(string title, string description, Type statType, Func<Task<DialogParameters>> query)
    {
        Title = title;
        Description = description;
        StatisticType = statType;
        ParameterQuery = query;
    }

    public string Title { get; set; }
    public string Description { get; set; }
    public Type StatisticType { get; set; }
    public Func<Task<DialogParameters>> ParameterQuery { get; set; }
}
