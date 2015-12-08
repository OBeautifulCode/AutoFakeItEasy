// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NegativeIntegerTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
namespace OBeautifulCode.AutoFakeItEasy.Test
{
    using System;

    using FluentAssertions;

    using OBeautifulCode.Math;

    using Xunit;

    /// <summary>
    /// Tests the <see cref="NegativeInteger"/> class.
    /// </summary>
    public static class NegativeIntegerTest
    {
        [Fact]
        public static void Constructor_When_the_value_parameter_is_zero_or_positive_Then_constructor_throws_ArgumentOutOfRangeException()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => new NegativeInteger(0));
            var ex2 = Record.Exception(() => new NegativeInteger(1));
            var ex3 = Record.Exception(() => new NegativeInteger(int.MaxValue));
            var ex4 = Record.Exception(() => new NegativeInteger(ThreadSafeRandom.Next(0, int.MaxValue)));

            // Assert
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
            ex3.Should().BeOfType<ArgumentOutOfRangeException>();
            ex4.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void Constructor_When_the_value_parameter_is_negative_Then_constructor_does_not_throw()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => new NegativeInteger(-1));
            var ex2 = Record.Exception(() => new NegativeInteger(int.MinValue));
            var ex3 = Record.Exception(() => new NegativeInteger(ThreadSafeRandom.Next(int.MinValue, -1)));

            // Assert
            ex1.Should().BeNull();
            ex2.Should().BeNull();
            ex3.Should().BeNull();
        }

        [Fact]
        public static void Value_When_the_object_has_been_constructed_Then_Value_returns_the_same_value_passed_to_constructor()
        {
            // Arrange
            var expectedInt = ThreadSafeRandom.Next(int.MinValue, -1);
            var systemUnderTest = new NegativeInteger(expectedInt);

            // Act
            var actualInt = systemUnderTest.Value;

            // Assert
            actualInt.Should().Be(expectedInt);
        }

        [Fact]
        public static void Cast_When_casting_to_int_Then_the_resulting_int_is_the_same_value_passed_to_constructor()
        {
            // Arrange
            var expectedInt = ThreadSafeRandom.Next(int.MinValue, -1);
            var systemUnderTest = new NegativeInteger(expectedInt);

            // Act
            var actualInt = systemUnderTest.Value;

            // Assert
            actualInt.Should().Be(expectedInt);
        }
    }
}

// ReSharper restore InconsistentNaming
// ReSharper restore CheckNamespace