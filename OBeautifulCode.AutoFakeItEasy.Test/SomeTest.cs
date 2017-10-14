// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SomeTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy.Test
{
    using System;
    using System.Linq;

    using FakeItEasy;

    using FluentAssertions;

    using OBeautifulCode.Math.Recipes;

    using Xunit;

    /// <summary>
    /// Tests the <see cref="Some"/> class.
    /// </summary>
    public static class SomeTest
    {
        // ReSharper disable InconsistentNaming
        [Fact]
        public static void Dummies___Should_throw_ArgumentException___When_parameter_createWith_is_OneOrMoreNulls_and_parameter_numberOfElements_is_zero()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => Some.Dummies<double>(0, CreateWith.OneOrMoreNulls));
            var ex2 = Record.Exception(() => Some.Dummies<string>(0, CreateWith.OneOrMoreNulls));
            var ex3 = Record.Exception(() => Some.Dummies<object>(0, CreateWith.OneOrMoreNulls));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
            ex3.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void Dummies___Should_throw_ArgumentException___When_parameter_createWith_is_OneOrMoreNulls_and_type_to_create_is_a_value_type()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => Some.Dummies<CreateWith>(3, CreateWith.OneOrMoreNulls));
            var ex2 = Record.Exception(() => Some.Dummies<bool>(3, CreateWith.OneOrMoreNulls));
            var ex3 = Record.Exception(() => Some.Dummies<byte>(3, CreateWith.OneOrMoreNulls));
            var ex4 = Record.Exception(() => Some.Dummies<char>(3, CreateWith.OneOrMoreNulls));
            var ex5 = Record.Exception(() => Some.Dummies<decimal>(3, CreateWith.OneOrMoreNulls));
            var ex6 = Record.Exception(() => Some.Dummies<double>(3, CreateWith.OneOrMoreNulls));
            var ex7 = Record.Exception(() => Some.Dummies<float>(3, CreateWith.OneOrMoreNulls));
            var ex8 = Record.Exception(() => Some.Dummies<int>(3, CreateWith.OneOrMoreNulls));
            var ex9 = Record.Exception(() => Some.Dummies<long>(3, CreateWith.OneOrMoreNulls));
            var ex10 = Record.Exception(() => Some.Dummies<sbyte>(3, CreateWith.OneOrMoreNulls));
            var ex11 = Record.Exception(() => Some.Dummies<short>(3, CreateWith.OneOrMoreNulls));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
            ex3.Should().BeOfType<ArgumentException>();
            ex4.Should().BeOfType<ArgumentException>();
            ex5.Should().BeOfType<ArgumentException>();
            ex6.Should().BeOfType<ArgumentException>();
            ex7.Should().BeOfType<ArgumentException>();
            ex8.Should().BeOfType<ArgumentException>();
            ex9.Should().BeOfType<ArgumentException>();
            ex10.Should().BeOfType<ArgumentException>();
            ex11.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "This is not excessively complex.")]
        public static void Dummies___Should_not_throw___When_parameter_createWith_is_OneOrMoreNulls_and_type_to_create_is_not_a_value_type()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => Some.Dummies<CreateWith?>(3, CreateWith.OneOrMoreNulls));
            var ex2 = Record.Exception(() => Some.Dummies<bool?>(3, CreateWith.OneOrMoreNulls));
            var ex3 = Record.Exception(() => Some.Dummies<byte?>(3, CreateWith.OneOrMoreNulls));
            var ex4 = Record.Exception(() => Some.Dummies<char?>(3, CreateWith.OneOrMoreNulls));
            var ex5 = Record.Exception(() => Some.Dummies<decimal?>(3, CreateWith.OneOrMoreNulls));
            var ex6 = Record.Exception(() => Some.Dummies<double?>(3, CreateWith.OneOrMoreNulls));
            var ex7 = Record.Exception(() => Some.Dummies<float?>(3, CreateWith.OneOrMoreNulls));
            var ex8 = Record.Exception(() => Some.Dummies<int?>(3, CreateWith.OneOrMoreNulls));
            var ex9 = Record.Exception(() => Some.Dummies<long?>(3, CreateWith.OneOrMoreNulls));
            var ex10 = Record.Exception(() => Some.Dummies<sbyte?>(3, CreateWith.OneOrMoreNulls));
            var ex11 = Record.Exception(() => Some.Dummies<short?>(3, CreateWith.OneOrMoreNulls));
            var ex12 = Record.Exception(() => Some.Dummies<string>(3, CreateWith.OneOrMoreNulls));
            var ex13 = Record.Exception(() => Some.Dummies<object>(3, CreateWith.OneOrMoreNulls));

            // Assert
            ex1.Should().BeNull();
            ex2.Should().BeNull();
            ex3.Should().BeNull();
            ex4.Should().BeNull();
            ex5.Should().BeNull();
            ex6.Should().BeNull();
            ex7.Should().BeNull();
            ex8.Should().BeNull();
            ex9.Should().BeNull();
            ex10.Should().BeNull();
            ex11.Should().BeNull();
            ex12.Should().BeNull();
            ex13.Should().BeNull();
        }

        [Fact]
        public static void Dummies___Should_return_object_of_type_SomeDummiesList_with_parameters_numberOfElements_and_createWith_stored_in_the_object_under_properties_of_the_same_name___When_called()
        {
            // Arrange
            // note: can't test numberOfElements = 0 with random createWith, because some combinations are not permitted (like creating a list with one or more nulls)
            var numberOfElements1 = ThreadSafeRandom.Next(-100000, -1);
            var numberOfElements2 = ThreadSafeRandom.Next(1, 100000);
            var createWith = (CreateWith)ThreadSafeRandom.Next(0, Enum.GetNames(typeof(CreateWith)).Length);

            // Act
            var result1 = Some.Dummies<object>(numberOfElements1, createWith);
            var result2 = Some.Dummies<object>(numberOfElements2, createWith);
            var result3 = Some.Dummies<object>(0, CreateWith.ZeroOrMoreNulls);

            // Assert
            var result1AsSomeDummiesList = result1 as SomeDummiesList<object>;
            var result2AsSomeDummiesList = result2 as SomeDummiesList<object>;
            var result3AsSomeDummiesList = result3 as SomeDummiesList<object>;

            result1AsSomeDummiesList.Should().NotBeNull();
            result2AsSomeDummiesList.Should().NotBeNull();
            result3AsSomeDummiesList.Should().NotBeNull();

            // ReSharper disable PossibleNullReferenceException
            result1AsSomeDummiesList.NumberOfElementsSpecifiedInCallToSomeDummies.Should().Be(numberOfElements1);
            result1AsSomeDummiesList.CreateWithSpecifiedInCallToSomeDummies.Should().Be(createWith);

            result2AsSomeDummiesList.NumberOfElementsSpecifiedInCallToSomeDummies.Should().Be(numberOfElements2);
            result2AsSomeDummiesList.CreateWithSpecifiedInCallToSomeDummies.Should().Be(createWith);

            result3AsSomeDummiesList.NumberOfElementsSpecifiedInCallToSomeDummies.Should().Be(0);
            result3AsSomeDummiesList.CreateWithSpecifiedInCallToSomeDummies.Should().Be(CreateWith.ZeroOrMoreNulls);
            // ReSharper restore PossibleNullReferenceException
        }

        [Fact]
        public static void Dummies___Should_return_empty_list___When_parameter_numberOfElements_is_0()
        {
            // Arrange, Act
            // ReSharper disable RedundantArgumentDefaultValue
            var result1 = Some.Dummies<double>(0, CreateWith.NoNulls);
            var result2 = Some.Dummies<double>(0, CreateWith.ZeroOrMoreNulls);
            var result3 = Some.Dummies<int?>(0, CreateWith.NoNulls);
            var result4 = Some.Dummies<int?>(0, CreateWith.ZeroOrMoreNulls);
            var result5 = Some.Dummies<string>(0, CreateWith.NoNulls);
            var result6 = Some.Dummies<string>(0, CreateWith.ZeroOrMoreNulls);
            var result7 = Some.Dummies<object>(0, CreateWith.NoNulls);
            var result8 = Some.Dummies<object>(0, CreateWith.ZeroOrMoreNulls);

            // ReSharper restore RedundantArgumentDefaultValue

            // Assert
            result1.Should().BeEmpty();
            result2.Should().BeEmpty();
            result3.Should().BeEmpty();
            result4.Should().BeEmpty();
            result5.Should().BeEmpty();
            result6.Should().BeEmpty();
            result7.Should().BeEmpty();
            result8.Should().BeEmpty();
        }

        [Fact]
        public static void Dummies___Should_return_list_with_MinRandomNumberOfElements_to_MaxRandomNumberOfElements_elements___When_parameter_numberOfElements_is_not_specified()
        {
            // Arrange, Act
            // ReSharper disable RedundantArgumentDefaultValue
            var result1 = Some.Dummies<double>(createWith: CreateWith.NoNulls);
            var result2 = Some.Dummies<double>(createWith: CreateWith.ZeroOrMoreNulls);

            var result3 = Some.Dummies<int?>(createWith: CreateWith.NoNulls);
            var result4 = Some.Dummies<int?>(createWith: CreateWith.ZeroOrMoreNulls);
            var result5 = Some.Dummies<int?>(createWith: CreateWith.OneOrMoreNulls);

            var result6 = Some.Dummies<string>(createWith: CreateWith.NoNulls);
            var result7 = Some.Dummies<string>(createWith: CreateWith.ZeroOrMoreNulls);
            var result8 = Some.Dummies<string>(createWith: CreateWith.OneOrMoreNulls);

            var result9 = Some.Dummies<object>(createWith: CreateWith.NoNulls);
            var result10 = Some.Dummies<object>(createWith: CreateWith.ZeroOrMoreNulls);
            var result11 = Some.Dummies<object>(createWith: CreateWith.OneOrMoreNulls);

            // ReSharper restore RedundantArgumentDefaultValue

            // Assert
            result1.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result2.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result3.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result4.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result5.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result6.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result7.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result8.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result9.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result10.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result11.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
        }

        [Fact]
        public static void Dummies___Should_return_list_with_MinRandomNumberOfElements_to_MaxRandomNumberOfElements_elements___When_parameter_numberOfElements_is_negative()
        {
            // Arrange, Act
            // ReSharper disable RedundantArgumentDefaultValue
            var result1 = Some.Dummies<double>(A.Dummy<NegativeInteger>(), CreateWith.NoNulls);
            var result2 = Some.Dummies<double>(A.Dummy<NegativeInteger>(), CreateWith.ZeroOrMoreNulls);

            var result3 = Some.Dummies<int?>(A.Dummy<NegativeInteger>(), CreateWith.NoNulls);
            var result4 = Some.Dummies<int?>(A.Dummy<NegativeInteger>(), CreateWith.ZeroOrMoreNulls);
            var result5 = Some.Dummies<int?>(A.Dummy<NegativeInteger>(), CreateWith.OneOrMoreNulls);

            var result6 = Some.Dummies<string>(A.Dummy<NegativeInteger>(), CreateWith.NoNulls);
            var result7 = Some.Dummies<string>(A.Dummy<NegativeInteger>(), CreateWith.ZeroOrMoreNulls);
            var result8 = Some.Dummies<string>(A.Dummy<NegativeInteger>(), CreateWith.OneOrMoreNulls);

            var result9 = Some.Dummies<object>(A.Dummy<NegativeInteger>(), CreateWith.NoNulls);
            var result10 = Some.Dummies<object>(A.Dummy<NegativeInteger>(), CreateWith.ZeroOrMoreNulls);
            var result11 = Some.Dummies<object>(A.Dummy<NegativeInteger>(), CreateWith.OneOrMoreNulls);

            // ReSharper restore RedundantArgumentDefaultValue

            // Assert
            result1.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result2.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result3.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result4.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result5.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result6.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result7.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result8.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result9.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result10.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result11.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
        }

        [Fact]
        public static void Dummies___Should_return_random_list_with_specified_number_of_elements_and_no_nulls___When_parameter_createWith_is_not_specified()
        {
            // Arrange, Act
            // ReSharper disable RedundantArgumentDefaultValue
            const int numberOfElements = 100;
            var result1 = Some.Dummies<double>(numberOfElements);
            var result2 = Some.Dummies<int?>(numberOfElements);
            var result3 = Some.Dummies<string>(numberOfElements);
            var result4 = Some.Dummies<object>(numberOfElements);

            // ReSharper restore RedundantArgumentDefaultValue

            // Assert
            result1.Should().HaveCount(numberOfElements).And.OnlyHaveUniqueItems();
            result2.Should().HaveCount(numberOfElements).And.OnlyHaveUniqueItems().And.NotContainNulls();
            result3.Should().HaveCount(numberOfElements).And.OnlyHaveUniqueItems().And.NotContainNulls();
            result4.Should().HaveCount(numberOfElements).And.OnlyHaveUniqueItems().And.NotContainNulls();
        }

        [Fact]
        public static void Dummies___Should_return_random_list_with_specified_number_of_elements_and_no_nulls___When_parameter_createWith_is_NoNulls()
        {
            // Arrange, Act
            // ReSharper disable RedundantArgumentDefaultValue
            const int numberOfElements = 100;
            var result1 = Some.Dummies<double>(numberOfElements, CreateWith.NoNulls);
            var result2 = Some.Dummies<int?>(numberOfElements, CreateWith.NoNulls);
            var result3 = Some.Dummies<string>(numberOfElements, CreateWith.NoNulls);
            var result4 = Some.Dummies<object>(numberOfElements, CreateWith.NoNulls);

            // ReSharper restore RedundantArgumentDefaultValue

            // Assert
            result1.Should().HaveCount(numberOfElements).And.OnlyHaveUniqueItems();
            result2.Should().HaveCount(numberOfElements).And.OnlyHaveUniqueItems().And.NotContainNulls();
            result3.Should().HaveCount(numberOfElements).And.OnlyHaveUniqueItems().And.NotContainNulls();
            result4.Should().HaveCount(numberOfElements).And.OnlyHaveUniqueItems().And.NotContainNulls();
        }

        [Fact]
        public static void Dummies___Should_return_random_list_with_specified_number_of_elements_and_one_or_more_nulls___When_parameter_createWith_is_OneOrMoreNulls()
        {
            // Arrange, Act
            // ReSharper disable RedundantArgumentDefaultValue
            const int numberOfElements = 3;
            var result1 = Some.Dummies<int?>(numberOfElements, CreateWith.OneOrMoreNulls);
            var result2 = Some.Dummies<string>(numberOfElements, CreateWith.OneOrMoreNulls);
            var result3 = Some.Dummies<object>(numberOfElements, CreateWith.OneOrMoreNulls);

            var result4 = Some.Dummies<int?>(1, CreateWith.OneOrMoreNulls);
            var result5 = Some.Dummies<string>(1, CreateWith.OneOrMoreNulls);
            var result6 = Some.Dummies<object>(1, CreateWith.OneOrMoreNulls);

            // ReSharper restore RedundantArgumentDefaultValue

            // Assert
            result1.Should().HaveCount(numberOfElements).And.Contain((int?)null);
            result1.Where(_ => _ != null).Should().OnlyHaveUniqueItems();

            result2.Should().HaveCount(numberOfElements).And.Contain((string)null);
            result2.Where(_ => _ != null).Should().OnlyHaveUniqueItems();

            result3.Should().HaveCount(numberOfElements).And.Contain((object)null);
            result3.Where(_ => _ != null).Should().OnlyHaveUniqueItems();

            result4.Should().ContainSingle().And.Contain((int?)null);
            result5.Should().ContainSingle().And.Contain((string)null);
            result6.Should().ContainSingle().And.Contain((object)null);
        }

        [Fact]
        public static void Dummies___Should_return_random_list_with_specified_number_of_elements_and_zero_or_more_nulls___When_parameter_createWith_is_ZeroOrMoreNulls()
        {
            // Arrange, Act
            // ReSharper disable RedundantArgumentDefaultValue
            const int numberOfElements = 100;
            var result1 = Some.Dummies<int?>(numberOfElements, CreateWith.ZeroOrMoreNulls);
            var result2 = Some.Dummies<string>(numberOfElements, CreateWith.ZeroOrMoreNulls);
            var result3 = Some.Dummies<object>(numberOfElements, CreateWith.ZeroOrMoreNulls);
            var result4 = Some.Dummies<double>(numberOfElements, CreateWith.ZeroOrMoreNulls);

            // ReSharper restore RedundantArgumentDefaultValue

            // Assert
            result1.Should().HaveCount(numberOfElements);
            result1.Where(_ => _ != null).Should().OnlyHaveUniqueItems();

            result2.Should().HaveCount(numberOfElements);
            result2.Where(_ => _ != null).Should().OnlyHaveUniqueItems();

            result3.Should().HaveCount(numberOfElements);
            result3.Where(_ => _ != null).Should().OnlyHaveUniqueItems();

            result4.Should().HaveCount(numberOfElements).And.OnlyHaveUniqueItems();
        }

        [Fact]
        public static void ReadOnlyDummies___Should_throw_ArgumentException___When_parameter_createWith_is_OneOrMoreNulls_and_parameter_numberOfElements_is_zero()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => Some.ReadOnlyDummies<double>(0, CreateWith.OneOrMoreNulls));
            var ex2 = Record.Exception(() => Some.ReadOnlyDummies<string>(0, CreateWith.OneOrMoreNulls));
            var ex3 = Record.Exception(() => Some.ReadOnlyDummies<object>(0, CreateWith.OneOrMoreNulls));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
            ex3.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void ReadOnlyDummies___Should_throw_ArgumentException___When_parameter_createWith_is_OneOrMoreNulls_and_type_to_create_is_a_value_type()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => Some.ReadOnlyDummies<CreateWith>(3, CreateWith.OneOrMoreNulls));
            var ex2 = Record.Exception(() => Some.ReadOnlyDummies<bool>(3, CreateWith.OneOrMoreNulls));
            var ex3 = Record.Exception(() => Some.ReadOnlyDummies<byte>(3, CreateWith.OneOrMoreNulls));
            var ex4 = Record.Exception(() => Some.ReadOnlyDummies<char>(3, CreateWith.OneOrMoreNulls));
            var ex5 = Record.Exception(() => Some.ReadOnlyDummies<decimal>(3, CreateWith.OneOrMoreNulls));
            var ex6 = Record.Exception(() => Some.ReadOnlyDummies<double>(3, CreateWith.OneOrMoreNulls));
            var ex7 = Record.Exception(() => Some.ReadOnlyDummies<float>(3, CreateWith.OneOrMoreNulls));
            var ex8 = Record.Exception(() => Some.ReadOnlyDummies<int>(3, CreateWith.OneOrMoreNulls));
            var ex9 = Record.Exception(() => Some.ReadOnlyDummies<long>(3, CreateWith.OneOrMoreNulls));
            var ex10 = Record.Exception(() => Some.ReadOnlyDummies<sbyte>(3, CreateWith.OneOrMoreNulls));
            var ex11 = Record.Exception(() => Some.ReadOnlyDummies<short>(3, CreateWith.OneOrMoreNulls));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
            ex3.Should().BeOfType<ArgumentException>();
            ex4.Should().BeOfType<ArgumentException>();
            ex5.Should().BeOfType<ArgumentException>();
            ex6.Should().BeOfType<ArgumentException>();
            ex7.Should().BeOfType<ArgumentException>();
            ex8.Should().BeOfType<ArgumentException>();
            ex9.Should().BeOfType<ArgumentException>();
            ex10.Should().BeOfType<ArgumentException>();
            ex11.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "This is not excessively complex.")]
        public static void ReadOnlyDummies___Should_not_throw___When_parameter_createWith_is_OneOrMoreNulls_and_type_to_create_is_not_a_value_type()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => Some.ReadOnlyDummies<CreateWith?>(3, CreateWith.OneOrMoreNulls));
            var ex2 = Record.Exception(() => Some.ReadOnlyDummies<bool?>(3, CreateWith.OneOrMoreNulls));
            var ex3 = Record.Exception(() => Some.ReadOnlyDummies<byte?>(3, CreateWith.OneOrMoreNulls));
            var ex4 = Record.Exception(() => Some.ReadOnlyDummies<char?>(3, CreateWith.OneOrMoreNulls));
            var ex5 = Record.Exception(() => Some.ReadOnlyDummies<decimal?>(3, CreateWith.OneOrMoreNulls));
            var ex6 = Record.Exception(() => Some.ReadOnlyDummies<double?>(3, CreateWith.OneOrMoreNulls));
            var ex7 = Record.Exception(() => Some.ReadOnlyDummies<float?>(3, CreateWith.OneOrMoreNulls));
            var ex8 = Record.Exception(() => Some.ReadOnlyDummies<int?>(3, CreateWith.OneOrMoreNulls));
            var ex9 = Record.Exception(() => Some.ReadOnlyDummies<long?>(3, CreateWith.OneOrMoreNulls));
            var ex10 = Record.Exception(() => Some.ReadOnlyDummies<sbyte?>(3, CreateWith.OneOrMoreNulls));
            var ex11 = Record.Exception(() => Some.ReadOnlyDummies<short?>(3, CreateWith.OneOrMoreNulls));
            var ex12 = Record.Exception(() => Some.ReadOnlyDummies<string>(3, CreateWith.OneOrMoreNulls));
            var ex13 = Record.Exception(() => Some.ReadOnlyDummies<object>(3, CreateWith.OneOrMoreNulls));

            // Assert
            ex1.Should().BeNull();
            ex2.Should().BeNull();
            ex3.Should().BeNull();
            ex4.Should().BeNull();
            ex5.Should().BeNull();
            ex6.Should().BeNull();
            ex7.Should().BeNull();
            ex8.Should().BeNull();
            ex9.Should().BeNull();
            ex10.Should().BeNull();
            ex11.Should().BeNull();
            ex12.Should().BeNull();
            ex13.Should().BeNull();
        }

        [Fact]
        public static void ReadOnlyDummies___Should_return_object_of_type_SomeReadOnlyDummiesList_with_parameters_numberOfElements_and_createWith_stored_in_the_object_under_properties_of_the_same_name___When_called()
        {
            // Arrange
            // note: can't test numberOfElements = 0 with random createWith, because some combinations are not permitted (like creating a list with one or more nulls)
            var numberOfElements1 = ThreadSafeRandom.Next(-100000, -1);
            var numberOfElements2 = ThreadSafeRandom.Next(1, 100000);
            var createWith = (CreateWith)ThreadSafeRandom.Next(0, Enum.GetNames(typeof(CreateWith)).Length);

            // Act
            var result1 = Some.ReadOnlyDummies<object>(numberOfElements1, createWith);
            var result2 = Some.ReadOnlyDummies<object>(numberOfElements2, createWith);
            var result3 = Some.ReadOnlyDummies<object>(0, CreateWith.ZeroOrMoreNulls);

            // Assert
            var result1AsSomeReadOnlyDummiesList = result1 as SomeReadOnlyDummiesList<object>;
            var result2AsSomeReadOnlyDummiesList = result2 as SomeReadOnlyDummiesList<object>;
            var result3AsSomeReadOnlyDummiesList = result3 as SomeReadOnlyDummiesList<object>;

            result1AsSomeReadOnlyDummiesList.Should().NotBeNull();
            result2AsSomeReadOnlyDummiesList.Should().NotBeNull();
            result3AsSomeReadOnlyDummiesList.Should().NotBeNull();

            // ReSharper disable PossibleNullReferenceException
            result1AsSomeReadOnlyDummiesList.NumberOfElementsSpecifiedInCallToSomeDummies.Should().Be(numberOfElements1);
            result1AsSomeReadOnlyDummiesList.CreateWithSpecifiedInCallToSomeDummies.Should().Be(createWith);

            result2AsSomeReadOnlyDummiesList.NumberOfElementsSpecifiedInCallToSomeDummies.Should().Be(numberOfElements2);
            result2AsSomeReadOnlyDummiesList.CreateWithSpecifiedInCallToSomeDummies.Should().Be(createWith);

            result3AsSomeReadOnlyDummiesList.NumberOfElementsSpecifiedInCallToSomeDummies.Should().Be(0);
            result3AsSomeReadOnlyDummiesList.CreateWithSpecifiedInCallToSomeDummies.Should().Be(CreateWith.ZeroOrMoreNulls);
            // ReSharper restore PossibleNullReferenceException
        }

        [Fact]
        public static void ReadOnlyDummies___Should_return_empty_list___When_parameter_numberOfElements_is_0()
        {
            // Arrange, Act
            // ReSharper disable RedundantArgumentDefaultValue
            var result1 = Some.ReadOnlyDummies<double>(0, CreateWith.NoNulls);
            var result2 = Some.ReadOnlyDummies<double>(0, CreateWith.ZeroOrMoreNulls);
            var result3 = Some.ReadOnlyDummies<int?>(0, CreateWith.NoNulls);
            var result4 = Some.ReadOnlyDummies<int?>(0, CreateWith.ZeroOrMoreNulls);
            var result5 = Some.ReadOnlyDummies<string>(0, CreateWith.NoNulls);
            var result6 = Some.ReadOnlyDummies<string>(0, CreateWith.ZeroOrMoreNulls);
            var result7 = Some.ReadOnlyDummies<object>(0, CreateWith.NoNulls);
            var result8 = Some.ReadOnlyDummies<object>(0, CreateWith.ZeroOrMoreNulls);

            // ReSharper restore RedundantArgumentDefaultValue

            // Assert
            result1.Should().BeEmpty();
            result2.Should().BeEmpty();
            result3.Should().BeEmpty();
            result4.Should().BeEmpty();
            result5.Should().BeEmpty();
            result6.Should().BeEmpty();
            result7.Should().BeEmpty();
            result8.Should().BeEmpty();
        }

        [Fact]
        public static void ReadOnlyDummies___Should_return_list_with_MinRandomNumberOfElements_to_MaxRandomNumberOfElements_elements___When_parameter_numberOfElements_is_not_specified()
        {
            // Arrange, Act
            // ReSharper disable RedundantArgumentDefaultValue
            var result1 = Some.ReadOnlyDummies<double>(createWith: CreateWith.NoNulls);
            var result2 = Some.ReadOnlyDummies<double>(createWith: CreateWith.ZeroOrMoreNulls);

            var result3 = Some.ReadOnlyDummies<int?>(createWith: CreateWith.NoNulls);
            var result4 = Some.ReadOnlyDummies<int?>(createWith: CreateWith.ZeroOrMoreNulls);
            var result5 = Some.ReadOnlyDummies<int?>(createWith: CreateWith.OneOrMoreNulls);

            var result6 = Some.ReadOnlyDummies<string>(createWith: CreateWith.NoNulls);
            var result7 = Some.ReadOnlyDummies<string>(createWith: CreateWith.ZeroOrMoreNulls);
            var result8 = Some.ReadOnlyDummies<string>(createWith: CreateWith.OneOrMoreNulls);

            var result9 = Some.ReadOnlyDummies<object>(createWith: CreateWith.NoNulls);
            var result10 = Some.ReadOnlyDummies<object>(createWith: CreateWith.ZeroOrMoreNulls);
            var result11 = Some.ReadOnlyDummies<object>(createWith: CreateWith.OneOrMoreNulls);

            // ReSharper restore RedundantArgumentDefaultValue

            // Assert
            result1.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result2.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result3.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result4.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result5.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result6.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result7.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result8.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result9.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result10.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result11.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
        }

        [Fact]
        public static void ReadOnlyDummies___Should_return_list_with_MinRandomNumberOfElements_to_MaxRandomNumberOfElements_elements___When_parameter_numberOfElements_is_negative()
        {
            // Arrange, Act
            // ReSharper disable RedundantArgumentDefaultValue
            var result1 = Some.ReadOnlyDummies<double>(A.Dummy<NegativeInteger>(), CreateWith.NoNulls);
            var result2 = Some.ReadOnlyDummies<double>(A.Dummy<NegativeInteger>(), CreateWith.ZeroOrMoreNulls);

            var result3 = Some.ReadOnlyDummies<int?>(A.Dummy<NegativeInteger>(), CreateWith.NoNulls);
            var result4 = Some.ReadOnlyDummies<int?>(A.Dummy<NegativeInteger>(), CreateWith.ZeroOrMoreNulls);
            var result5 = Some.ReadOnlyDummies<int?>(A.Dummy<NegativeInteger>(), CreateWith.OneOrMoreNulls);

            var result6 = Some.ReadOnlyDummies<string>(A.Dummy<NegativeInteger>(), CreateWith.NoNulls);
            var result7 = Some.ReadOnlyDummies<string>(A.Dummy<NegativeInteger>(), CreateWith.ZeroOrMoreNulls);
            var result8 = Some.ReadOnlyDummies<string>(A.Dummy<NegativeInteger>(), CreateWith.OneOrMoreNulls);

            var result9 = Some.ReadOnlyDummies<object>(A.Dummy<NegativeInteger>(), CreateWith.NoNulls);
            var result10 = Some.ReadOnlyDummies<object>(A.Dummy<NegativeInteger>(), CreateWith.ZeroOrMoreNulls);
            var result11 = Some.ReadOnlyDummies<object>(A.Dummy<NegativeInteger>(), CreateWith.OneOrMoreNulls);

            // ReSharper restore RedundantArgumentDefaultValue

            // Assert
            result1.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result2.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result3.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result4.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result5.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result6.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result7.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result8.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result9.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result10.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
            result11.Count.Should().BeGreaterOrEqualTo(Some.MinRandomNumberOfElements).And.BeLessOrEqualTo(Some.MaxRandomNumberOfElements);
        }

        [Fact]
        public static void ReadOnlyDummies___Should_return_random_list_with_specified_number_of_elements_and_no_nulls___When_parameter_createWith_is_not_specified()
        {
            // Arrange, Act
            // ReSharper disable RedundantArgumentDefaultValue
            const int numberOfElements = 100;
            var result1 = Some.ReadOnlyDummies<double>(numberOfElements);
            var result2 = Some.ReadOnlyDummies<int?>(numberOfElements);
            var result3 = Some.ReadOnlyDummies<string>(numberOfElements);
            var result4 = Some.ReadOnlyDummies<object>(numberOfElements);

            // ReSharper restore RedundantArgumentDefaultValue

            // Assert
            result1.Should().HaveCount(numberOfElements).And.OnlyHaveUniqueItems();
            result2.Should().HaveCount(numberOfElements).And.OnlyHaveUniqueItems().And.NotContainNulls();
            result3.Should().HaveCount(numberOfElements).And.OnlyHaveUniqueItems().And.NotContainNulls();
            result4.Should().HaveCount(numberOfElements).And.OnlyHaveUniqueItems().And.NotContainNulls();
        }

        [Fact]
        public static void ReadOnlyDummies___Should_return_random_list_with_specified_number_of_elements_and_no_nulls___When_parameter_createWith_is_NoNulls()
        {
            // Arrange, Act
            // ReSharper disable RedundantArgumentDefaultValue
            const int numberOfElements = 100;
            var result1 = Some.ReadOnlyDummies<double>(numberOfElements, CreateWith.NoNulls);
            var result2 = Some.ReadOnlyDummies<int?>(numberOfElements, CreateWith.NoNulls);
            var result3 = Some.ReadOnlyDummies<string>(numberOfElements, CreateWith.NoNulls);
            var result4 = Some.ReadOnlyDummies<object>(numberOfElements, CreateWith.NoNulls);

            // ReSharper restore RedundantArgumentDefaultValue

            // Assert
            result1.Should().HaveCount(numberOfElements).And.OnlyHaveUniqueItems();
            result2.Should().HaveCount(numberOfElements).And.OnlyHaveUniqueItems().And.NotContainNulls();
            result3.Should().HaveCount(numberOfElements).And.OnlyHaveUniqueItems().And.NotContainNulls();
            result4.Should().HaveCount(numberOfElements).And.OnlyHaveUniqueItems().And.NotContainNulls();
        }

        [Fact]
        public static void ReadOnlyDummies___Should_return_random_list_with_specified_number_of_elements_and_one_or_more_nulls___When_parameter_createWith_is_OneOrMoreNulls()
        {
            // Arrange, Act
            // ReSharper disable RedundantArgumentDefaultValue
            const int numberOfElements = 3;
            var result1 = Some.ReadOnlyDummies<int?>(numberOfElements, CreateWith.OneOrMoreNulls);
            var result2 = Some.ReadOnlyDummies<string>(numberOfElements, CreateWith.OneOrMoreNulls);
            var result3 = Some.ReadOnlyDummies<object>(numberOfElements, CreateWith.OneOrMoreNulls);

            var result4 = Some.ReadOnlyDummies<int?>(1, CreateWith.OneOrMoreNulls);
            var result5 = Some.ReadOnlyDummies<string>(1, CreateWith.OneOrMoreNulls);
            var result6 = Some.ReadOnlyDummies<object>(1, CreateWith.OneOrMoreNulls);

            // ReSharper restore RedundantArgumentDefaultValue

            // Assert
            result1.Should().HaveCount(numberOfElements).And.Contain((int?)null);
            result1.Where(_ => _ != null).Should().OnlyHaveUniqueItems();

            result2.Should().HaveCount(numberOfElements).And.Contain((string)null);
            result2.Where(_ => _ != null).Should().OnlyHaveUniqueItems();

            result3.Should().HaveCount(numberOfElements).And.Contain((object)null);
            result3.Where(_ => _ != null).Should().OnlyHaveUniqueItems();

            result4.Should().ContainSingle().And.Contain((int?)null);
            result5.Should().ContainSingle().And.Contain((string)null);
            result6.Should().ContainSingle().And.Contain((object)null);
        }

        [Fact]
        public static void ReadOnlyDummies___Should_return_random_list_with_specified_number_of_elements_and_zero_or_more_nulls___When_parameter_createWith_is_ZeroOrMoreNulls()
        {
            // Arrange, Act
            // ReSharper disable RedundantArgumentDefaultValue
            const int numberOfElements = 100;
            var result1 = Some.ReadOnlyDummies<int?>(numberOfElements, CreateWith.ZeroOrMoreNulls);
            var result2 = Some.ReadOnlyDummies<string>(numberOfElements, CreateWith.ZeroOrMoreNulls);
            var result3 = Some.ReadOnlyDummies<object>(numberOfElements, CreateWith.ZeroOrMoreNulls);
            var result4 = Some.ReadOnlyDummies<double>(numberOfElements, CreateWith.ZeroOrMoreNulls);

            // ReSharper restore RedundantArgumentDefaultValue

            // Assert
            result1.Should().HaveCount(numberOfElements);
            result1.Where(_ => _ != null).Should().OnlyHaveUniqueItems();

            result2.Should().HaveCount(numberOfElements);
            result2.Where(_ => _ != null).Should().OnlyHaveUniqueItems();

            result3.Should().HaveCount(numberOfElements);
            result3.Where(_ => _ != null).Should().OnlyHaveUniqueItems();

            result4.Should().HaveCount(numberOfElements).And.OnlyHaveUniqueItems();
        }

        // ReSharper restore InconsistentNaming
    }
}
