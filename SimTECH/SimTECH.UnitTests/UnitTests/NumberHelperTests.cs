﻿using FluentAssertions;
using SimTECH.Extensions;
using Xunit;

namespace SimTECH.Tests.UnitTests;

public class NumberHelperTests
{
    [Fact]
    public void RandomIntOutputWithinRange()
    {
        var minRange = 2;
        var maxRange = 4;

        var randomValue = NumberHelper.RandomInt(minRange, maxRange);

        randomValue.Should().BeGreaterThanOrEqualTo(minRange)
            .And.BeLessThanOrEqualTo(maxRange);
    }

    [Fact]
    public void AverageNumbersIsCorrect()
    {
        var numbers = new List<int>(4) { 1, 3, 5, 7 };

        var expected = 4;

        var resultAsSum = NumberHelper.Average(numbers.Sum(), 4);

        resultAsSum.Should().Be(expected);

        var resultAsParams = NumberHelper.Average(1, 3, 5, 7);

        resultAsParams.Should().Be(expected);
    }
}
