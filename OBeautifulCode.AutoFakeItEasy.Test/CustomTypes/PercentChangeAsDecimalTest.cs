// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PercentChangeAsDecimalTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
namespace OBeautifulCode.AutoFakeItEasy.Test
{
    using System;

    using FluentAssertions;

    using Xunit;

    /// <summary>
    /// Tests the <see cref="PercentChangeAsDecimal"/> class.
    /// </summary>
    public static class PercentChangeAsDecimalTest
    {
        [Fact]
        public static void Constructor___Should_throw_ArgumentOutOfRangeException___When_the_value_parameter_is_less_than_MinPercentChange()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => new PercentChangeAsDecimal(PercentChangeAsDecimal.MinPercentChange - .00001m));
            var ex2 = Record.Exception(() => new PercentChangeAsDecimal(PercentChangeAsDecimal.MinPercentChange - 1m));
            var ex3 = Record.Exception(() => new PercentChangeAsDecimal(decimal.MinValue));

            // Assert
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
            ex3.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentOutOfRangeException___When_the_value_parameter_is_greater_than_MaxPercentChange()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => new PercentChangeAsDecimal(PercentChangeAsDecimal.MaxPercentChange + .00001m));
            var ex2 = Record.Exception(() => new PercentChangeAsDecimal(PercentChangeAsDecimal.MaxPercentChange + 1m));
            var ex3 = Record.Exception(() => new PercentChangeAsDecimal(decimal.MaxValue));

            // Assert
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
            ex3.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void Constructor___Should_not_throw___When_the_value_parameter_is_greater_than_or_equal_MinPercentChange_and_less_than_or_equal_MaxPercentChange()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => new PercentChangeAsDecimal(PercentChangeAsDecimal.MinPercentChange));
            var ex2 = Record.Exception(() => new PercentChangeAsDecimal(PercentChangeAsDecimal.MaxPercentChange));
            var ex3 = Record.Exception(() => new PercentChangeAsDecimal((PercentChangeAsDecimal.MinPercentChange + PercentChangeAsDecimal.MaxPercentChange) / 2m));

            // Assert
            ex1.Should().BeNull();
            ex2.Should().BeNull();
            ex3.Should().BeNull();
        }

        [Fact]
        public static void Value___Should_return_the_same_value_passed_to_constructor___When_getting()
        {
            // Arrange
            var expectedDecimal = (PercentChangeAsDecimal.MinPercentChange + PercentChangeAsDecimal.MaxPercentChange) / 2m;
            var systemUnderTest = new PercentChangeAsDecimal(expectedDecimal);

            // Act
            var actualDecimal = systemUnderTest.Value;

            // Assert
            actualDecimal.Should().Be(expectedDecimal);
        }

        [Fact]
        public static void Cast___Should_return_the_same_value_passed_to_constructor___When_casting_to_decimal()
        {
            // Arrange
            var expectedDecimal = (PercentChangeAsDecimal.MinPercentChange + PercentChangeAsDecimal.MaxPercentChange) / 2m;
            var systemUnderTest = new PercentChangeAsDecimal(expectedDecimal);

            // Act
            var actualDecimal = (decimal)systemUnderTest;

            // Assert
            actualDecimal.Should().Be(expectedDecimal);
        }

        [Fact]
        public static void Implicit_conversion___Should_return_same_int_value_passed_to_constructor___When_converting_to_object_of_type_int()
        {
            // Arrange
            var expectedDecimal = (PercentChangeAsDecimal.MinPercentChange + PercentChangeAsDecimal.MaxPercentChange) / 2m;
            var systemUnderTest = new PercentChangeAsDecimal(expectedDecimal);

            // Act
            decimal actualDecimal = systemUnderTest;

            // Assert
            actualDecimal.Should().Be(expectedDecimal);
        }
    }
}

// ReSharper restore InconsistentNaming
// ReSharper restore CheckNamespace