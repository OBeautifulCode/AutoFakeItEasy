// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoFixtureBackedDummyFactoryTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy.Test
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using FakeItEasy;
    using FluentAssertions;

    using Xunit;

    /// <summary>
    /// Tests the <see cref="AutoFixtureBackedDummyFactory"/> class.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Testing the dummy factory requires using many types.")]
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
            var sequentialBools1 = new List<bool> { true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false };
            var sequentialBools2 = new List<bool> { false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true };

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
        public static void UseRandomConcreteSubclassForDummy___Should_throw_ArgumentException___When_both_parameter_typesToInclude_and_typesToExclude_are_not_null()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<Cake>(typesToExclude: new[] { typeof(StrawberryShortcake) }, typesToInclude: new Type[] { }));
            var ex2 = Record.Exception(() => AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<Cake>(typesToExclude: new[] { typeof(StrawberryShortcake) }, typesToInclude: new[] { typeof(ChocolateCake) }));
            var ex3 = Record.Exception(() => AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<Cake>(typesToExclude: new Type[] { }, typesToInclude: new[] { typeof(ChocolateCake) }));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
            ex3.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void UseRandomConcreteSubclassForDummy___Should_throw_ArgumentException___When_parameter_typesToExclude_is_not_null_but_empty()
        {
            // Arrange, Act
            var ex = Record.Exception(() => AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<Cake>(typesToExclude: new Type[] { }));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void UseRandomConcreteSubclassForDummy___Should_throw_ArgumentException___When_parameter_typesToInclude_is_not_null_but_empty()
        {
            // Arrange, Act
            var ex = Record.Exception(() => AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<Cake>(typesToInclude: new Type[] { }));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
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
        public static void UseRandomConcreteSubclassForDummy___Should_throw_ArgumentException___When_all_concrete_subclasses_of_specified_type_are_specified_in_parameter_typesToExclude()
        {
            // Arrange, Act
            var ex = Record.Exception(() => AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<Wine>(typesToExclude: new[] { typeof(Pinonoir), typeof(Shiraz), typeof(Malbec) }));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void UseRandomConcreteSubclassForDummy___Should_throw_ArgumentException___When_no_concrete_subclasses_of_specified_type_are_specified_in_parameter_typesToInclude()
        {
            // Arrange, Act
            var ex = Record.Exception(() => AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<Beer>(typesToInclude: new[] { typeof(Pinonoir) }));

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

        [Fact]
        public static void ADummy___Should_return_all_TwoWheelers_except_Bicycle_and_Moped___When_UseRandomConcreteSubclassForDummy_is_called_for_type_TwoWheeler_and_Bicycle_and_Moped_are_specified_in_parameter_typesToExclude()
        {
            // Arrange
            AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<TwoWheelers>(typesToExclude: new[] { typeof(Bicycle), typeof(Moped) });
            var twoWheelers = new List<TwoWheelers>();

            // Act
            for (int i = 0; i < 100; i++)
            {
                var twoWheeler = A.Dummy<TwoWheelers>();
                twoWheelers.Add(twoWheeler);
            }

            // Assert
            twoWheelers.OfType<Motorcycle>().Should().NotBeEmpty();
            twoWheelers.OfType<Bicycle>().Should().BeEmpty();
            twoWheelers.OfType<Moped>().Should().BeEmpty();
            twoWheelers.OfType<Scooter>().Should().NotBeEmpty();
        }

        [Fact]
        public static void ADummy___Should_return_only_HighSchool_and_University___When_UseRandomConcreteSubclassForDummy_is_called_for_type_School_and_HighSchool_and_University_are_specified_in_parameter_typesToInclude()
        {
            // Arrange
            AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<School>(typesToInclude: new[] { typeof(HighSchool), typeof(University) });
            var schools = new List<School>();

            // Act
            for (int i = 0; i < 100; i++)
            {
                var school = A.Dummy<School>();
                schools.Add(school);
            }

            // Assert
            schools.OfType<HighSchool>().Should().NotBeEmpty();
            schools.OfType<Elementary>().Should().BeEmpty();
            schools.OfType<Postgraduate>().Should().BeEmpty();
            schools.OfType<University>().Should().NotBeEmpty();
        }

        [Fact]
        public static void ConstrainDummyToBeOneOf_without_IEqualityComparer___Should_use_most_recently_registered_constraint___When_called_multiple_times_for_same_type()
        {
            // Arrange
            AutoFixtureBackedDummyFactory.ConstrainDummyToBeOneOf(MostlyBadStuffWithoutComparerReestablished.JunkFood);
            AutoFixtureBackedDummyFactory.ConstrainDummyToBeOneOf(MostlyBadStuffWithoutComparerReestablished.Chores, MostlyBadStuffWithoutComparerReestablished.Hurricane);
            AutoFixtureBackedDummyFactory.ConstrainDummyToBeOneOf(MostlyBadStuffWithoutComparerReestablished.MeanPeople, MostlyBadStuffWithoutComparerReestablished.Tulips);
            var allStuff = new List<MostlyBadStuffWithoutComparerReestablished>();

            // Act
            for (int i = 0; i < 100; i++)
            {
                var mostlyBadStuff = A.Dummy<MostlyBadStuffWithoutComparerReestablished>();
                allStuff.Add(mostlyBadStuff);
            }

            // Assert
            allStuff = allStuff.Distinct().ToList();
            allStuff.Should().HaveCount(2);
            allStuff.Should().Contain(MostlyBadStuffWithoutComparerReestablished.MeanPeople);
            allStuff.Should().Contain(MostlyBadStuffWithoutComparerReestablished.Tulips);
        }

        [Fact]
        public static void ConstrainDummyToBeOneOf_without_IEqualityComparer___Should_return_only_values_within_set_of_possibleDummies___When_called_for_type_MostlyBadStuffWithoutComparer()
        {
            // Arrange
            AutoFixtureBackedDummyFactory.ConstrainDummyToBeOneOf(MostlyBadStuffWithoutComparer.Treasure, MostlyBadStuffWithoutComparer.Tulips, MostlyBadStuffWithoutComparer.WalkInThePark);
            var goodStuff = new List<MostlyBadStuffWithoutComparer>();

            // Act
            for (int i = 0; i < 100; i++)
            {
                var mostlyBadStuff = A.Dummy<MostlyBadStuffWithoutComparer>();
                goodStuff.Add(mostlyBadStuff);
            }

            // Assert
            goodStuff = goodStuff.Distinct().ToList();
            goodStuff.Should().HaveCount(3);
            goodStuff.Should().Contain(MostlyBadStuffWithoutComparer.Treasure);
            goodStuff.Should().Contain(MostlyBadStuffWithoutComparer.Tulips);
            goodStuff.Should().Contain(MostlyBadStuffWithoutComparer.WalkInThePark);
        }

        [Fact]
        public static void ConstrainDummyToBeOneOf_with_IEqualityComparer___Should_use_most_recently_registered_constraint___When_called_multiple_times_for_same_type()
        {
            // Arrange
            AutoFixtureBackedDummyFactory.ConstrainDummyToBeOneOf(new[] { MostlyBadStuffWithComparerReestablished.Hurricane }, EqualityComparer<MostlyBadStuffWithComparerReestablished>.Default);
            AutoFixtureBackedDummyFactory.ConstrainDummyToBeOneOf(new[] { MostlyBadStuffWithComparerReestablished.Chores, MostlyBadStuffWithComparerReestablished.Tulips }, EqualityComparer<MostlyBadStuffWithComparerReestablished>.Default);
            AutoFixtureBackedDummyFactory.ConstrainDummyToBeOneOf(new[] { MostlyBadStuffWithComparerReestablished.JunkFood, MostlyBadStuffWithComparerReestablished.WalkInThePark }, EqualityComparer<MostlyBadStuffWithComparerReestablished>.Default);
            var allStuff = new List<MostlyBadStuffWithComparerReestablished>();

            // Act
            for (int i = 0; i < 100; i++)
            {
                var mostlyBadStuff = A.Dummy<MostlyBadStuffWithComparerReestablished>();
                allStuff.Add(mostlyBadStuff);
            }

            // Assert
            allStuff = allStuff.Distinct().ToList();
            allStuff.Should().HaveCount(2);
            allStuff.Should().Contain(MostlyBadStuffWithComparerReestablished.JunkFood);
            allStuff.Should().Contain(MostlyBadStuffWithComparerReestablished.WalkInThePark);
        }

        [Fact]
        public static void ConstrainDummyToBeOneOf_with_IEqualityComparer___Should_return_only_values_within_set_of_possibleDummies___When_called_for_type_MostlyBadStuffWithComparer()
        {
            // Arrange
            AutoFixtureBackedDummyFactory.ConstrainDummyToBeOneOf(new[] { MostlyBadStuffWithComparer.Chores, MostlyBadStuffWithComparer.MeanPeople, MostlyBadStuffWithComparer.Hurricane }, EqualityComparer<MostlyBadStuffWithComparer>.Default);
            var goodStuff = new List<MostlyBadStuffWithComparer>();

            // Act
            for (int i = 0; i < 100; i++)
            {
                var mostlyBadStuff = A.Dummy<MostlyBadStuffWithComparer>();
                goodStuff.Add(mostlyBadStuff);
            }

            // Assert
            goodStuff = goodStuff.Distinct().ToList();
            goodStuff.Should().HaveCount(3);
            goodStuff.Should().Contain(MostlyBadStuffWithComparer.Chores);
            goodStuff.Should().Contain(MostlyBadStuffWithComparer.MeanPeople);
            goodStuff.Should().Contain(MostlyBadStuffWithComparer.Hurricane);
        }

        [Fact]
        public static void ConstrainDummyToBeOneOf___Should_construct_objects_with_constrained_properties___When_called_for_type_that_contains_constrained_types()
        {
            // Arrange
            AutoFixtureBackedDummyFactory.ConstrainDummyToBeOneOf(MostlyBadStuffWithoutComparerIndirect.Hurricane, MostlyBadStuffWithoutComparerIndirect.Chores, MostlyBadStuffWithoutComparerIndirect.MeanPeople);
            AutoFixtureBackedDummyFactory.ConstrainDummyToBeOneOf(new[] { MostlyBadStuffWithComparerIndirect.MeanPeople, MostlyBadStuffWithComparerIndirect.Tulips, MostlyBadStuffWithComparerIndirect.WalkInThePark }, EqualityComparer<MostlyBadStuffWithComparerIndirect>.Default);
            var results = new List<ConstrainDummiesToBeOneOfIndirect>();

            // Act
            for (int i = 0; i < 1000; i++)
            {
                var objectWithConstrainedProperties = A.Dummy<ConstrainDummiesToBeOneOfIndirect>();
                results.Add(objectWithConstrainedProperties);
            }

            // Assert
            var constrainedProperty1Values = results.Select(_ => _.MostlyBadStuffWithoutComparerIndirect).Distinct().ToList();
            constrainedProperty1Values.Should().HaveCount(3);
            constrainedProperty1Values.Should().Contain(MostlyBadStuffWithoutComparerIndirect.Hurricane);
            constrainedProperty1Values.Should().Contain(MostlyBadStuffWithoutComparerIndirect.Chores);
            constrainedProperty1Values.Should().Contain(MostlyBadStuffWithoutComparerIndirect.MeanPeople);

            var constrainedProperty2Values = results.Select(_ => _.MostlyBadStuffWithComparerIndirect).Distinct().ToList();
            constrainedProperty2Values.Should().HaveCount(3);
            constrainedProperty2Values.Should().Contain(MostlyBadStuffWithComparerIndirect.MeanPeople);
            constrainedProperty2Values.Should().Contain(MostlyBadStuffWithComparerIndirect.Tulips);
            constrainedProperty2Values.Should().Contain(MostlyBadStuffWithComparerIndirect.WalkInThePark);
        }

        [Fact]
        public static void ConstrainDummyToExclude_without_IEqualityComparer___Should_use_most_recently_registered_constraint___When_called_multiple_times_for_same_type()
        {
            // Arrange
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(MostlyGoodStuffWithoutComparerReestablished.Chocolate);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(MostlyGoodStuffWithoutComparerReestablished.Vacation, MostlyGoodStuffWithoutComparerReestablished.WorkingFromHome);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(MostlyGoodStuffWithoutComparerReestablished.Meditation, MostlyGoodStuffWithoutComparerReestablished.Bulldogs);
            var allStuff = new List<MostlyGoodStuffWithoutComparerReestablished>();

            // Act
            for (int i = 0; i < 5000; i++)
            {
                var mostlyGoodStuff = A.Dummy<MostlyGoodStuffWithoutComparerReestablished>();
                allStuff.Add(mostlyGoodStuff);
            }

            // Assert
            allStuff = allStuff.Distinct().ToList();
            allStuff.Should().HaveCount(5);
            allStuff.Should().Contain(MostlyGoodStuffWithoutComparerReestablished.WorkingFromHome);
            allStuff.Should().Contain(MostlyGoodStuffWithoutComparerReestablished.RainyDays);
            allStuff.Should().Contain(MostlyGoodStuffWithoutComparerReestablished.Chocolate);
            allStuff.Should().Contain(MostlyGoodStuffWithoutComparerReestablished.Vacation);
            allStuff.Should().Contain(MostlyGoodStuffWithoutComparerReestablished.FoodPoisoning);
        }

        [Fact]
        public static void ConstrainDummyToExclude_without_IEqualityComparer___Should_return_only_values_within_set_of_possibleDummies___When_called_for_type_MostlyGoodStuffWithoutComparer()
        {
            // Arrange
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(MostlyGoodStuffWithoutComparer.WorkingFromHome, MostlyGoodStuffWithoutComparer.FoodPoisoning, MostlyGoodStuffWithoutComparer.RainyDays);
            var goodStuff = new List<MostlyGoodStuffWithoutComparer>();

            // Act
            for (int i = 0; i < 5000; i++)
            {
                var mostlyGoodStuff = A.Dummy<MostlyGoodStuffWithoutComparer>();
                goodStuff.Add(mostlyGoodStuff);
            }

            // Assert
            goodStuff = goodStuff.Distinct().ToList();
            goodStuff.Should().HaveCount(4);
            goodStuff.Should().Contain(MostlyGoodStuffWithoutComparer.Chocolate);
            goodStuff.Should().Contain(MostlyGoodStuffWithoutComparer.Vacation);
            goodStuff.Should().Contain(MostlyGoodStuffWithoutComparer.Meditation);
            goodStuff.Should().Contain(MostlyGoodStuffWithoutComparer.Bulldogs);
        }

        [Fact]
        public static void ConstrainDummyToExclude_with_IEqualityComparer___Should_use_most_recently_registered_constraint___When_called_multiple_times_for_same_type()
        {
            // Arrange
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(new[] { MostlyGoodStuffWithComparerReestablished.Chocolate }, EqualityComparer<MostlyGoodStuffWithComparerReestablished>.Default);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(new[] { MostlyGoodStuffWithComparerReestablished.Meditation, MostlyGoodStuffWithComparerReestablished.WorkingFromHome }, EqualityComparer<MostlyGoodStuffWithComparerReestablished>.Default);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(new[] { MostlyGoodStuffWithComparerReestablished.RainyDays, MostlyGoodStuffWithComparerReestablished.FoodPoisoning }, EqualityComparer<MostlyGoodStuffWithComparerReestablished>.Default);
            var allStuff = new List<MostlyGoodStuffWithComparerReestablished>();

            // Act
            for (int i = 0; i < 5000; i++)
            {
                var mostlyGoodStuff = A.Dummy<MostlyGoodStuffWithComparerReestablished>();
                allStuff.Add(mostlyGoodStuff);
            }

            // Assert
            allStuff = allStuff.Distinct().ToList();
            allStuff.Should().HaveCount(5);
            allStuff.Should().Contain(MostlyGoodStuffWithComparerReestablished.WorkingFromHome);
            allStuff.Should().Contain(MostlyGoodStuffWithComparerReestablished.Chocolate);
            allStuff.Should().Contain(MostlyGoodStuffWithComparerReestablished.Vacation);
            allStuff.Should().Contain(MostlyGoodStuffWithComparerReestablished.Meditation);
            allStuff.Should().Contain(MostlyGoodStuffWithComparerReestablished.Bulldogs);
        }

        [Fact]
        public static void ConstrainDummyToExclude_with_IEqualityComparer___Should_return_only_values_within_set_of_possibleDummies___When_called_for_type_MostlyGoodStuffWithComparer()
        {
            // Arrange
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(new[] { MostlyGoodStuffWithComparer.FoodPoisoning, MostlyGoodStuffWithComparer.Chocolate, MostlyGoodStuffWithComparer.WorkingFromHome }, EqualityComparer<MostlyGoodStuffWithComparer>.Default);
            var goodStuff = new List<MostlyGoodStuffWithComparer>();

            // Act
            for (int i = 0; i < 5000; i++)
            {
                var mostlyGoodStuff = A.Dummy<MostlyGoodStuffWithComparer>();
                goodStuff.Add(mostlyGoodStuff);
            }

            // Assert
            goodStuff = goodStuff.Distinct().ToList();
            goodStuff.Should().HaveCount(4);
            goodStuff.Should().Contain(MostlyGoodStuffWithComparer.RainyDays);
            goodStuff.Should().Contain(MostlyGoodStuffWithComparer.Vacation);
            goodStuff.Should().Contain(MostlyGoodStuffWithComparer.Meditation);
            goodStuff.Should().Contain(MostlyGoodStuffWithComparer.Bulldogs);
        }

        [Fact]
        public static void ConstrainDummyToExclude___Should_construct_objects_with_constrained_properties___When_called_for_type_that_contains_constrained_types()
        {
            // Arrange
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(MostlyGoodStuffWithoutComparerIndirect.Vacation, MostlyGoodStuffWithoutComparerIndirect.RainyDays, MostlyGoodStuffWithoutComparerIndirect.Meditation);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(new[] { MostlyGoodStuffWithComparerIndirect.Chocolate, MostlyGoodStuffWithComparerIndirect.Meditation, MostlyGoodStuffWithComparerIndirect.WorkingFromHome }, EqualityComparer<MostlyGoodStuffWithComparerIndirect>.Default);
            var results = new List<ConstrainDummiesToExcludeIndirect>();

            // Act
            for (int i = 0; i < 5000; i++)
            {
                var objectWithConstrainedProperties = A.Dummy<ConstrainDummiesToExcludeIndirect>();
                results.Add(objectWithConstrainedProperties);
            }

            // Assert
            var constrainedProperty1Values = results.Select(_ => _.MostlyGoodStuffWithoutComparerIndirect).Distinct().ToList();
            constrainedProperty1Values.Should().HaveCount(4);
            constrainedProperty1Values.Should().Contain(MostlyGoodStuffWithoutComparerIndirect.WorkingFromHome);
            constrainedProperty1Values.Should().Contain(MostlyGoodStuffWithoutComparerIndirect.Chocolate);
            constrainedProperty1Values.Should().Contain(MostlyGoodStuffWithoutComparerIndirect.FoodPoisoning);
            constrainedProperty1Values.Should().Contain(MostlyGoodStuffWithoutComparerIndirect.Bulldogs);

            var constrainedProperty2Values = results.Select(_ => _.MostlyGoodStuffWithComparerIndirect).Distinct().ToList();
            constrainedProperty2Values.Should().HaveCount(4);
            constrainedProperty2Values.Should().Contain(MostlyGoodStuffWithComparerIndirect.RainyDays);
            constrainedProperty2Values.Should().Contain(MostlyGoodStuffWithComparerIndirect.Vacation);
            constrainedProperty2Values.Should().Contain(MostlyGoodStuffWithComparerIndirect.FoodPoisoning);
            constrainedProperty2Values.Should().Contain(MostlyGoodStuffWithComparerIndirect.Bulldogs);
        }

        [Fact]
        public static void UseRandomInterfaceImplementationForDummy___Should_throw_ArgumentException___When_there_are_no_implementations_of_the_specified_type()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => AutoFixtureBackedDummyFactory.UseRandomInterfaceImplementationForDummy<IAmAnInterfaceWithOnlyInterfaceImplementations>());
            var ex2 = Record.Exception(() => AutoFixtureBackedDummyFactory.UseRandomInterfaceImplementationForDummy<IAmAnInterfaceWithNoImplementations>(includeOtherInterfaces: true));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void UseRandomInterfaceImplementationForDummy___Should_not_throw___When_there_are_interface_implementations_of_specified_type()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => AutoFixtureBackedDummyFactory.UseRandomInterfaceImplementationForDummy<IAmAnInterfaceWithSomeClassImplementations>());
            var ex2 = Record.Exception(() => AutoFixtureBackedDummyFactory.UseRandomInterfaceImplementationForDummy<IAmAnInterfaceWithSomeInterfaceImplementations>(includeOtherInterfaces: true));

            // Assert
            ex1.Should().BeNull();
            ex2.Should().BeNull();
        }

        [Fact]
        public static void UseRandomInterfaceImplementationForDummy___Should_not_throw___When_called_twice_for_same_type()
        {
            // Arrange
            AutoFixtureBackedDummyFactory.UseRandomInterfaceImplementationForDummy<IAmAnInterfaceWithSomeClassImplementations>();
            AutoFixtureBackedDummyFactory.UseRandomInterfaceImplementationForDummy<IAmAnInterfaceWithSomeInterfaceImplementations>(includeOtherInterfaces: true);

            // Act
            var ex1 = Record.Exception(() => AutoFixtureBackedDummyFactory.UseRandomInterfaceImplementationForDummy<IAmAnInterfaceWithSomeClassImplementations>());
            var ex2 = Record.Exception(() => AutoFixtureBackedDummyFactory.UseRandomInterfaceImplementationForDummy<IAmAnInterfaceWithSomeInterfaceImplementations>(includeOtherInterfaces: true));

            // Assert
            ex1.Should().BeNull();
            ex2.Should().BeNull();
        }

        [Fact]
        public static void ADummy___Should_return_random_interface_implementation___When_UseRandomInterfaceImplementationForDummy_is_called_for_type_IAmAnInterfaceWithSomeClassImplementations_and_includeOtherInterfaces_is_false()
        {
            // Arrange
            AutoFixtureBackedDummyFactory.UseRandomInterfaceImplementationForDummy<IAmAnInterfaceWithSomeClassImplementations>();
            var dummies = new List<IAmAnInterfaceWithSomeClassImplementations>();

            // Act
            for (int i = 0; i < 100; i++)
            {
                var dummy = A.Dummy<IAmAnInterfaceWithSomeClassImplementations>();
                dummies.Add(dummy);
            }

            // Assert
            dummies.OfType<ClassInterfaceImplementation1>().Count().Should().BeGreaterThan(1);
            dummies.OfType<ClassInterfaceImplementation2>().Count().Should().BeGreaterThan(1);
            dummies.OfType<ClassInterfaceImplementation3>().Count().Should().BeGreaterThan(1);
        }

        [Fact]
        public static void ADummy___Should_return_random_interface_implementation___When_UseRandomInterfaceImplementationForDummy_is_called_for_type_IAmAnInterfaceWithSomeClassImplementations_and_includeOtherInterfaces_is_true()
        {
            // Arrange
            AutoFixtureBackedDummyFactory.UseRandomInterfaceImplementationForDummy<IAmAnInterfaceWithSomeInterfaceImplementations2>(includeOtherInterfaces: true);
            AutoFixtureBackedDummyFactory.UseRandomInterfaceImplementationForDummy<IAmAnInterfaceWithSomeClassImplementations2>(includeOtherInterfaces: true);
            var dummies = new List<IAmAnInterfaceWithSomeInterfaceImplementations2>();

            // Act
            for (int i = 0; i < 100; i++)
            {
                var dummy = A.Dummy<IAmAnInterfaceWithSomeInterfaceImplementations2>();
                dummies.Add(dummy);
            }

            // Assert
            dummies.OfType<ClassInterfaceImplementation4>().Count().Should().BeGreaterThan(1);
            dummies.OfType<ClassInterfaceImplementation5>().Count().Should().BeGreaterThan(1);
            dummies.OfType<ClassInterfaceImplementation6>().Count().Should().BeGreaterThan(1);
        }

        [Fact]
        public static void ADummy___Should_return_supported_but_unregistered_generic_interfaces_types___When_called_for_those_interface_types()
        {
            // Arrange, Act
            var list1 = A.Dummy<IEnumerable<string>>();
            var list2 = A.Dummy<IList<string>>();
            var list3 = A.Dummy<ICollection<string>>();
            var list4 = A.Dummy<IReadOnlyCollection<string>>();
            var list5 = A.Dummy<IReadOnlyList<string>>();
            var dictionary1 = A.Dummy<IDictionary<int, string>>();
            var dictionary2 = A.Dummy<IReadOnlyDictionary<int, string>>();

            // Assert
            list1.ToList().Should().HaveCount(3);
            list2.ToList().Should().HaveCount(3);
            list3.ToList().Should().HaveCount(3);
            list4.ToList().Should().HaveCount(3);
            list5.ToList().Should().HaveCount(3);
            dictionary1.ToDictionary(_ => _, _ => _).Should().HaveCount(3);
            dictionary2.ToDictionary(_ => _, _ => _).Should().HaveCount(3);
        }

        [Fact]
        public static void ADummy___Should_use_dummy_creator___When_creating_dummies_of_unregistered_but_supported_generic_interfaces_of_types_for_which_dummy_creators_have_been_registered()
        {
            // Arrange
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(MostlyGoodStuffInGenericInterface.Chocolate, MostlyGoodStuffInGenericInterface.Vacation, MostlyGoodStuffInGenericInterface.WorkingFromHome);
            var goodStuff = new List<MostlyGoodStuffInGenericInterface>();

            // Act
            for (int x = 0; x < 1000; x++)
            {
                var listOfMostlyGoodStuff1 = A.Dummy<IEnumerable<MostlyGoodStuffInGenericInterface>>();
                var listOfMostlyGoodStuff2 = A.Dummy<ICollection<MostlyGoodStuffInGenericInterface>>();
                var listOfMostlyGoodStuff3 = A.Dummy<IList<MostlyGoodStuffInGenericInterface>>();
                var listOfMostlyGoodStuff4 = A.Dummy<IReadOnlyCollection<MostlyGoodStuffInGenericInterface>>();
                var listOfMostlyGoodStuff5 = A.Dummy<IReadOnlyList<MostlyGoodStuffInGenericInterface>>();
                var listOfMostlyGoodStuff6 = A.Dummy<IDictionary<string, MostlyGoodStuffInGenericInterface>>();
                var listOfMostlyGoodStuff7 = A.Dummy<IReadOnlyDictionary<string, MostlyGoodStuffInGenericInterface>>();

                goodStuff.AddRange(listOfMostlyGoodStuff1);
                goodStuff.AddRange(listOfMostlyGoodStuff2);
                goodStuff.AddRange(listOfMostlyGoodStuff3);
                goodStuff.AddRange(listOfMostlyGoodStuff4);
                goodStuff.AddRange(listOfMostlyGoodStuff5);
                goodStuff.AddRange(listOfMostlyGoodStuff6.Values);
                goodStuff.AddRange(listOfMostlyGoodStuff7.Values);
            }

            // Assert
            goodStuff.Should().HaveCount(1000 * 7 * 3);
            goodStuff = goodStuff.Distinct().ToList();
            goodStuff.Should().HaveCount(4);
            goodStuff.Should().Contain(MostlyGoodStuffInGenericInterface.Bulldogs);
            goodStuff.Should().Contain(MostlyGoodStuffInGenericInterface.FoodPoisoning);
            goodStuff.Should().Contain(MostlyGoodStuffInGenericInterface.Meditation);
            goodStuff.Should().Contain(MostlyGoodStuffInGenericInterface.RainyDays);
        }

        [Fact]
        public static void ADummy___Should_use_dummy_creator___When_creating_dummies_of_type_that_contains_unregistered_but_supported_generic_interfaces_of_types_for_which_dummy_creators_have_been_registered()
        {
            // Arrange
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(MostlyGoodStuffInGenericInterfaceIndirect.FoodPoisoning, MostlyGoodStuffInGenericInterfaceIndirect.Bulldogs, MostlyGoodStuffInGenericInterfaceIndirect.RainyDays);
            var goodStuff = new List<MostlyGoodStuffInGenericInterfaceIndirect>();

            // Act
            for (int x = 0; x < 1000; x++)
            {
                var listOfMostlyGoodStuff = A.Dummy<SupportedButUnregisteredGenericInterfaceIndirect>();
                goodStuff.AddRange(listOfMostlyGoodStuff.Enumerable);
                goodStuff.AddRange(listOfMostlyGoodStuff.Collection);
                goodStuff.AddRange(listOfMostlyGoodStuff.List);
                goodStuff.AddRange(listOfMostlyGoodStuff.ReadOnlyCollection);
                goodStuff.AddRange(listOfMostlyGoodStuff.ReadOnlyList);
                goodStuff.AddRange(listOfMostlyGoodStuff.Dictionary.Values);
                goodStuff.AddRange(listOfMostlyGoodStuff.ReadOnlyDictionary.Values);
            }

            // Assert
            goodStuff.Should().HaveCount(1000 * 7 * 3);
            goodStuff = goodStuff.Distinct().ToList();
            goodStuff.Should().HaveCount(4);
            goodStuff.Should().Contain(MostlyGoodStuffInGenericInterfaceIndirect.Chocolate);
            goodStuff.Should().Contain(MostlyGoodStuffInGenericInterfaceIndirect.Meditation);
            goodStuff.Should().Contain(MostlyGoodStuffInGenericInterfaceIndirect.WorkingFromHome);
            goodStuff.Should().Contain(MostlyGoodStuffInGenericInterfaceIndirect.Vacation);
        }

        // ReSharper restore InconsistentNaming
    }
}
