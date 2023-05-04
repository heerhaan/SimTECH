namespace SimTECH.Data.Models
{
    // Can be used to make generic models sharing properties, think State
    // ModelBase<T> if I can see why one would add the generic <T> for an implementation (generics? interfaces?)
    public abstract class ModelBase
    {
        public long Id { get; set; }
    }

    public abstract class ModelState : ModelBase
    {
        public State State { get; set; }
    }
}
