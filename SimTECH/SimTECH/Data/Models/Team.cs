using SimTECH.Common.Enums;

namespace SimTECH.Data.Models;

public sealed class Team : ModelState
{
    public string Name { get; set; }
    public Country Country { get; set; }
    public string Biography { get; set; }
    public bool Mark { get; set; }

    // We need a very worked out design for how finances will be used and how to implement it in such a way that it can work automatically too
    // Thus far sponsors each season are a swell idea, (so part of SeasonTeam). But how will the money be used, contracts?
    //public int Balance { get; set; }//the fuck is balance, dat is geld mika

    public IList<Contract> Contracts { get; set; }
    public IList<SeasonTeam> SeasonTeams { get; set; }
    public IList<TeamTrait> TeamTraits { get; set; }
}
