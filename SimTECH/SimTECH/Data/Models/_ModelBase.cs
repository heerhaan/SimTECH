namespace SimTECH.Data.Models
{
    // Can be used to make generic models sharing properties, think State
    // ModelBase<T> if I can see why one would add the generic <T>
    public abstract class ModelBase
    {
        public long Id { get; set; }
    }
}
