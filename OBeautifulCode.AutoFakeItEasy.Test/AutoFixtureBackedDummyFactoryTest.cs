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
        private const int NumberOfCallsToCoverAllShortsRegardlessOfFixtureState = short.MaxValue * 3;

        // ReSharper disable InconsistentNaming
        [Fact]
        public static void ADummy_bool___Should_return_bool_values_in_random_order___When_creating_object_of_type_bool()
        {
            // Arrange
            var sequentialBools1 = new List<bool> { true, false, true, false, true, false, true, false };
            var sequentialBools2 = new List<bool> { false, true, false, true, false, true, false, true };

            // Act
            var actualResult = Enumerable.Range(1, sequentialBools1.Count).Select(_ => A.Dummy<bool>()).ToList();

            // Assert
            actualResult.Should().NotEqual(sequentialBools1);
            actualResult.Should().NotEqual(sequentialBools2);
        }

        [Fact]
        public static void ADummy_bool___Should_return_both_true_and_false_at_least_once___When_creating_many_objects_of_type_bool()
        {
            // Arrange, Act
            var actualResult = Enumerable.Range(1, 1000).Select(_ => A.Dummy<bool>()).ToList();

            // Assert
            actualResult.Should().Contain(true);
            actualResult.Should().Contain(false);
        }

        [Fact]
        public static void ADummy_bool___Should_return_enum_values_in_random_order___When_creating_objects_of_type_enum()
        {
            // Arrange
            var enumValuesCount = Enum.GetValues(typeof(Number)).Length;

            // Act
            var randomEnumValues = Enumerable.Range(1, enumValuesCount).Select(_ => A.Dummy<Number>()).ToList();

            // Assert
            randomEnumValues.Should().NotBeAscendingInOrder();
        }

        [Fact]
        public static void ADummy_enum___Should_returns_all_enum_values_at_least_once___When_creating_many_enums()
        {
            // Arrange
            var allEnumValues = Enum.GetValues(typeof(Number)).Cast<Number>();

            // Act
            var actualResult = Enumerable.Range(1, 1000).Select(_ => A.Dummy<Number>()).ToList();

            // Assert
            foreach (var enumValue in allEnumValues)
            {
                actualResult.Should().Contain(enumValue);
            }
        }

        [Fact]
        public static void ADummy_int___Should_always_return_integers_greater_than_or_equal_to_negative_32768_and_less_than_or_equal_to_32786____When_creating_many_object_of_type_int()
        {
            // Arrange, Act
            var actualResult = Enumerable.Range(1, NumberOfCallsToCoverAllShortsRegardlessOfFixtureState).Select(_ => A.Dummy<int>()).ToList();

            // Assert
            actualResult.ForEach(_ => _.Should().BeGreaterOrEqualTo(-32768).And.BeLessOrEqualTo(32768));
        }

        [Fact]
        public static void ADummy_int___Should_returns_every_number_in_range_of_negative_32768_to_32768_inclusive_at_least_once___When_creating_many_object_of_type_int()
        {
            // Arrange, Act
            var actualResult = Enumerable.Range(1, NumberOfCallsToCoverAllShortsRegardlessOfFixtureState).Select(_ => A.Dummy<int>()).ToList();

            // Assert
            for (int i = -32768; i <= 32768; i++)
            {
                actualResult.Should().Contain(i);
            }
        }

        [Fact]
        public static void ADummy_byte___Should_returns_every_possible_byte_value_at_least_once___When_creating_many_object_of_type_byte()
        {
            // Arrange, Act
            var actualResult = Enumerable.Range(1, byte.MaxValue * 20).Select(_ => A.Dummy<byte>()).ToList();

            // Assert
            for (int i = byte.MinValue; i <= 255; i++)
            {
                actualResult.Should().Contain((byte)i);
            }
        }

        [Fact]
        public static void ADummy_PositiveInteger___Should_always_return_positive_integers_less_than_or_equal_to_32768___When_creating_many_object_of_type_PositiveInteger()
        {
            // Arrange, Act
            var actualResult = Enumerable.Range(1, NumberOfCallsToCoverAllShortsRegardlessOfFixtureState).Select(_ => (int)A.Dummy<PositiveInteger>()).ToList();

            // Assert
            actualResult.ForEach(_ => _.Should().BeGreaterThan(0).And.BeLessOrEqualTo(32768));
        }

        [Fact]
        public static void ADummy_PositiveInteger___Should_return_every_number_in_range_of_1_to_32768_inclusive_at_least_once___When_creating_many_object_of_type_PositiveInteger()
        {
            // Arrange, Act
            var actualResult = Enumerable.Range(1, NumberOfCallsToCoverAllShortsRegardlessOfFixtureState).Select(_ => (int)A.Dummy<PositiveInteger>()).ToList();

            // Assert
            for (int i = 1; i <= 32768; i++)
            {
                actualResult.Should().Contain(i);
            }
        }

        [Fact]
        public static void ADummy_ZeroOrPositiveInteger___Should_always_return_zero_or_positive_integers_less_than_or_equal_to_32768___When_creating_many_object_of_type_ZeroOrPositiveInteger()
        {
            // Arrange, Act
            var actualResult = Enumerable.Range(1, NumberOfCallsToCoverAllShortsRegardlessOfFixtureState).Select(_ => (int)A.Dummy<ZeroOrPositiveInteger>()).ToList();

            // Assert
            actualResult.ForEach(_ => _.Should().BeGreaterOrEqualTo(0).And.BeLessOrEqualTo(32768));
        }

        [Fact]
        public static void ADummy_ZeroOrPositiveInteger___Should_returns_every_number_in_range_of_0_to_32768_inclusive_at_least_once___When_creating_many_object_of_type_ZeroOrPositiveInteger()
        {
            // Arrange, Act
            var actualResult = Enumerable.Range(1, NumberOfCallsToCoverAllShortsRegardlessOfFixtureState).Select(_ => (int)A.Dummy<ZeroOrPositiveInteger>()).ToList();

            // Assert
            for (int i = 0; i <= 32768; i++)
            {
                actualResult.Should().Contain(i);
            }
        }

        [Fact]
        public static void ADummy_NegativeInteger___Should_always_return_negative_integers_greater_than_or_equal_to_negative_32768___When_creating_many_object_of_type_NegativeInteger()
        {
            // Arrange, Act
            var actualResult = Enumerable.Range(1, NumberOfCallsToCoverAllShortsRegardlessOfFixtureState).Select(_ => (int)A.Dummy<NegativeInteger>()).ToList();

            // Assert
            actualResult.ForEach(_ => _.Should().BeGreaterOrEqualTo(-32768).And.BeLessThan(0));
        }

        [Fact]
        public static void ADummy_NegativeInteger___Should_returns_every_number_in_range_of_1_to_32768_inclusive_at_least_once___When_creating_many_object_of_type_NegativeInteger()
        {
            // Arrange, Act
            var actualResult = Enumerable.Range(1, NumberOfCallsToCoverAllShortsRegardlessOfFixtureState).Select(_ => (int)A.Dummy<NegativeInteger>()).ToList();

            // Assert
            for (int i = -32768; i <= -1; i++)
            {
                actualResult.Should().Contain(i);
            }
        }

        [Fact]
        public static void ADummy_ZeroOrNegativeInteger___Should_always_return_zero_or_negative_integers_greater_than_or_equal_to_negative_32768___When_creating_many_object_of_type_ZeroOrNegativeInteger()
        {
            // Arrange, Act
            var actualResult = Enumerable.Range(1, NumberOfCallsToCoverAllShortsRegardlessOfFixtureState).Select(_ => (int)A.Dummy<ZeroOrNegativeInteger>()).ToList();

            // Assert
            actualResult.ForEach(_ => _.Should().BeGreaterOrEqualTo(-32768).And.BeLessOrEqualTo(0));
        }

        [Fact]
        public static void ADummy_ZeroNegativeInteger___Should_returns_every_number_in_range_of_0_to_32768_inclusive_at_least_once___When_creating_many_object_of_type_ZeroOrNegativeInteger()
        {
            // Arrange, Act
            var actualResult = Enumerable.Range(1, NumberOfCallsToCoverAllShortsRegardlessOfFixtureState).Select(_ => (int)A.Dummy<ZeroOrNegativeInteger>()).ToList();

            // Assert
            for (int i = -32768; i <= 0; i++)
            {
                actualResult.Should().Contain(i);
            }
        }

        // ReSharper restore InconsistentNaming
    }
}
