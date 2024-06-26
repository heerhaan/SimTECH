using SimTECH.Common.Enums;
using SimTECH.Data.Models;

namespace SimTECH.PageModels.RaceWeek;

public class RaceDriver : DriverBase
{
    public RaceStatus Status { get; set; }
    public Incident? Incident { get; set; }

    public int Position { get; set; }
    public int Grid { get; set; }
    public int AbsolutePosition { get; set; }
    public int AbsoluteGrid { get; set; }
    public int Setup { get; set; }
    public int TyreLife { get; set; }
    public Tyre CurrentTyre { get; set; }

    public int DriverReliability { get; set; }
    public int CarReliability { get; set; }
    public int EngineReliability { get; set; }
    public int WearMinMod { get; set; }
    public int WearMaxMod { get; set; }
    public int RngMinMod { get; set; }
    public int RngMaxMod { get; set; }
    public int LifeBonus { get; set; }

    public bool HasFastestLap { get; set; }
    // TODO: Would like to remove the property underneath, rather check the racerflags of lastscore
    public bool InstantOvertaken { get; set; }// set to true when pitstop OR dnf
    // TODO: Would like to remove the property underneath, rather check the racerflags of lastscore
    public bool RecentMistake { get; set; }

    public List<LapScore> LapScores { get; set; } = [];

    public int LastScore { get; set; }
    public int OvertakeCount { get; set; }
    public int DefensiveCount { get; set; }

    public int LapSum => LapScores.Sum(e => e.Score);
    public int GridChange => AbsoluteGrid - AbsolutePosition;

    /* Using this model = FINE, BUT implementation needs work.
     * Consider methods to control state of race driver!
     * For the cells in race table; properties here should be used. No logic in cell!
     * Visitor pattern to control actions defined by enum?
     * Aggregate to collectively advance drivers?
     */
}

// I had an idea to use this class with some good ole class methods but uhhhh, hmmm, i dont know man
class RaceTyre
{
    public long TyreId { get; set; }

    public int TyreLife { get; set; }

    public Tyre Tyre { get; set; }
}