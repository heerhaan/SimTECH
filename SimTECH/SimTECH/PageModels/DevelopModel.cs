namespace SimTECH.PageModels
{
    public enum Quantifier { Set, Range, Direct }

    public class DevelopedEntrant
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Country? Nationality { get; set; }
        public int? Optional { get; set; }
        public int Old { get; set; }
        public int Change { get; set; }
        public int New { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
    }
}
