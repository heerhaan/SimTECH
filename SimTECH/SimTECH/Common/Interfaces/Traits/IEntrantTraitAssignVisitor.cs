namespace SimTECH.Common.Interfaces.Traits;

public interface IEntrantTraitAssignVisitor<in TArg>
{
    Task AssignDrivers(TArg arg);

    Task AssignTeams(TArg arg);

    Task AssignTraits(TArg arg);
}
