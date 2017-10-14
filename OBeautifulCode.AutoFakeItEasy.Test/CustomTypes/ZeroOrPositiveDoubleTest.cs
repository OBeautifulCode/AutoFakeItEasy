// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZeroOrPositiveDoubleTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
namespace OBeautifulCode.AutoFakeItEasy.Test
{
    using System;

    using FluentAssertions;

    using OBeautifulCode.Math.Recipes;

    using Xunit;

    /// <summary>
    /// Tests the <see cref="ZeroOrPositiveDouble"/> class.
    /// </summary>
    public static class ZeroOrPositiveDoubleTest
    {
        [Fact]
        public static void Constructor___Should_throw_ArgumentOutOfRangeException___When_the_value_parameter_is_negative()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => new ZeroOrPositiveDouble(-.000001));
            var ex2 = Record.Exception(() => new ZeroOrPositiveDouble(int.MinValue));
            var ex3 = Record.Exception(() => new ZeroOrPositiveDouble(-1d * Math.Abs(ThreadSafeRandom.NextDouble())));

            // Assert
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
            ex3.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void Constructor___Should_not_throw___When_the_value_parameter_is_zero_or_positive()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => new ZeroOrPositiveDouble(0));
            var ex2 = Record.Exception(() => new ZeroOrPositiveDouble(1));
            var ex3 = Record.Exception(() => new ZeroOrPositiveDouble(int.MaxValue));
            var ex4 = Record.Exception(() => new ZeroOrPositiveDouble(Math.Abs(ThreadSafeRandom.NextDouble())));

            // Assert
            ex1.Should().BeNull();
            ex2.Should().BeNull();
            ex3.Should().BeNull();
            ex4.Should().BeNull();
        }

        [Fact]
        public static void Value___Should_return_the_same_value_passed_to_constructor___When_getting()
        {
            // Arrange
            var expectedDouble = Math.Abs(ThreadSafeRandom.NextDouble());
            var systemUnderTest = new ZeroOrPositiveDouble(expectedDouble);

            // Act
            var actualDouble = systemUnderTest.Value;

            // Assert
            actualDouble.Should().Be(expectedDouble);
        }

        [Fact]
        public static void Cast___Should_return_the_same_value_passed_to_constructor___When_casting_to_int()
        {
            // Arrange
            var expectedDouble = Math.Abs(ThreadSafeRandom.NextDouble());
            var systemUnderTest = new ZeroOrPositiveDouble(expectedDouble);

            // Act
            var actualDouble = (double)systemUnderTest;

            // Assert
            actualDouble.Should().Be(expectedDouble);
        }

        [Fact]
        public static void Implicit_conversion___Should_return_same_int_value_passed_to_constructor___When_converting_to_object_of_type_int()
        {
            // Arrange
            var expectedDouble = Math.Abs(ThreadSafeRandom.NextDouble());
            var systemUnderTest = new ZeroOrPositiveDouble(expectedDouble);

            // Act
            double actualDouble = systemUnderTest;

            // Assert
            actualDouble.Should().Be(expectedDouble);
        }
    }
}

// ReSharper restore InconsistentNaming
// ReSharper restore CheckNamespace