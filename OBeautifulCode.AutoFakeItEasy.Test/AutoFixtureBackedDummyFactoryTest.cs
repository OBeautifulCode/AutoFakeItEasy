// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoFixtureBackedDummyFactoryTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using FakeItEasy;
    using FluentAssertions;

    using Xunit;

    /// <summary>
    /// Tests the <see cref="AutoFixtureBackedDummyFactory"/>.
    /// </summary>
    public static class AutoFixtureBackedDummyFactoryTest
    {
        // ReSharper disable InconsistentNaming
        [Fact]
        public static void ADummy_WhenCreatingObjectOfTypeEnum_ReturnsEnumValuesInRandomOrder()
        {
            // Arrange
            var enumValuesCount = Enum.GetValues(typeof(Number)).Length;

            // Act
            var randomEnumValues = Enumerable.Range(1, enumValuesCount).Select(_ => A.Dummy<Number>()).ToList();

            // Assert
            randomEnumValues.Should().NotBeAscendingInOrder();
        }

        [Fact]
        public static void ADummy_WhenCreatingObjectOfTypeBool_ReturnsBoolValuesInRandomOrder()
        {
            // Arrange
            var sequentialBools1 = new List<bool> { true, false, true, false, true, false, true, false };
            var sequentialBools2 = new List<bool> { false, true, false, true, false, true, false, true };

            // Act
            var randomEnumValues = Enumerable.Range(1, sequentialBools1.Count).Select(_ => A.Dummy<bool>()).ToList();

            // Assert
            randomEnumValues.Should().NotEqual(sequentialBools1);
            randomEnumValues.Should().NotEqual(sequentialBools2);
        }

        // ReSharper restore InconsistentNaming
    }
}
