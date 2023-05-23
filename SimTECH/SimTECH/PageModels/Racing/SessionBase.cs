namespace SimTECH.PageModels.Racing
{
    public abstract class SessionBase
    {
        public int[] SessionScores { get; set; }
        public int Position { get; set; }
    }

    public static class ExtendSession
    {
        public static int MaxScore(this SessionBase session) => session.SessionScores.Max();
    }
}
