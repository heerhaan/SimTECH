namespace SimTECH.Data.Models;

public class LeagueTyre
{
    public long LeagueId { get; set; }
    public League League { get; set; }
    public long TyreId { get; set; }
    public Tyre Tyre { get; set; }
}
