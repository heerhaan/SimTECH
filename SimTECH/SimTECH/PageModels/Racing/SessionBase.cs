namespace SimTECH.PageModels.Racing
{
    public abstract class SessionBase
    {
        public int SessionIndex { get; set; }
        public bool IsFinished { get; set; }
    }

    public static class ExtendSession
    {
        //public static int MaxScore(this SessionBase session) => session.SessionScores.Max();
    }
}
