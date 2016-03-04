// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SomeTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy.Test
{
    using System;
    using System.Linq;

    using FluentAssertions;

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
        public static void Dummies___Should_return_empty_list___When_parameter_createWith_is_0()
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

        // ReSharper restore InconsistentNaming
    }
}
