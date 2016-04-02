// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtensionMethodsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy.Test
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using FakeItEasy;

    using FluentAssertions;

    using Xunit;

    /// <summary>
    /// Tests the <see cref="ExtensionMethods"/> class.
    /// </summary>
    public static class ExtensionMethodsTest
    {
        // ReSharper disable InconsistentNaming
        [Fact]
        public static void ThatIs___Should_throw_ArgumentNullException___When_parameter_condition_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => A.Dummy<int>().ThatIs(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void ThatIs___Should_throw_InvalidOperation___When_maxAttempts_is_1_and_referenceDummy_does_not_meet_the_condition()
        {
            // Arrange, Act
            var ex = Record.Exception(() => A.Dummy<string>().ThatIs(ConditionThatCannnotBeMet, maxAttempts: 1));

            // Assert
            ex.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void ThatIs___Should_throw_InvalidOperation___When_condition_cannot_be_met_and_maxAttempts_is_greater_than_one()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => A.Dummy<string>().ThatIs(ConditionThatCannnotBeMet));
            var ex2 = Record.Exception(() => A.Dummy<string>().ThatIs(ConditionThatCannnotBeMet, maxAttempts: 101));

            // Assert
            ex1.Should().BeOfType<InvalidOperationException>();
            ex2.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void ThatIs___Should_try_maxAttempts_number_of_times_to_meet_condition_before_throwing___When_condition_cannot_be_met_and_maxAttempts_is_positive()
        {
            // Arrange
            var referenceDummy = A.Dummy<string>();
            var dummies1 = new HashSet<string>();
            Func<string, bool> condition1 = _ =>
            {
                dummies1.Add(_);
                return false;
            };

            var dummies2 = new HashSet<string>();
            Func<string, bool> condition2 = _ =>
            {
                dummies2.Add(_);
                return false;
            };

            // Act
            var ex1 = Record.Exception(() => referenceDummy.ThatIs(condition1, maxAttempts: 1));
            var ex2 = Record.Exception(() => referenceDummy.ThatIs(condition2, maxAttempts: 101));

            // Assert
            ex1.Should().BeOfType<InvalidOperationException>();
            ex2.Should().BeOfType<InvalidOperationException>();

            dummies1.Should().HaveCount(1);
            dummies2.Should().HaveCount(101);

            dummies1.Should().Contain(referenceDummy);
            dummies2.Should().Contain(referenceDummy);
        }

        [Fact]
        public static void ThatIs___Should_try_an_infinite_number_of_times_to_meet_condition___When_condition_cannot_be_met_and_maxAttempts_is_zero_or_negative()
        {
            // Arrange
            const int NegativeMaxAttempts = -1000;
            const int InfiniteAttemptsMax = 1000;
            var attempts1 = 0;
            var attempts2 = 0;
            Func<string, bool> condition1 = _ =>
                {
                    attempts1++;
                    if (attempts1 >= InfiniteAttemptsMax)
                    {
                        throw new OverflowException();
                    }

                    return false;
                };

            Func<string, bool> condition2 = _ =>
            {
                attempts2++;
                if (attempts2 >= InfiniteAttemptsMax)
                {
                    throw new OverflowException();
                }

                return false;
            };

            // Act
            var ex1 = Record.Exception(() => A.Dummy<string>().ThatIs(condition1, maxAttempts: 0));
            var ex2 = Record.Exception(() => A.Dummy<string>().ThatIs(condition2, NegativeMaxAttempts));

            // Assert
            ex1.Should().BeOfType<OverflowException>();
            ex2.Should().BeOfType<OverflowException>();
            attempts1.Should().Be(InfiniteAttemptsMax);
            attempts2.Should().Be(InfiniteAttemptsMax);
        }

        [Fact]
        public static void ThatIs___Should_return_referenceDummy___When_referenceDummy_meets_condition_regardless_of_maxAttempts()
        {
            // Arrange
            var referenceDummy = A.Dummy<string>();

            // Act
            // ReSharper disable RedundantArgumentName
            var result1 = referenceDummy.ThatIs(ConditionThatsAlwaysMet);
            var result2 = referenceDummy.ThatIs(ConditionThatsAlwaysMet, maxAttempts: 101);
            var result3 = referenceDummy.ThatIs(ConditionThatsAlwaysMet, maxAttempts: 0);
            var result4 = referenceDummy.ThatIs(ConditionThatsAlwaysMet, maxAttempts: -1);
            var result5 = referenceDummy.ThatIs(ConditionThatsAlwaysMet, maxAttempts: -1000);
            // ReSharper restore RedundantArgumentName

            // Assert
            result1.Should().Be(referenceDummy);
            result2.Should().Be(referenceDummy);
            result3.Should().Be(referenceDummy);
            result4.Should().Be(referenceDummy);
            result5.Should().Be(referenceDummy);
        }

        [Fact]
        public static void ThatIs___Should_return_new_dummy_that_meets_condition___When_referenceDummy_is_null_regardless_of_maxAttempts()
        {
            // Arrange
            const string ExpectedCharacter = "a";
            string referenceDummy = null;
            Func<string, bool> condition = _ => (_ != null) && _.Contains(ExpectedCharacter);

            // Act
            // ReSharper disable RedundantArgumentName
            // ReSharper disable ExpressionIsAlwaysNull
            var result1 = referenceDummy.ThatIs(condition);
            var result2 = referenceDummy.ThatIs(condition, maxAttempts: 101);
            var result3 = referenceDummy.ThatIs(condition, maxAttempts: 0);
            var result4 = referenceDummy.ThatIs(condition, maxAttempts: -1);
            var result5 = referenceDummy.ThatIs(condition, maxAttempts: -1000);
            // ReSharper restore ExpressionIsAlwaysNull
            // ReSharper restore RedundantArgumentName

            // Assert
            result1.Should().Contain(ExpectedCharacter);
            result2.Should().Contain(ExpectedCharacter);
            result3.Should().Contain(ExpectedCharacter);
            result4.Should().Contain(ExpectedCharacter);
            result5.Should().Contain(ExpectedCharacter);
        }

        [Fact]
        public static void ThatIs___Should_return_dummy_that_meets_condition___When_referenceDummy_does_not_meet_condition_but_condition_can_be_satisfied_after_one_or_more_attempts()
        {
            // Arrange
            const string ExpectedCharacter = "a";
            string referenceDummy = A.Dummy<string>().Replace(ExpectedCharacter, string.Empty);
            Func<string, bool> condition = _ => _.Contains(ExpectedCharacter);

            // Act
            // ReSharper disable RedundantArgumentName
            var result1 = referenceDummy.ThatIs(condition);
            var result2 = referenceDummy.ThatIs(condition, maxAttempts: 101);
            var result3 = referenceDummy.ThatIs(condition, maxAttempts: 0);
            var result4 = referenceDummy.ThatIs(condition, maxAttempts: -1);
            var result5 = referenceDummy.ThatIs(condition, maxAttempts: -1000);
            // ReSharper restore RedundantArgumentName

            // Assert
            result1.Should().NotBe(referenceDummy);
            result2.Should().NotBe(referenceDummy);
            result3.Should().NotBe(referenceDummy);
            result4.Should().NotBe(referenceDummy);
            result5.Should().NotBe(referenceDummy);

            result1.Should().Contain(ExpectedCharacter);
            result2.Should().Contain(ExpectedCharacter);
            result3.Should().Contain(ExpectedCharacter);
            result4.Should().Contain(ExpectedCharacter);
            result5.Should().Contain(ExpectedCharacter);
        }

        [Fact]
        public static void Whose___Should_throw_ArgumentNullException___When_parameter_condition_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => A.Dummy<int>().Whose(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Whose___Should_throw_InvalidOperation___When_maxAttempts_is_1_and_referenceDummy_does_not_meet_the_condition()
        {
            // Arrange, Act
            var ex = Record.Exception(() => A.Dummy<string>().Whose(ConditionThatCannnotBeMet, maxAttempts: 1));

            // Assert
            ex.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void Whose___Should_throw_InvalidOperation___When_condition_cannot_be_met_and_maxAttempts_is_greater_than_one()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => A.Dummy<string>().Whose(ConditionThatCannnotBeMet));
            var ex2 = Record.Exception(() => A.Dummy<string>().Whose(ConditionThatCannnotBeMet, maxAttempts: 101));

            // Assert
            ex1.Should().BeOfType<InvalidOperationException>();
            ex2.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void Whose___Should_try_maxAttempts_number_of_times_to_meet_condition_before_throwing___When_condition_cannot_be_met_and_maxAttempts_is_positive()
        {
            // Arrange
            var referenceDummy = A.Dummy<string>();
            var dummies1 = new HashSet<string>();
            Func<string, bool> condition1 = _ =>
            {
                dummies1.Add(_);
                return false;
            };

            var dummies2 = new HashSet<string>();
            Func<string, bool> condition2 = _ =>
            {
                dummies2.Add(_);
                return false;
            };

            // Act
            var ex1 = Record.Exception(() => referenceDummy.Whose(condition1, maxAttempts: 1));
            var ex2 = Record.Exception(() => referenceDummy.Whose(condition2, maxAttempts: 101));

            // Assert
            ex1.Should().BeOfType<InvalidOperationException>();
            ex2.Should().BeOfType<InvalidOperationException>();

            dummies1.Should().HaveCount(1);
            dummies2.Should().HaveCount(101);

            dummies1.Should().Contain(referenceDummy);
            dummies2.Should().Contain(referenceDummy);
        }

        [Fact]
        public static void Whose___Should_try_an_infinite_number_of_times_to_meet_condition___When_condition_cannot_be_met_and_maxAttempts_is_zero_or_negative()
        {
            // Arrange
            const int NegativeMaxAttempts = -1000;
            const int InfiniteAttemptsMax = 1000;
            var attempts1 = 0;
            var attempts2 = 0;
            Func<string, bool> condition1 = _ =>
            {
                attempts1++;
                if (attempts1 >= InfiniteAttemptsMax)
                {
                    throw new OverflowException();
                }

                return false;
            };

            Func<string, bool> condition2 = _ =>
            {
                attempts2++;
                if (attempts2 >= InfiniteAttemptsMax)
                {
                    throw new OverflowException();
                }

                return false;
            };

            // Act
            var ex1 = Record.Exception(() => A.Dummy<string>().Whose(condition1, maxAttempts: 0));
            var ex2 = Record.Exception(() => A.Dummy<string>().Whose(condition2, NegativeMaxAttempts));

            // Assert
            ex1.Should().BeOfType<OverflowException>();
            ex2.Should().BeOfType<OverflowException>();
            attempts1.Should().Be(InfiniteAttemptsMax);
            attempts2.Should().Be(InfiniteAttemptsMax);
        }

        [Fact]
        public static void Whose___Should_return_referenceDummy___When_referenceDummy_meets_condition_regardless_of_maxAttempts()
        {
            // Arrange
            var referenceDummy = A.Dummy<string>();

            // Act
            // ReSharper disable RedundantArgumentName
            var result1 = referenceDummy.Whose(ConditionThatsAlwaysMet);
            var result2 = referenceDummy.Whose(ConditionThatsAlwaysMet, maxAttempts: 101);
            var result3 = referenceDummy.Whose(ConditionThatsAlwaysMet, maxAttempts: 0);
            var result4 = referenceDummy.Whose(ConditionThatsAlwaysMet, maxAttempts: -1);
            var result5 = referenceDummy.Whose(ConditionThatsAlwaysMet, maxAttempts: -1000);
            // ReSharper restore RedundantArgumentName

            // Assert
            result1.Should().Be(referenceDummy);
            result2.Should().Be(referenceDummy);
            result3.Should().Be(referenceDummy);
            result4.Should().Be(referenceDummy);
            result5.Should().Be(referenceDummy);
        }

        [Fact]
        public static void Whose___Should_return_new_dummy_that_meets_condition___When_referenceDummy_is_null_regardless_of_maxAttempts()
        {
            // Arrange
            const string ExpectedCharacter = "a";
            string referenceDummy = null;
            Func<string, bool> condition = _ => (_ != null) && _.Contains(ExpectedCharacter);

            // Act
            // ReSharper disable RedundantArgumentName
            // ReSharper disable ExpressionIsAlwaysNull
            var result1 = referenceDummy.Whose(condition);
            var result2 = referenceDummy.Whose(condition, maxAttempts: 101);
            var result3 = referenceDummy.Whose(condition, maxAttempts: 0);
            var result4 = referenceDummy.Whose(condition, maxAttempts: -1);
            var result5 = referenceDummy.Whose(condition, maxAttempts: -1000);
            // ReSharper restore ExpressionIsAlwaysNull
            // ReSharper restore RedundantArgumentName

            // Assert
            result1.Should().Contain(ExpectedCharacter);
            result2.Should().Contain(ExpectedCharacter);
            result3.Should().Contain(ExpectedCharacter);
            result4.Should().Contain(ExpectedCharacter);
            result5.Should().Contain(ExpectedCharacter);
        }

        [Fact]
        public static void Whose___Should_return_dummy_that_meets_condition___When_referenceDummy_does_not_meet_condition_but_condition_can_be_satisfied_after_one_or_more_attempts()
        {
            // Arrange
            const string ExpectedCharacter = "a";
            string referenceDummy = A.Dummy<string>().Replace(ExpectedCharacter, string.Empty);
            Func<string, bool> condition = _ => _.Contains(ExpectedCharacter);

            // Act
            // ReSharper disable RedundantArgumentName
            var result1 = referenceDummy.Whose(condition);
            var result2 = referenceDummy.Whose(condition, maxAttempts: 101);
            var result3 = referenceDummy.Whose(condition, maxAttempts: 0);
            var result4 = referenceDummy.Whose(condition, maxAttempts: -1);
            var result5 = referenceDummy.Whose(condition, maxAttempts: -1000);
            // ReSharper restore RedundantArgumentName

            // Assert
            result1.Should().NotBe(referenceDummy);
            result2.Should().NotBe(referenceDummy);
            result3.Should().NotBe(referenceDummy);
            result4.Should().NotBe(referenceDummy);
            result5.Should().NotBe(referenceDummy);

            result1.Should().Contain(ExpectedCharacter);
            result2.Should().Contain(ExpectedCharacter);
            result3.Should().Contain(ExpectedCharacter);
            result4.Should().Contain(ExpectedCharacter);
            result5.Should().Contain(ExpectedCharacter);
        }

        [Fact]
        public static void ThatIsNot___Should_throw_InvalidOperation___When_maxAttempts_is_1_and_referenceDummy_equals_comparisonDummy()
        {
            // Arrange
            var referenceDummy = A.Dummy<string>();

            // Act
            var ex = Record.Exception(() => referenceDummy.ThatIsNot(referenceDummy, maxAttempts: 1));

            // Assert
            ex.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void ThatIsNot___Should_throw_InvalidOperation___When_all_possible_dummies_equal_comparisonDummy_and_maxAttempts_is_greater_than_1()
        {
            // Arrange
            var comparisonDummy = A.Dummy<AllInstancesEqual>();

            // Act
            var ex1 = Record.Exception(() => A.Dummy<AllInstancesEqual>().ThatIsNot(comparisonDummy));
            var ex2 = Record.Exception(() => A.Dummy<AllInstancesEqual>().ThatIsNot(comparisonDummy, maxAttempts: 101));

            // Assert
            ex1.Should().BeOfType<InvalidOperationException>();
            ex2.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void ThatIsNot___Should_return_referenceDummy___When_referenceDummy_is_not_equal_to_comparisonDummy_regardless_of_maxAttempts()
        {
            // Arrange
            var referenceDummy = A.Dummy<NoInstancesAreEqual>();
            var comparisonDummy = A.Dummy<NoInstancesAreEqual>();

            // Act
            // ReSharper disable RedundantArgumentName
            var result1 = referenceDummy.ThatIsNot(comparisonDummy);
            var result2 = referenceDummy.ThatIsNot(comparisonDummy, maxAttempts: 101);
            var result3 = referenceDummy.ThatIsNot(comparisonDummy, maxAttempts: 0);
            var result4 = referenceDummy.ThatIsNot(comparisonDummy, maxAttempts: -1);
            var result5 = referenceDummy.ThatIsNot(comparisonDummy, maxAttempts: -1000);
            // ReSharper restore RedundantArgumentName

            // Assert
            result1.Should().BeSameAs(referenceDummy);
            result2.Should().BeSameAs(referenceDummy);
            result3.Should().BeSameAs(referenceDummy);
            result4.Should().BeSameAs(referenceDummy);
            result5.Should().BeSameAs(referenceDummy);
        }

        [Fact]
        public static void ThatIsNot___Should_return_new_dummy___When_referenceDummy_reference_equals_comparisonDummy_regardless_of_maxAttempts()
        {
            // Arrange
            var referenceDummy = A.Dummy<NoInstancesAreEqual>();

            // Act
            // ReSharper disable RedundantArgumentName
            var result1 = referenceDummy.ThatIsNot(referenceDummy);
            var result2 = referenceDummy.ThatIsNot(referenceDummy, maxAttempts: 101);
            var result3 = referenceDummy.ThatIsNot(referenceDummy, maxAttempts: 0);
            var result4 = referenceDummy.ThatIsNot(referenceDummy, maxAttempts: -1);
            var result5 = referenceDummy.ThatIsNot(referenceDummy, maxAttempts: -1000);
            // ReSharper restore RedundantArgumentName

            // Assert
            result1.Should().NotBeNull().And.NotBeSameAs(referenceDummy);
            result2.Should().NotBeNull().And.NotBeSameAs(referenceDummy);
            result3.Should().NotBeNull().And.NotBeSameAs(referenceDummy);
            result4.Should().NotBeNull().And.NotBeSameAs(referenceDummy);
            result5.Should().NotBeNull().And.NotBeSameAs(referenceDummy);
        }

        [Fact]
        public static void ThatIsNot___Should_return_new_dummy___When_referenceDummy_value_equals_comparisonDummy_regardless_of_maxAttempts()
        {
            // Arrange
            var now = DateTime.Now;
            var referenceDummy = now.ToString(CultureInfo.CurrentCulture);
            var comparisonDummy = now.ToString(CultureInfo.CurrentCulture);

            // Act
            // ReSharper disable RedundantArgumentName
            var result1 = referenceDummy.ThatIsNot(comparisonDummy);
            var result2 = referenceDummy.ThatIsNot(comparisonDummy, maxAttempts: 101);
            var result3 = referenceDummy.ThatIsNot(comparisonDummy, maxAttempts: 0);
            var result4 = referenceDummy.ThatIsNot(comparisonDummy, maxAttempts: -1);
            var result5 = referenceDummy.ThatIsNot(comparisonDummy, maxAttempts: -1000);
            // ReSharper restore RedundantArgumentName

            // Assert
            referenceDummy.Should().NotBeSameAs(comparisonDummy);
            result1.Should().NotBe(referenceDummy);
            result2.Should().NotBe(referenceDummy);
            result3.Should().NotBe(referenceDummy);
            result4.Should().NotBe(referenceDummy);
            result5.Should().NotBe(referenceDummy);
        }

        [Fact]
        public static void ThatIsNot___Should_return_referenceDummy___When_referenceDummy_is_not_null_and_comparisonDummy_is_null_regardless_of_maxAttempts()
        {
            // Arrange
            var referenceDummy = A.Dummy<NoInstancesAreEqual>();

            // Act
            // ReSharper disable RedundantArgumentName
            // ReSharper disable ExpressionIsAlwaysNull
            var result1 = referenceDummy.ThatIsNot(null);
            var result2 = referenceDummy.ThatIsNot(null, maxAttempts: 101);
            var result3 = referenceDummy.ThatIsNot(null, maxAttempts: 0);
            var result4 = referenceDummy.ThatIsNot(null, maxAttempts: -1);
            var result5 = referenceDummy.ThatIsNot(null, maxAttempts: -1000);
            // ReSharper restore ExpressionIsAlwaysNull
            // ReSharper restore RedundantArgumentName

            // Assert
            result1.Should().BeSameAs(referenceDummy);
            result2.Should().BeSameAs(referenceDummy);
            result3.Should().BeSameAs(referenceDummy);
            result4.Should().BeSameAs(referenceDummy);
            result5.Should().BeSameAs(referenceDummy);
        }

        [Fact]
        public static void ThatIsNot___Should_return_null___When_referenceDummy_is_null_and_comparisonDummy_is_not_null_regardless_of_maxAttempts()
        {
            // Arrange
            NoInstancesAreEqual referenceDummy = null;
            var comparisonDummy = A.Dummy<NoInstancesAreEqual>();

            // Act
            // ReSharper disable RedundantArgumentName
            // ReSharper disable ExpressionIsAlwaysNull
            var result1 = referenceDummy.ThatIsNot(comparisonDummy);
            var result2 = referenceDummy.ThatIsNot(comparisonDummy, maxAttempts: 101);
            var result3 = referenceDummy.ThatIsNot(comparisonDummy, maxAttempts: 0);
            var result4 = referenceDummy.ThatIsNot(comparisonDummy, maxAttempts: -1);
            var result5 = referenceDummy.ThatIsNot(comparisonDummy, maxAttempts: -1000);
            // ReSharper restore ExpressionIsAlwaysNull
            // ReSharper restore RedundantArgumentName

            // Assert
            result1.Should().BeNull();
            result2.Should().BeNull();
            result3.Should().BeNull();
            result4.Should().BeNull();
            result5.Should().BeNull();
        }

        private static bool ConditionThatCannnotBeMet(string input)
        {
            return false;
        }

        private static bool ConditionThatsAlwaysMet(string input)
        {
            return true;
        }

        // ReSharper restore InconsistentNaming
    }
}
