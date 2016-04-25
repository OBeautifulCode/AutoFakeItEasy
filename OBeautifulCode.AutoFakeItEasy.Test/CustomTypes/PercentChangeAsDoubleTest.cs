// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PercentChangeAsDoubleTest.cs" company="OBeautifulCode">
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
    /// Tests the <see cref="PercentChangeAsDouble"/> class.
    /// </summary>
    public static class PercentChangeAsDoubleTest
    {
        [Fact]
        public static void Constructor___Should_throw_ArgumentOutOfRangeException___When_the_value_parameter_is_less_than_MinPercentChange()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => new PercentChangeAsDouble(PercentChangeAsDouble.MinPercentChange - .00001d));
            var ex2 = Record.Exception(() => new PercentChangeAsDouble(PercentChangeAsDouble.MinPercentChange - 1d));
            var ex3 = Record.Exception(() => new PercentChangeAsDouble(double.MinValue));

            // Assert
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
            ex3.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentOutOfRangeException___When_the_value_parameter_is_greater_than_MaxPercentChange()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => new PercentChangeAsDouble(PercentChangeAsDouble.MaxPercentChange + .00001d));
            var ex2 = Record.Exception(() => new PercentChangeAsDouble(PercentChangeAsDouble.MaxPercentChange + 1d));
            var ex3 = Record.Exception(() => new PercentChangeAsDouble(double.MaxValue));

            // Assert
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
            ex3.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void Constructor___Should_not_throw___When_the_value_parameter_is_greater_than_or_equal_MinPercentChange_and_less_than_or_equal_MaxPercentChange()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => new PercentChangeAsDouble(PercentChangeAsDouble.MinPercentChange));
            var ex2 = Record.Exception(() => new PercentChangeAsDouble(PercentChangeAsDouble.MaxPercentChange));
            var ex3 = Record.Exception(() => new PercentChangeAsDouble((PercentChangeAsDouble.MinPercentChange + PercentChangeAsDouble.MaxPercentChange) / 2d));

            // Assert
            ex1.Should().BeNull();
            ex2.Should().BeNull();
            ex3.Should().BeNull();
        }

        [Fact]
        public static void Value___Should_return_the_same_value_passed_to_constructor___When_getting()
        {
            // Arrange
            var expectedDouble = (PercentChangeAsDouble.MinPercentChange + PercentChangeAsDouble.MaxPercentChange) / 2d;
            var systemUnderTest = new PercentChangeAsDouble(expectedDouble);

            // Act
            var actualDouble = systemUnderTest.Value;

            // Assert
            actualDouble.Should().Be(expectedDouble);
        }

        [Fact]
        public static void Cast___Should_return_the_same_value_passed_to_constructor___When_casting_to_double()
        {
            // Arrange
            var expectedDouble = (PercentChangeAsDouble.MinPercentChange + PercentChangeAsDouble.MaxPercentChange) / 2d;
            var systemUnderTest = new PercentChangeAsDouble(expectedDouble);

            // Act
            var actualDouble = (double)systemUnderTest;

            // Assert
            actualDouble.Should().Be(expectedDouble);
        }

        [Fact]
        public static void Implicit_conversion___Should_return_same_int_value_passed_to_constructor___When_converting_to_object_of_type_int()
        {
            // Arrange
            var expectedDouble = (PercentChangeAsDouble.MinPercentChange + PercentChangeAsDouble.MaxPercentChange) / 2d;
            var systemUnderTest = new PercentChangeAsDouble(expectedDouble);

            // Act
            double actualDouble = systemUnderTest;

            // Assert
            actualDouble.Should().Be(expectedDouble);
        }
    }
}

// ReSharper restore InconsistentNaming
// ReSharper restore CheckNamespace