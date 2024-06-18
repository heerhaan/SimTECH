using Bunit;
using FluentAssertions;
using SimTECH.Constants;
using SimTECH.Data;
using SimTECH.Shared.Components;
using SimTECH.Tests.Infrastructure;
using Xunit;
using static Bunit.ComponentParameterFactory;

namespace SimTECH.Tests.Components;

public class GridChangeTests : IClassFixture<DataFixture>
{
    private SimTechDbContext _context;

    public GridChangeTests(DataFixture fixture)
    {
        _context = fixture.GetDbContext();
    }

    /// <summary>
    /// Checks whether the component by default renders the position retained styling
    /// </summary>
    [Fact]
    public void ShouldRenderEqual()
    {
        using var ctx = new TestContext();

        var component = ctx.RenderComponent<GridChange>();

        component.Markup.Trim().Should().StartWith("<div")
            .And.Contain("color:black")
            .And.Contain("mud-warning-text")
            .And.Contain(IconCollection.Equal);
    }

    [Fact]
    public void ShouldRenderSuccessGain()
    {
        using var ctx = new TestContext();

        var change = Parameter(nameof(GridChange.Change), 3);
        var component = ctx.RenderComponent<GridChange>(change);

        component.Markup.Trim().Should().StartWith("<div")
            .And.Contain("mud-success-text")
            .And.Contain(IconCollection.ArrowUp);
    }

    [Fact]
    public void TEMPShouldRetrieveData()
    {
        var maxie = _context.Driver.FirstOrDefault(e => e.FirstName.Contains("Max"));

        maxie.Should().NotBeNull();
    }
}
