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
    /// Tests the <see cref="AutoFixtureBackedDummyFactory"/> class.
    /// </summary>
    public static class AutoFixtureBackedDummyFactoryTest
    {
        private const int NumberOfCallsToCoverAllShortsRegardlessOfFixtureState = short.MaxValue * 4;

        private static int NumberOfCallsToCoverAllPercentChangeAsDouble => Convert.ToInt32(((PercentChangeAsDouble.MaxPercentChange - PercentChangeAsDouble.MinPercentChange) * 1000d) + 1d);

        private static int NumberOfCallsToCoverAllPercentChangeAsDecimal => Convert.ToInt32(((PercentChangeAsDecimal.MaxPercentChange - PercentChangeAsDecimal.MinPercentChange) * 1000m) + 1m);

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
        public static void ADummy_enum___Should_return_enum_values_in_random_order___When_creating_objects_of_type_enum()
        {
            // Arrange
            var enumValuesCount = Enum.GetValues(typeof(Number)).Length;
            var enumValuesInOrder = Enum.GetValues(typeof(Number)).Cast<Number>();

            // Act
            var randomEnumValues = Enumerable.Range(1, enumValuesCount).Select(_ => A.Dummy<Number>()).ToList();

            // Assert
            randomEnumValues.Should().NotEqual(enumValuesInOrder);
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

        [Fact]
        public static void ADummy_PositiveDouble___Should_always_return_positive_doubles_less_than_or_equal_to_32768___When_creating_many_object_of_type_PositiveDouble()
        {
            // Arrange, Act
            var actualResult = Enumerable.Range(1, NumberOfCallsToCoverAllShortsRegardlessOfFixtureState).Select(_ => (double)A.Dummy<PositiveDouble>()).ToList();

            // Assert
            actualResult.ForEach(_ => _.Should().BeGreaterThan(0d).And.BeLessOrEqualTo(32768d));
        }

        [Fact]
        public static void ADummy_PositiveDouble___Should_return_every_number_in_range_of_1_to_32768_inclusive_at_least_once___When_creating_many_object_of_type_PositiveDouble()
        {
            // Arrange, Act
            var actualResult = Enumerable.Range(1, NumberOfCallsToCoverAllShortsRegardlessOfFixtureState).Select(_ => (double)A.Dummy<PositiveDouble>()).ToList();

            // Assert
            for (double i = 1; i <= 32768; i++)
            {
                actualResult.Should().Contain(i);
            }
        }

        [Fact]
        public static void ADummy_ZeroOrPositiveDouble___Should_always_return_zero_or_positive_doubles_less_than_or_equal_to_32768___When_creating_many_object_of_type_ZeroOrPositiveDouble()
        {
            // Arrange, Act
            var actualResult = Enumerable.Range(1, NumberOfCallsToCoverAllShortsRegardlessOfFixtureState).Select(_ => (double)A.Dummy<ZeroOrPositiveDouble>()).ToList();

            // Assert
            actualResult.ForEach(_ => _.Should().BeGreaterOrEqualTo(0d).And.BeLessOrEqualTo(32768d));
        }

        [Fact]
        public static void ADummy_ZeroOrPositiveDouble___Should_returns_every_number_in_range_of_0_to_32768_inclusive_at_least_once___When_creating_many_object_of_type_ZeroOrPositiveDouble()
        {
            // Arrange, Act
            var actualResult = Enumerable.Range(1, NumberOfCallsToCoverAllShortsRegardlessOfFixtureState).Select(_ => (double)A.Dummy<ZeroOrPositiveDouble>()).ToList();

            // Assert
            for (double i = 0d; i <= 32768d; i++)
            {
                actualResult.Should().Contain(i);
            }
        }

        [Fact]
        public static void ADummy_NegativeDouble___Should_always_return_negative_doubles_greater_than_or_equal_to_negative_32768___When_creating_many_object_of_type_NegativeDouble()
        {
            // Arrange, Act
            var actualResult = Enumerable.Range(1, NumberOfCallsToCoverAllShortsRegardlessOfFixtureState).Select(_ => (double)A.Dummy<NegativeDouble>()).ToList();

            // Assert
            actualResult.ForEach(_ => _.Should().BeGreaterOrEqualTo(-32768d).And.BeLessThan(0d));
        }

        [Fact]
        public static void ADummy_NegativeDouble___Should_returns_every_number_in_range_of_1_to_32768_inclusive_at_least_once___When_creating_many_object_of_type_NegativeDouble()
        {
            // Arrange, Act
            var actualResult = Enumerable.Range(1, NumberOfCallsToCoverAllShortsRegardlessOfFixtureState).Select(_ => (double)A.Dummy<NegativeDouble>()).ToList();

            // Assert
            for (double i = -32768d; i <= -1; i++)
            {
                actualResult.Should().Contain(i);
            }
        }

        [Fact]
        public static void ADummy_ZeroOrNegativeDouble___Should_always_return_zero_or_negative_doubles_greater_than_or_equal_to_negative_32768___When_creating_many_object_of_type_ZeroOrNegativeDouble()
        {
            // Arrange, Act
            var actualResult = Enumerable.Range(1, NumberOfCallsToCoverAllShortsRegardlessOfFixtureState).Select(_ => (double)A.Dummy<ZeroOrNegativeDouble>()).ToList();

            // Assert
            actualResult.ForEach(_ => _.Should().BeGreaterOrEqualTo(-32768d).And.BeLessOrEqualTo(0d));
        }

        [Fact]
        public static void ADummy_ZeroNegativeDouble___Should_returns_every_number_in_range_of_0_to_32768_inclusive_at_least_once___When_creating_many_object_of_type_ZeroOrNegativeDouble()
        {
            // Arrange, Act
            var actualResult = Enumerable.Range(1, NumberOfCallsToCoverAllShortsRegardlessOfFixtureState).Select(_ => (double)A.Dummy<ZeroOrNegativeDouble>()).ToList();

            // Assert
            for (double i = -32768d; i <= 0d; i++)
            {
                actualResult.Should().Contain(i);
            }
        }

        [Fact]
        public static void ADummy_PercentChangeAsDouble___Should_always_return_doubles_that_are_greater_than_or_equal_MinPercentChange_and_less_than_or_equal_MaxPercentChange___When_creating_many_object_of_type_PercentChangeAsDouble()
        {
            // Arrange, Act
            var actualResult = Enumerable.Range(1, NumberOfCallsToCoverAllPercentChangeAsDouble).Select(_ => (double)A.Dummy<PercentChangeAsDouble>()).ToList();

            // Assert
            actualResult.ForEach(_ => _.Should().BeGreaterOrEqualTo(PercentChangeAsDouble.MinPercentChange).And.BeLessOrEqualTo(PercentChangeAsDouble.MaxPercentChange));
        }

        [Fact]
        public static void ADummy_PercentChangeAsDecimal___Should_always_return_decimals_that_are_greater_than_or_equal_MinPercentChange_and_less_than_or_equal_MaxPercentChange___When_creating_many_object_of_type_PercentChangeAsDecimal()
        {
            // Arrange, Act
            var actualResult = Enumerable.Range(1, NumberOfCallsToCoverAllPercentChangeAsDecimal).Select(_ => (decimal)A.Dummy<PercentChangeAsDecimal>()).ToList();

            // Assert
            actualResult.ForEach(_ => _.Should().BeGreaterOrEqualTo(PercentChangeAsDecimal.MinPercentChange).And.BeLessOrEqualTo(PercentChangeAsDecimal.MaxPercentChange));
        }

        [Fact]
        public static void ADummy___Should_use_custom_dummy_creator___When_custom_dummy_creator_for_concrete_type_is_added()
        {
            // Arrange
            var dummyBeforeCustomization = A.Dummy<UseCustomDummyCreatorFuncForConcreteType>();
            AutoFixtureBackedDummyFactory.AddDummyCreator(() => new UseCustomDummyCreatorFuncForConcreteType(int.MinValue));

            // Act
            var dummyAfterCutomization = A.Dummy<UseCustomDummyCreatorFuncForConcreteType>();

            // Assert
            dummyBeforeCustomization.Value.Should().NotBe(int.MinValue);
            dummyAfterCutomization.Value.Should().Be(int.MinValue);
        }

        [Fact]
        public static void ADummy___Should_use_custom_dummy_creator___When_custom_dummy_creator_for_abstract_type_is_added()
        {
            // Arrange
            // note: this line will generate a Fake<UseCustomDummyCreatorFuncForAbstractType> because our factory will return false
            // when FakeItEasy calls CanCreate().  FakeItEasy will cache this information.  If we subsequently register a dummy creator
            // for the type, it won't matter because FakeItEasy won't use our factory for this type.
            // var dummyBeforeCustomization = A.Dummy<UseCustomDummyCreatorFuncForAbstractType>();
            AutoFixtureBackedDummyFactory.AddDummyCreator<UseCustomDummyCreatorFuncForAbstractType>(() => new UseCustomDummyCreatorFuncForAbstractTypeReturnedType(int.MinValue, int.MaxValue));

            // Act
            var dummyAfterCutomization = A.Dummy<UseCustomDummyCreatorFuncForAbstractType>();

            // Assert
            dummyAfterCutomization.AbstractValue.Should().Be(int.MinValue);
            dummyAfterCutomization.Should().BeOfType<UseCustomDummyCreatorFuncForAbstractTypeReturnedType>();
            ((UseCustomDummyCreatorFuncForAbstractTypeReturnedType)dummyAfterCutomization).ConcreteValue.Should().Be(int.MaxValue);
        }

        [Fact]
        public static void ADummy___Should_use_custom_dummy_creator___When_custom_dummy_creator_for_interface_type_is_added()
        {
            // Arrange
            // note: this line will generate a Fake<IUseCustomDummyCreatorFuncForInterfaceType> because our factory will return false
            // when FakeItEasy calls CanCreate().  FakeItEasy will cache this information.  If we subsequently register a dummy creator
            // for the type, it won't matter because FakeItEasy won't use our factory for this type.
            // var dummyBeforeCustomization = A.Dummy<IUseCustomDummyCreatorFuncForInterfaceType>();
            AutoFixtureBackedDummyFactory.AddDummyCreator<IUseCustomDummyCreatorFuncForInterfaceType>(() => new UseCustomDummyCreatorFuncForInterfaceType(int.MaxValue));

            // Act
            var dummyAfterCutomization = A.Dummy<IUseCustomDummyCreatorFuncForInterfaceType>();

            // Assert
            dummyAfterCutomization.Value.Should().Be(int.MaxValue);
            dummyAfterCutomization.Should().BeOfType<UseCustomDummyCreatorFuncForInterfaceType>();
        }

        [Fact]
        public static void ADummy___Should_use_the_most_recently_added_custom_dummy_creator___When_custom_dummy_creator_is_added_twice_for_same_type()
        {
            // Arrange
            AutoFixtureBackedDummyFactory.AddDummyCreator(() => new UseMostRecentlyAddedCustomDummyCreatorFunc(int.MinValue));
            var dummyAfterFirstCustomization = A.Dummy<UseMostRecentlyAddedCustomDummyCreatorFunc>();
            AutoFixtureBackedDummyFactory.AddDummyCreator(() => new UseMostRecentlyAddedCustomDummyCreatorFunc(int.MaxValue));

            // Act
            var dummyAfterSecondCustomization = A.Dummy<UseMostRecentlyAddedCustomDummyCreatorFunc>();

            // Assert
            dummyAfterFirstCustomization.Value.Should().Be(int.MinValue);
            dummyAfterSecondCustomization.Value.Should().Be(int.MaxValue);
        }

        [Fact]
        public static void AddDummyCreator___Should_not_throw___When_specifying_non_abstract_class()
        {
            // Arrange, Act
            var ex = Record.Exception(() => AutoFixtureBackedDummyFactory.AddDummyCreator(() => new CustomDummyDoesNotThrowWhenCreated(int.MinValue)));

            // Assert
            ex.Should().BeNull();
        }

        [Fact]
        public static void AddDummyCreator___Should_not_throw___When_specifying_creator_func_for_same_type_twice()
        {
            // Arrange
            AutoFixtureBackedDummyFactory.AddDummyCreator(() => new CustomDummyDoesNotThrowWhenRegisteredTwice(int.MaxValue));

            // Act
            var ex = Record.Exception(() => AutoFixtureBackedDummyFactory.AddDummyCreator(() => new CustomDummyDoesNotThrowWhenRegisteredTwice(int.MinValue)));

            // Assert
            ex.Should().BeNull();
        }

        [Fact]
        public static void UseRandomConcreteSubclassForDummy___Should_throw_ArgumentException___When_there_are_no_concrete_subclasses_of_specified_type()
        {
            // Arrange, Act
            var ex = Record.Exception(() => AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<Dog>());

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void UseRandomConcreteSubclassForDummy___Should_not_throw___When_there_are_concrete_subclasses_of_specified_type()
        {
            // Arrange, Act
            var ex = Record.Exception(() => AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<Animal>());

            // Assert
            ex.Should().BeNull();
        }

        [Fact]
        public static void UseRandomConcreteSubclassForDummy___Should_not_throw___When_called_twice_for_same_type()
        {
            // Arrange
            AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<Animal>();

            // Act
            var ex = Record.Exception(() => AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<Animal>());

            // Assert
            ex.Should().BeNull();
        }

        [Fact]
        public static void ADummy___Should_return_random_Dog_or_Lion_or_Zebra___When_UseRandomConcreteSubclassForDummy_is_called_for_type_Animal()
        {
            // Arrange
            AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<Animal>();
            var animals = new List<Animal>();

            // Act
            for (int i = 0; i < 100; i++)
            {
                var animal = A.Dummy<Animal>();
                animals.Add(animal);
            }

            // Assert
            animals.OfType<Dog>().Count().Should().BeGreaterThan(1);
            animals.OfType<Lion>().Count().Should().BeGreaterThan(1);
            animals.OfType<Zebra>().Count().Should().BeGreaterThan(1);
        }

        // ReSharper restore InconsistentNaming
    }
}
