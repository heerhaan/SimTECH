namespace SimTECH.Extensions;

public static class TypeHelper
{
    // initialMailingGroup.ReflectedDowncast(new EmailInvitationGroupEditor());
    public static TChild CastDown<TChild, TParent>(this TParent parent, TChild child)
        where TChild : TParent
    {
        var props = typeof(TParent).GetProperties();

        foreach (var prop in props.Where(e => e.SetMethod != null && e.GetMethod != null))
            prop.SetValue(child, prop.GetValue(parent));

        return child;
    }

    /// <summary>
    /// Reflects property values of instance of superclass into created instance of subclass. Allows for prefilling, creating a shallow-copy downcast.
    /// </summary>
    /// <param name="parents">Instance of superclass to reflect from</param>
    /// <param name="instanceFill">Custom conversion delegate for sub-instance specific conversion logic and initialisation</param>
    /// <returns>The state-changed instance of subclass passed in sub parameter</returns>
    /// (await logic.GetInstructions()).ReflectedDowncast(e => new InstructionDownloadableModel() { DownloadURL = GetInstructionDownloadPath(e) })
    public static IEnumerable<TChild> CastDown<TChild, TParent>(this IEnumerable<TParent> parents, Func<TParent, TChild> instanceFill)
        where TChild : TParent
    {
        return parents.Select(e => e.CastDown(instanceFill(e))).AsEnumerable();
    }
}
