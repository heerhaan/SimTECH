namespace SimTECH.PageModels.Seasons.Development
{
    public enum Quantifier { Set, Range, Direct }

    public class DevelopModel
    {
        public long ActiveRaceClassId { get; set; }
        public Entrant ActiveEntrant { get; set; } = Entrant.Driver;
        public Aspect ActiveAspect { get; set; } = Aspect.Reliability;
        public Quantifier ActiveQuantifier { get; set; } = Quantifier.Range;
        public int MinChange { get; set; }
        public int MaxChange { get; set; }
        public bool IsOptionalColumnVisible { get; set; }
        public bool IsDirectRangeReadOnly => ActiveQuantifier != Quantifier.Direct;
    }

    public class DevelopedEntrant
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Country? Nationality { get; set; }
        public bool Mark { get; set; }

        public int? Optional { get; set; }

        public int Old { get; set; }
        public int Change { get; set; }
        public int New { get; set; }

        public int Min { get; set; }
        public int Max { get; set; }

        public int ValueMinimumLimit { get; set; }
        public int ValueMaximumLimit { get; set; } = int.MaxValue;
    }
}
