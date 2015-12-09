// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZeroOrPositiveIntegerTest.cs" company="OBeautifulCode">
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
    /// Tests the <see cref="ZeroOrPositiveInteger"/> class.
    /// </summary>
    public static class ZeroOrPositiveIntegerTest
    {
        [Fact]
        public static void Constructor___Should_throw_ArgumentOutOfRangeException___When_the_value_parameter_is_negative()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => new ZeroOrPositiveInteger(-1));
            var ex2 = Record.Exception(() => new ZeroOrPositiveInteger(int.MinValue));
            var ex3 = Record.Exception(() => new ZeroOrPositiveInteger(ThreadSafeRandom.Next(int.MinValue, -1)));

            // Assert
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
            ex3.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void Constructor___Should_not_throw___When_the_value_parameter_is_zero_or_positive()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => new ZeroOrPositiveInteger(0));
            var ex2 = Record.Exception(() => new ZeroOrPositiveInteger(1));
            var ex3 = Record.Exception(() => new ZeroOrPositiveInteger(int.MaxValue));
            var ex4 = Record.Exception(() => new ZeroOrPositiveInteger(ThreadSafeRandom.Next(0, int.MaxValue)));

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
            var expectedInt = ThreadSafeRandom.Next(1, int.MaxValue);
            var systemUnderTest = new ZeroOrPositiveInteger(expectedInt);

            // Act
            var actualInt = systemUnderTest.Value;

            // Assert
            actualInt.Should().Be(expectedInt);
        }

        [Fact]
        public static void Cast___Should_return_the_same_value_passed_to_constructor___When_casting_to_int()
        {
            // Arrange
            var expectedInt = ThreadSafeRandom.Next(1, int.MaxValue);
            var systemUnderTest = new ZeroOrPositiveInteger(expectedInt);

            // Act
            var actualInt = (int)systemUnderTest;

            // Assert
            actualInt.Should().Be(expectedInt);
        }

        [Fact]
        public static void Implicit_conversion___Should_return_same_int_value_passed_to_constructor___When_converting_to_object_of_type_int()
        {
            // Arrange
            var expectedInt = ThreadSafeRandom.Next(0, int.MaxValue);
            var systemUnderTest = new ZeroOrPositiveInteger(expectedInt);

            // Act
            int actualInt = systemUnderTest;

            // Assert
            actualInt.Should().Be(expectedInt);
        }

        [Fact]
        public static void ToInt___Should_return_the_same_value_passed_to_constructor___When_called()
        {
            // Arrange
            var expectedInt = ThreadSafeRandom.Next(0, int.MaxValue);
            var systemUnderTest = new ZeroOrPositiveInteger(expectedInt);

            // Act
            var actualInt = ConstrainedInteger.ToInt(systemUnderTest);

            // Assert
            actualInt.Should().Be(expectedInt);
        }
    }
}

// ReSharper restore InconsistentNaming
// ReSharper restore CheckNamespace