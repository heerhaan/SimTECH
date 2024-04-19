using Bunit;
using FluentAssertions;
using NUnit.Framework;
using SimTECH.Shared.Components;
using static Bunit.ComponentParameterFactory;

namespace SimTECH.UnitTests.Components;

[TestFixture]
public class IconTests : BunitTest
{
    [Test]
    public void ShouldExampleTest()
    {
        var changeParam = Parameter(nameof(GridChange.Change), 10);
        var component = Context.RenderComponent<GridChange>();

        // component.Should().StartWith(bla bla mudstacki ets)
        //      .And.Contain(IconCollectionArrowUp)
        //      .And.Contain(Color.Succ)
    }
}
