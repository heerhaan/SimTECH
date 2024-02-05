namespace SimTECH.PageModels.Seasons;

public class PreviousEntrantSetup<T>
{
    public long LeagueId { get; set; }
    public string LeagueName { get; set; }

    public long SeasonId { get; set; }
    public int SeasonYear { get; set; }

    public long? MemberId { get; set; }

    public T Entrant { get; set; }
}
