using FluentAssertions;
using SimTECH.Constants;
using SimTECH.Shared.Components;
using static Bunit.ComponentParameterFactory;

namespace SimTECH.UnitTests.Components;

[TestFixture]
public class GridChangeTests : BunitTest
{
    /// <summary>
    /// Checks whether the component by default renders the position retained styling
    /// </summary>
    [Test]
    public void ShouldRenderEqual()
    {
        var component = Context.RenderComponent<GridChange>();

        component.Markup.Trim().Should().StartWith("<div")
            .And.Contain("color:black")
            .And.Contain("mud-warning-text")
            .And.Contain(IconCollection.Equal);
    }

    [Test]
    public void ShouldRenderSuccessGain()
    {
        var change = Parameter(nameof(GridChange.Change), 3);
        var component = Context.RenderComponent<GridChange>(change);

        component.Markup.Trim().Should().StartWith("<div")
            .And.Contain("mud-success-text")
            .And.Contain(IconCollection.ArrowUp);
    }
}
