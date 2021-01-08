// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UtcDateTimeTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy.Test
{
    using System;

    using FluentAssertions;

    using Xunit;

    public static class UtcDateTimeTest
    {
        [Fact]
        public static void Constructor___Should_throw_ArgumentException___When_the_value_parameter_Kind_is_not_Utc()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => new UtcDateTime(DateTime.Now));
            var ex2 = Record.Exception(() => new UtcDateTime(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local)));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void Constructor___Should_not_throw___When_the_value_parameter_is_Utc_DateTime()
        {
            // Arrange, Act
            var ex = Record.Exception(() => new UtcDateTime(DateTime.UtcNow));

            // Assert
            ex.Should().BeNull();
        }

        [Fact]
        public static void Value___Should_return_the_same_value_passed_to_constructor___When_getting()
        {
            // Arrange
            var expected = DateTime.UtcNow;
            var systemUnderTest = new UtcDateTime(expected);

            // Act
            var actual = systemUnderTest.Value;

            // Assert
            actual.Should().Be(expected);
            actual.Kind.Should().Be(DateTimeKind.Utc);
        }

        [Fact]
        public static void Cast___Should_return_the_same_value_passed_to_constructor___When_casting_to_DateTime()
        {
            // Arrange
            var expected = DateTime.UtcNow;
            var systemUnderTest = new UtcDateTime(expected);

            // Act
            var actual = (DateTime)systemUnderTest;

            // Assert
            actual.Should().Be(expected);
            actual.Kind.Should().Be(DateTimeKind.Utc);
        }

        [Fact]
        public static void Implicit_conversion___Should_return_same_double_value_passed_to_constructor___When_converting_to_object_of_type_double()
        {
            // Arrange
            var expected = DateTime.UtcNow;
            var systemUnderTest = new UtcDateTime(expected);

            // Act
            DateTime actual = systemUnderTest;

            // Assert
            actual.Should().Be(expected);
            actual.Kind.Should().Be(DateTimeKind.Utc);
        }
    }
}