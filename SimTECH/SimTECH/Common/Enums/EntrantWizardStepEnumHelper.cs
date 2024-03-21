namespace SimTECH.Common.Enums;

public enum EntrantWizardStep
{
    EnginesAdd = 0,
    EnginesEdit = 1,
    TeamsAdd = 2,
    TeamsEdit = 3,
    TeamsAssignment = 4,
    RaceClassAssignment = 5,
    DriversAdd = 6,
    DriversEdit = 7,
    DriversAssignment = 8,
    Summary = 9,
}

public static class EntrantWizardStepEnumHelper
{
    public static string GetStepTitle(this EntrantWizardStep step) => step switch
    {
        EntrantWizardStep.EnginesAdd => "Add engines",
        EntrantWizardStep.EnginesEdit => "Edit engines",
        EntrantWizardStep.TeamsAdd => "Add teams",
        EntrantWizardStep.TeamsEdit => "Edit teams",
        EntrantWizardStep.TeamsAssignment => "Assign teams",
        EntrantWizardStep.RaceClassAssignment => "Assign race classes",
        EntrantWizardStep.DriversAdd => "Add drivers",
        EntrantWizardStep.DriversEdit => "Edit drivers",
        EntrantWizardStep.DriversAssignment => "Assign drivers",
        EntrantWizardStep.Summary => "Participants",
        _ => "Unknown (how tf did you do this)",
    };
}
