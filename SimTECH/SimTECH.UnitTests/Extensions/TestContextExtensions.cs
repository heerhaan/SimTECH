using MudBlazor.Services;

namespace SimTECH.UnitTests.Extensions;

public static class TestContextExtensions
{
    public static void AddTestServices(this Bunit.TestContext context)
    {
        context.Services.AddMudServices(options =>
        {
            options.SnackbarConfiguration.ShowTransitionDuration = 0;
            options.SnackbarConfiguration.HideTransitionDuration = 0;
        });
    }
}
