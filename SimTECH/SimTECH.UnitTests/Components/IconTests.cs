﻿using Bunit;
using FluentAssertions;
using SimTECH.Shared.Components;
using SimTECH.UnitTests.Infrastructure;
using Xunit;
using static Bunit.ComponentParameterFactory;

namespace SimTECH.UnitTests.Components;

public class IconTests : TestContext
{
    [Fact]
    public void ShouldExampleTest()
    {
        var changeParam = Parameter(nameof(GridChange.Change), 10);
        var component = RenderComponent<GridChange>();

        // component.Should().StartWith(bla bla mudstacki ets)
        //      .And.Contain(IconCollectionArrowUp)
        //      .And.Contain(Color.Succ)
    }
}
