using SimTECH.Data.Models;

namespace SimTECH.PageModels.Traits;

public class AssignableTrait
{
    public AssignableTrait() { }
    public AssignableTrait(Trait trait)
    {
        Id = trait.Id;
        Name = trait.Name;
    }

    public long Id { get; set; }

    public string Name { get; set; }
}
