namespace SimTECH.Data.Models;

public class TeamTrait
{
    public long TraitId { get; set; }
    public Trait Trait { get; set; }
    public long TeamId { get; set; }
    public Team Team { get; set; }
}
