using SimTECH.Data.Models;

namespace SimTECH.PageModels.RaceWeek
{
    public class QualifyingSession : SessionBase
    {
        public List<QualifyingScore> SessionScores { get; set; }
        public int AllowedEntries { get; set; } = int.MaxValue;
        public int ProgressionCutoff { get; set; } = int.MaxValue;
        public bool IsDisabled { get;set; }
    }
}
