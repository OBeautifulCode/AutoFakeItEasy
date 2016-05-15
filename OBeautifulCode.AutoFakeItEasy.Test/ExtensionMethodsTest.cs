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
    using System.Linq;

    using FakeItEasy;

    using FluentAssertions;

    using OBeautifulCode.Math;

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
            var ex1 = Record.Exception(() => A.Dummy<int>().ThatIs(null));
            var ex2 = Record.Exception(() => Some.Dummies<int>().ThatIs(null));
            var ex3 = Record.Exception(() => Some.ReadOnlyDummies<int>().ThatIs(null));

            // Assert
            ex1.Should().BeOfType<ArgumentNullException>();
            ex2.Should().BeOfType<ArgumentNullException>();
            ex3.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void ThatIs___Should_throw_InvalidOperation___When_maxAttempts_is_1_and_referenceDummy_does_not_meet_the_condition()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => A.Dummy<string>().ThatIs(ConditionThatCannnotBeMet, maxAttempts: 1));
            var ex2 = Record.Exception(() => Some.Dummies<string>().ThatIs(ConditionThatCannnotBeMet, maxAttempts: 1));
            var ex3 = Record.Exception(() => Some.ReadOnlyDummies<string>().ThatIs(ConditionThatCannnotBeMet, maxAttempts: 1));

            // Assert
            ex1.Should().BeOfType<InvalidOperationException>();
            ex2.Should().BeOfType<InvalidOperationException>();
            ex3.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void ThatIs___Should_throw_InvalidOperation___When_condition_cannot_be_met_and_maxAttempts_is_greater_than_one()
        {
            // Arrange, Act
            var ex1a = Record.Exception(() => A.Dummy<string>().ThatIs(ConditionThatCannnotBeMet));
            var ex1b = Record.Exception(() => A.Dummy<string>().ThatIs(ConditionThatCannnotBeMet, maxAttempts: 101));

            var ex2a = Record.Exception(() => Some.Dummies<string>().ThatIs(ConditionThatCannnotBeMet));
            var ex2b = Record.Exception(() => Some.Dummies<string>().ThatIs(ConditionThatCannnotBeMet, maxAttempts: 101));

            var ex3a = Record.Exception(() => Some.ReadOnlyDummies<string>().ThatIs(ConditionThatCannnotBeMet));
            var ex3b = Record.Exception(() => Some.ReadOnlyDummies<string>().ThatIs(ConditionThatCannnotBeMet, maxAttempts: 101));

            // Assert
            ex1a.Should().BeOfType<InvalidOperationException>();
            ex1b.Should().BeOfType<InvalidOperationException>();

            ex2a.Should().BeOfType<InvalidOperationException>();
            ex2b.Should().BeOfType<InvalidOperationException>();

            ex3a.Should().BeOfType<InvalidOperationException>();
            ex3b.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void ThatIs___Should_try_maxAttempts_number_of_times_to_meet_condition_before_throwing___When_condition_cannot_be_met_and_maxAttempts_is_positive()
        {
            // Arrange
            var referenceDummy1a = A.Dummy<string>();
            var dummies1a = new List<string>();
            Func<string, bool> condition1a = _ =>
            {
                dummies1a.Add(_);
                return false;
            };

            var referenceDummy1b = A.Dummy<string>();
            var dummies1b = new List<string>();
            Func<string, bool> condition1b = _ =>
            {
                dummies1b.Add(_);
                return false;
            };

            var referenceDummy2a = Some.Dummies<string>();
            var dummies2a = new List<IList<string>>();
            Func<IList<string>, bool> condition2a = _ =>
            {
                dummies2a.Add(_);
                return false;
            };

            var refereceDummy2b = Some.Dummies<string>();
            var dummies2b = new List<IList<string>>();
            Func<IList<string>, bool> condition2b = _ =>
            {
                dummies2b.Add(_);
                return false;
            };

            var referenceDummy3a = Some.ReadOnlyDummies<string>();
            var dummies3a = new List<IReadOnlyList<string>>();
            Func<IReadOnlyList<string>, bool> condition3a = _ =>
            {
                dummies3a.Add(_);
                return false;
            };

            var refereceDummy3b = Some.ReadOnlyDummies<string>();
            var dummies3b = new List<IReadOnlyList<string>>();
            Func<IReadOnlyList<string>, bool> condition3b = _ =>
            {
                dummies3b.Add(_);
                return false;
            };

            // Act
            var ex1a = Record.Exception(() => referenceDummy1a.ThatIs(condition1a, maxAttempts: 1));
            var ex1b = Record.Exception(() => referenceDummy1b.ThatIs(condition1b, maxAttempts: 101));

            var ex2a = Record.Exception(() => referenceDummy2a.ThatIs(condition2a, maxAttempts: 1));
            var ex2b = Record.Exception(() => refereceDummy2b.ThatIs(condition2b, maxAttempts: 101));

            var ex3a = Record.Exception(() => referenceDummy3a.ThatIs(condition3a, maxAttempts: 1));
            var ex3b = Record.Exception(() => refereceDummy3b.ThatIs(condition3b, maxAttempts: 101));

            // Assert
            ex1a.Should().BeOfType<InvalidOperationException>();
            ex1b.Should().BeOfType<InvalidOperationException>();

            ex2a.Should().BeOfType<InvalidOperationException>();
            ex2b.Should().BeOfType<InvalidOperationException>();

            ex3a.Should().BeOfType<InvalidOperationException>();
            ex3b.Should().BeOfType<InvalidOperationException>();

            dummies1a.Should().HaveCount(1);
            dummies1b.Should().HaveCount(101);

            dummies2a.Should().HaveCount(1);
            dummies2b.Should().HaveCount(101);

            dummies1a.Should().StartWith(referenceDummy1a);
            dummies1b.Should().StartWith(referenceDummy1b);

            dummies2a.Should().StartWith(referenceDummy2a);
            dummies2b.Should().StartWith(refereceDummy2b);

            dummies3a.Should().StartWith(referenceDummy3a);
            dummies3b.Should().StartWith(refereceDummy3b);
        }

        [Fact]
        public static void ThatIs___Should_try_an_infinite_number_of_times_to_meet_condition___When_condition_cannot_be_met_and_maxAttempts_is_zero_or_negative()
        {
            // Arrange
            const int NegativeMaxAttempts = -1000;
            const int InfiniteAttemptsMax = 1000;
            var attempts1a = 0;
            var attempts1b = 0;
            var attempts2a = 0;
            var attempts2b = 0;
            var attempts3a = 0;
            var attempts3b = 0;

            Func<string, bool> condition1a = _ =>
                {
                    attempts1a++;
                    if (attempts1a >= InfiniteAttemptsMax)
                    {
                        throw new OverflowException();
                    }

                    return false;
                };

            Func<string, bool> condition1b = _ =>
            {
                attempts1b++;
                if (attempts1b >= InfiniteAttemptsMax)
                {
                    throw new OverflowException();
                }

                return false;
            };

            Func<IList<string>, bool> condition2a = _ =>
            {
                attempts2a++;
                if (attempts2a >= InfiniteAttemptsMax)
                {
                    throw new OverflowException();
                }

                return false;
            };

            Func<IList<string>, bool> condition2b = _ =>
            {
                attempts2b++;
                if (attempts2b >= InfiniteAttemptsMax)
                {
                    throw new OverflowException();
                }

                return false;
            };

            Func<IReadOnlyList<string>, bool> condition3a = _ =>
            {
                attempts3a++;
                if (attempts3a >= InfiniteAttemptsMax)
                {
                    throw new OverflowException();
                }

                return false;
            };

            Func<IReadOnlyList<string>, bool> condition3b = _ =>
            {
                attempts3b++;
                if (attempts3b >= InfiniteAttemptsMax)
                {
                    throw new OverflowException();
                }

                return false;
            };

            // Act
            var ex1a = Record.Exception(() => A.Dummy<string>().ThatIs(condition1a, maxAttempts: 0));
            var ex1b = Record.Exception(() => A.Dummy<string>().ThatIs(condition1b, NegativeMaxAttempts));
            var ex2a = Record.Exception(() => Some.Dummies<string>().ThatIs(condition2a, maxAttempts: 0));
            var ex2b = Record.Exception(() => Some.Dummies<string>().ThatIs(condition2b, NegativeMaxAttempts));
            var ex3a = Record.Exception(() => Some.ReadOnlyDummies<string>().ThatIs(condition3a, maxAttempts: 0));
            var ex3b = Record.Exception(() => Some.ReadOnlyDummies<string>().ThatIs(condition3b, NegativeMaxAttempts));

            // Assert
            ex1a.Should().BeOfType<OverflowException>();
            ex1b.Should().BeOfType<OverflowException>();

            ex2a.Should().BeOfType<OverflowException>();
            ex2b.Should().BeOfType<OverflowException>();

            ex3a.Should().BeOfType<OverflowException>();
            ex3b.Should().BeOfType<OverflowException>();

            attempts1a.Should().Be(InfiniteAttemptsMax);
            attempts1b.Should().Be(InfiniteAttemptsMax);

            attempts2a.Should().Be(InfiniteAttemptsMax);
            attempts2b.Should().Be(InfiniteAttemptsMax);

            attempts3a.Should().Be(InfiniteAttemptsMax);
            attempts3b.Should().Be(InfiniteAttemptsMax);
        }

        [Fact]
        public static void ThatIs___Should_return_referenceDummy___When_referenceDummy_meets_condition_regardless_of_maxAttempts()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<string>();
            var referenceDummy2 = Some.Dummies<string>();
            var referenceDummy3 = Some.ReadOnlyDummies<string>();

            // Act
            // ReSharper disable RedundantArgumentName
            var result1a = referenceDummy1.ThatIs(ConditionThatsAlwaysMet);
            var result1b = referenceDummy1.ThatIs(ConditionThatsAlwaysMet, maxAttempts: 101);
            var result1c = referenceDummy1.ThatIs(ConditionThatsAlwaysMet, maxAttempts: 0);
            var result1d = referenceDummy1.ThatIs(ConditionThatsAlwaysMet, maxAttempts: -1);
            var result1e = referenceDummy1.ThatIs(ConditionThatsAlwaysMet, maxAttempts: -1000);

            var result2a = referenceDummy2.ThatIs(ConditionThatsAlwaysMet);
            var result2b = referenceDummy2.ThatIs(ConditionThatsAlwaysMet, maxAttempts: 101);
            var result2c = referenceDummy2.ThatIs(ConditionThatsAlwaysMet, maxAttempts: 0);
            var result2d = referenceDummy2.ThatIs(ConditionThatsAlwaysMet, maxAttempts: -1);
            var result2e = referenceDummy2.ThatIs(ConditionThatsAlwaysMet, maxAttempts: -1000);

            var result3a = referenceDummy3.ThatIs(ConditionThatsAlwaysMet);
            var result3b = referenceDummy3.ThatIs(ConditionThatsAlwaysMet, maxAttempts: 101);
            var result3c = referenceDummy3.ThatIs(ConditionThatsAlwaysMet, maxAttempts: 0);
            var result3d = referenceDummy3.ThatIs(ConditionThatsAlwaysMet, maxAttempts: -1);
            var result3e = referenceDummy3.ThatIs(ConditionThatsAlwaysMet, maxAttempts: -1000);
            // ReSharper restore RedundantArgumentName

            // Assert
            result1a.Should().Be(referenceDummy1);
            result1b.Should().Be(referenceDummy1);
            result1c.Should().Be(referenceDummy1);
            result1d.Should().Be(referenceDummy1);
            result1e.Should().Be(referenceDummy1);

            result2a.Should().Equal(referenceDummy2);
            result2b.Should().Equal(referenceDummy2);
            result2c.Should().Equal(referenceDummy2);
            result2d.Should().Equal(referenceDummy2);
            result2e.Should().Equal(referenceDummy2);

            result3a.Should().Equal(referenceDummy3);
            result3b.Should().Equal(referenceDummy3);
            result3c.Should().Equal(referenceDummy3);
            result3d.Should().Equal(referenceDummy3);
            result3e.Should().Equal(referenceDummy3);
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
        public static void ThatIs___Should_return_dummy_that_meets_condition___When_referenceDummy_is_generated_via_ADummy_and_does_not_meet_condition_but_condition_can_be_satisfied_after_one_or_more_attempts()
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
        public static void ThatIs___Should_return_dummy_that_meets_condition___When_referenceDummy_is_generated_via_SomeDummies_and_does_not_meet_condition_but_condition_can_be_satisfied_after_one_or_more_attempts()
        {
            // Arrange
            const int MinCount = 5;
            var referenceDummy = Some.Dummies<string>();
            referenceDummy.Clear();
            Func<IList<string>, bool> condition = _ => _.Count >= MinCount;

            // Act
            // ReSharper disable RedundantArgumentName
            var result1 = referenceDummy.ThatIs(condition);
            var result2 = referenceDummy.ThatIs(condition, maxAttempts: 101);
            var result3 = referenceDummy.ThatIs(condition, maxAttempts: 0);
            var result4 = referenceDummy.ThatIs(condition, maxAttempts: -1);
            var result5 = referenceDummy.ThatIs(condition, maxAttempts: -1000);
            // ReSharper restore RedundantArgumentName

            // Assert
            result1.Count.Should().BeGreaterOrEqualTo(MinCount);
            result2.Count.Should().BeGreaterOrEqualTo(MinCount);
            result3.Count.Should().BeGreaterOrEqualTo(MinCount);
            result4.Count.Should().BeGreaterOrEqualTo(MinCount);
            result5.Count.Should().BeGreaterOrEqualTo(MinCount);
        }

        [Fact]
        public static void ThatIs___Should_return_dummy_that_meets_condition___When_referenceDummy_is_generated_via_SomeReadOnlyDummies_and_does_not_meet_condition_but_condition_can_be_satisfied_after_one_or_more_attempts()
        {
            // Arrange
            const int MinCount = 5;
            var referenceDummy = new SomeReadOnlyDummiesList<double>(new List<double>(), -1, CreateWith.NoNulls);
            Func<IReadOnlyList<double>, bool> condition = _ => _.Count >= MinCount;

            // Act
            // ReSharper disable RedundantArgumentName
            var result1 = referenceDummy.ThatIs(condition);
            var result2 = referenceDummy.ThatIs(condition, maxAttempts: 101);
            var result3 = referenceDummy.ThatIs(condition, maxAttempts: 0);
            var result4 = referenceDummy.ThatIs(condition, maxAttempts: -1);
            var result5 = referenceDummy.ThatIs(condition, maxAttempts: -1000);
            // ReSharper restore RedundantArgumentName

            // Assert
            result1.Count.Should().BeGreaterOrEqualTo(MinCount);
            result2.Count.Should().BeGreaterOrEqualTo(MinCount);
            result3.Count.Should().BeGreaterOrEqualTo(MinCount);
            result4.Count.Should().BeGreaterOrEqualTo(MinCount);
            result5.Count.Should().BeGreaterOrEqualTo(MinCount);
        }

        [Fact]
        public static void ThatIs___Should_return_IList_with_specified_numberOfElements_and_specified_createWith___When_referenceDummy_is_generated_via_SomeDummies_and_does_not_meet_condition_but_condition_can_be_satisfied_after_one_or_more_attempts()
        {
            // Arrange
            var expectedSize1 = ThreadSafeRandom.Next(1, 10);
            var referenceDummy1 = Some.Dummies<string>(expectedSize1, CreateWith.OneOrMoreNulls);
            referenceDummy1.Clear();
            Func<IList<string>, bool> trueOnFirstRetryCondition = _ => _.Count != 0;

            var expectedSize2 = ThreadSafeRandom.Next(2, 10);
            var referenceDummy2 = Some.Dummies<string>(expectedSize2, CreateWith.ZeroOrMoreNulls);
            referenceDummy2.Clear();
            Func<IList<string>, bool> trueAfterSeveralRetriesCondition = _ => _.Count(s => s == null) > 1;

            // Act
            // ReSharper disable RedundantArgumentName
            var result1 = referenceDummy1.ThatIs(trueOnFirstRetryCondition, maxAttempts: 2);
            var result2 = referenceDummy2.ThatIs(trueAfterSeveralRetriesCondition, maxAttempts: 0);
            // ReSharper restore RedundantArgumentName

            // Assert
            result1.Should().BeAssignableTo<IList<string>>();
            result1.Should().HaveCount(expectedSize1);
            result1.Should().Contain((string)null);

            result2.Should().BeAssignableTo<IList<string>>();
            result2.Should().HaveCount(expectedSize2);
            result2.Count(_ => _ == null).Should().BeGreaterThan(1);
        }

        [Fact]
        public static void ThatIs___Should_return_IReadOnlyList_with_specified_numberOfElements_and_specified_createWith___When_referenceDummy_is_generated_via_SomeReadOnlyDummies_and_does_not_meet_condition_but_condition_can_be_satisfied_after_one_or_more_attempts()
        {
            // Arrange
            var expectedSize1 = ThreadSafeRandom.Next(1, 10);
            var referenceDummy1 = new SomeReadOnlyDummiesList<string>(new List<string>(), expectedSize1, CreateWith.OneOrMoreNulls);
            Func<IReadOnlyList<string>, bool> trueOnFirstRetryCondition = _ => _.Count != 0;

            var expectedSize2 = ThreadSafeRandom.Next(2, 10);
            var referenceDummy2 = new SomeReadOnlyDummiesList<string>(new List<string>(), expectedSize2, CreateWith.ZeroOrMoreNulls);
            Func<IReadOnlyList<string>, bool> trueAfterSeveralRetriesCondition = _ => _.Count(s => s == null) > 1;

            // Act
            // ReSharper disable RedundantArgumentName
            var result1 = referenceDummy1.ThatIs(trueOnFirstRetryCondition, maxAttempts: 2);
            var result2 = referenceDummy2.ThatIs(trueAfterSeveralRetriesCondition, maxAttempts: 0);
            // ReSharper restore RedundantArgumentName

            // Assert
            result1.Should().BeAssignableTo<IReadOnlyList<string>>();
            result1.Should().HaveCount(expectedSize1);
            result1.Should().Contain((string)null);

            result2.Should().BeAssignableTo<IReadOnlyList<string>>();
            result2.Should().HaveCount(expectedSize2);
            result2.Count(_ => _ == null).Should().BeGreaterThan(1);
        }

        [Fact]
        public static void Whose___Should_throw_ArgumentNullException___When_parameter_condition_is_null()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => A.Dummy<int>().Whose(null));
            var ex2 = Record.Exception(() => Some.Dummies<int>().Whose(null));
            var ex3 = Record.Exception(() => Some.ReadOnlyDummies<int>().Whose(null));

            // Assert
            ex1.Should().BeOfType<ArgumentNullException>();
            ex2.Should().BeOfType<ArgumentNullException>();
            ex3.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Whose___Should_throw_InvalidOperation___When_maxAttempts_is_1_and_referenceDummy_does_not_meet_the_condition()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => A.Dummy<string>().Whose(ConditionThatCannnotBeMet, maxAttempts: 1));
            var ex2 = Record.Exception(() => Some.Dummies<string>().Whose(ConditionThatCannnotBeMet, maxAttempts: 1));
            var ex3 = Record.Exception(() => Some.ReadOnlyDummies<string>().Whose(ConditionThatCannnotBeMet, maxAttempts: 1));

            // Assert
            ex1.Should().BeOfType<InvalidOperationException>();
            ex2.Should().BeOfType<InvalidOperationException>();
            ex3.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void Whose___Should_throw_InvalidOperation___When_condition_cannot_be_met_and_maxAttempts_is_greater_than_one()
        {
            // Arrange, Act
            var ex1a = Record.Exception(() => A.Dummy<string>().Whose(ConditionThatCannnotBeMet));
            var ex1b = Record.Exception(() => A.Dummy<string>().Whose(ConditionThatCannnotBeMet, maxAttempts: 101));

            var ex2a = Record.Exception(() => Some.Dummies<string>().Whose(ConditionThatCannnotBeMet));
            var ex2b = Record.Exception(() => Some.Dummies<string>().Whose(ConditionThatCannnotBeMet, maxAttempts: 101));

            var ex3a = Record.Exception(() => Some.ReadOnlyDummies<string>().Whose(ConditionThatCannnotBeMet));
            var ex3b = Record.Exception(() => Some.ReadOnlyDummies<string>().Whose(ConditionThatCannnotBeMet, maxAttempts: 101));

            // Assert
            ex1a.Should().BeOfType<InvalidOperationException>();
            ex1b.Should().BeOfType<InvalidOperationException>();

            ex2a.Should().BeOfType<InvalidOperationException>();
            ex2b.Should().BeOfType<InvalidOperationException>();

            ex3a.Should().BeOfType<InvalidOperationException>();
            ex3b.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void Whose___Should_try_maxAttempts_number_of_times_to_meet_condition_before_throwing___When_condition_cannot_be_met_and_maxAttempts_is_positive()
        {
            // Arrange
            var referenceDummy1a = A.Dummy<string>();
            var dummies1a = new List<string>();
            Func<string, bool> condition1a = _ =>
            {
                dummies1a.Add(_);
                return false;
            };

            var referenceDummy1b = A.Dummy<string>();
            var dummies1b = new List<string>();
            Func<string, bool> condition1b = _ =>
            {
                dummies1b.Add(_);
                return false;
            };

            var referenceDummy2a = Some.Dummies<string>();
            var dummies2a = new List<IList<string>>();
            Func<IList<string>, bool> condition2a = _ =>
            {
                dummies2a.Add(_);
                return false;
            };

            var refereceDummy2b = Some.Dummies<string>();
            var dummies2b = new List<IList<string>>();
            Func<IList<string>, bool> condition2b = _ =>
            {
                dummies2b.Add(_);
                return false;
            };

            var referenceDummy3a = Some.ReadOnlyDummies<string>();
            var dummies3a = new List<IReadOnlyList<string>>();
            Func<IReadOnlyList<string>, bool> condition3a = _ =>
            {
                dummies3a.Add(_);
                return false;
            };

            var refereceDummy3b = Some.ReadOnlyDummies<string>();
            var dummies3b = new List<IReadOnlyList<string>>();
            Func<IReadOnlyList<string>, bool> condition3b = _ =>
            {
                dummies3b.Add(_);
                return false;
            };

            // Act
            var ex1a = Record.Exception(() => referenceDummy1a.Whose(condition1a, maxAttempts: 1));
            var ex1b = Record.Exception(() => referenceDummy1b.Whose(condition1b, maxAttempts: 101));

            var ex2a = Record.Exception(() => referenceDummy2a.Whose(condition2a, maxAttempts: 1));
            var ex2b = Record.Exception(() => refereceDummy2b.Whose(condition2b, maxAttempts: 101));

            var ex3a = Record.Exception(() => referenceDummy3a.Whose(condition3a, maxAttempts: 1));
            var ex3b = Record.Exception(() => refereceDummy3b.Whose(condition3b, maxAttempts: 101));

            // Assert
            ex1a.Should().BeOfType<InvalidOperationException>();
            ex1b.Should().BeOfType<InvalidOperationException>();

            ex2a.Should().BeOfType<InvalidOperationException>();
            ex2b.Should().BeOfType<InvalidOperationException>();

            ex3a.Should().BeOfType<InvalidOperationException>();
            ex3b.Should().BeOfType<InvalidOperationException>();

            dummies1a.Should().HaveCount(1);
            dummies1b.Should().HaveCount(101);

            dummies2a.Should().HaveCount(1);
            dummies2b.Should().HaveCount(101);

            dummies1a.Should().StartWith(referenceDummy1a);
            dummies1b.Should().StartWith(referenceDummy1b);

            dummies2a.Should().StartWith(referenceDummy2a);
            dummies2b.Should().StartWith(refereceDummy2b);

            dummies3a.Should().StartWith(referenceDummy3a);
            dummies3b.Should().StartWith(refereceDummy3b);
        }

        [Fact]
        public static void Whose___Should_try_an_infinite_number_of_times_to_meet_condition___When_condition_cannot_be_met_and_maxAttempts_is_zero_or_negative()
        {
            // Arrange
            const int NegativeMaxAttempts = -1000;
            const int InfiniteAttemptsMax = 1000;
            var attempts1a = 0;
            var attempts1b = 0;
            var attempts2a = 0;
            var attempts2b = 0;
            var attempts3a = 0;
            var attempts3b = 0;

            Func<string, bool> condition1a = _ =>
            {
                attempts1a++;
                if (attempts1a >= InfiniteAttemptsMax)
                {
                    throw new OverflowException();
                }

                return false;
            };

            Func<string, bool> condition1b = _ =>
            {
                attempts1b++;
                if (attempts1b >= InfiniteAttemptsMax)
                {
                    throw new OverflowException();
                }

                return false;
            };

            Func<IList<string>, bool> condition2a = _ =>
            {
                attempts2a++;
                if (attempts2a >= InfiniteAttemptsMax)
                {
                    throw new OverflowException();
                }

                return false;
            };

            Func<IList<string>, bool> condition2b = _ =>
            {
                attempts2b++;
                if (attempts2b >= InfiniteAttemptsMax)
                {
                    throw new OverflowException();
                }

                return false;
            };

            Func<IReadOnlyList<string>, bool> condition3a = _ =>
            {
                attempts3a++;
                if (attempts3a >= InfiniteAttemptsMax)
                {
                    throw new OverflowException();
                }

                return false;
            };

            Func<IReadOnlyList<string>, bool> condition3b = _ =>
            {
                attempts3b++;
                if (attempts3b >= InfiniteAttemptsMax)
                {
                    throw new OverflowException();
                }

                return false;
            };

            // Act
            var ex1a = Record.Exception(() => A.Dummy<string>().Whose(condition1a, maxAttempts: 0));
            var ex1b = Record.Exception(() => A.Dummy<string>().Whose(condition1b, NegativeMaxAttempts));
            var ex2a = Record.Exception(() => Some.Dummies<string>().Whose(condition2a, maxAttempts: 0));
            var ex2b = Record.Exception(() => Some.Dummies<string>().Whose(condition2b, NegativeMaxAttempts));
            var ex3a = Record.Exception(() => Some.ReadOnlyDummies<string>().Whose(condition3a, maxAttempts: 0));
            var ex3b = Record.Exception(() => Some.ReadOnlyDummies<string>().Whose(condition3b, NegativeMaxAttempts));

            // Assert
            ex1a.Should().BeOfType<OverflowException>();
            ex1b.Should().BeOfType<OverflowException>();

            ex2a.Should().BeOfType<OverflowException>();
            ex2b.Should().BeOfType<OverflowException>();

            ex3a.Should().BeOfType<OverflowException>();
            ex3b.Should().BeOfType<OverflowException>();

            attempts1a.Should().Be(InfiniteAttemptsMax);
            attempts1b.Should().Be(InfiniteAttemptsMax);

            attempts2a.Should().Be(InfiniteAttemptsMax);
            attempts2b.Should().Be(InfiniteAttemptsMax);

            attempts3a.Should().Be(InfiniteAttemptsMax);
            attempts3b.Should().Be(InfiniteAttemptsMax);
        }

        [Fact]
        public static void Whose___Should_return_referenceDummy___When_referenceDummy_meets_condition_regardless_of_maxAttempts()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<string>();
            var referenceDummy2 = Some.Dummies<string>();
            var referenceDummy3 = Some.ReadOnlyDummies<string>();

            // Act
            // ReSharper disable RedundantArgumentName
            var result1a = referenceDummy1.Whose(ConditionThatsAlwaysMet);
            var result1b = referenceDummy1.Whose(ConditionThatsAlwaysMet, maxAttempts: 101);
            var result1c = referenceDummy1.Whose(ConditionThatsAlwaysMet, maxAttempts: 0);
            var result1d = referenceDummy1.Whose(ConditionThatsAlwaysMet, maxAttempts: -1);
            var result1e = referenceDummy1.Whose(ConditionThatsAlwaysMet, maxAttempts: -1000);

            var result2a = referenceDummy2.Whose(ConditionThatsAlwaysMet);
            var result2b = referenceDummy2.Whose(ConditionThatsAlwaysMet, maxAttempts: 101);
            var result2c = referenceDummy2.Whose(ConditionThatsAlwaysMet, maxAttempts: 0);
            var result2d = referenceDummy2.Whose(ConditionThatsAlwaysMet, maxAttempts: -1);
            var result2e = referenceDummy2.Whose(ConditionThatsAlwaysMet, maxAttempts: -1000);

            var result3a = referenceDummy3.Whose(ConditionThatsAlwaysMet);
            var result3b = referenceDummy3.Whose(ConditionThatsAlwaysMet, maxAttempts: 101);
            var result3c = referenceDummy3.Whose(ConditionThatsAlwaysMet, maxAttempts: 0);
            var result3d = referenceDummy3.Whose(ConditionThatsAlwaysMet, maxAttempts: -1);
            var result3e = referenceDummy3.Whose(ConditionThatsAlwaysMet, maxAttempts: -1000);
            // ReSharper restore RedundantArgumentName

            // Assert
            result1a.Should().Be(referenceDummy1);
            result1b.Should().Be(referenceDummy1);
            result1c.Should().Be(referenceDummy1);
            result1d.Should().Be(referenceDummy1);
            result1e.Should().Be(referenceDummy1);

            result2a.Should().Equal(referenceDummy2);
            result2b.Should().Equal(referenceDummy2);
            result2c.Should().Equal(referenceDummy2);
            result2d.Should().Equal(referenceDummy2);
            result2e.Should().Equal(referenceDummy2);

            result3a.Should().Equal(referenceDummy3);
            result3b.Should().Equal(referenceDummy3);
            result3c.Should().Equal(referenceDummy3);
            result3d.Should().Equal(referenceDummy3);
            result3e.Should().Equal(referenceDummy3);
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
        public static void Whose___Should_return_dummy_that_meets_condition___When_referenceDummy_is_generated_via_ADummy_and_does_not_meet_condition_but_condition_can_be_satisfied_after_one_or_more_attempts()
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
        public static void Whose___Should_return_dummy_that_meets_condition___When_referenceDummy_is_generated_via_SomeDummies_and_does_not_meet_condition_but_condition_can_be_satisfied_after_one_or_more_attempts()
        {
            // Arrange
            const int MinCount = 5;
            var referenceDummy = Some.Dummies<string>();
            referenceDummy.Clear();
            Func<IList<string>, bool> condition = _ => _.Count >= MinCount;

            // Act
            // ReSharper disable RedundantArgumentName
            var result1 = referenceDummy.Whose(condition);
            var result2 = referenceDummy.Whose(condition, maxAttempts: 101);
            var result3 = referenceDummy.Whose(condition, maxAttempts: 0);
            var result4 = referenceDummy.Whose(condition, maxAttempts: -1);
            var result5 = referenceDummy.Whose(condition, maxAttempts: -1000);
            // ReSharper restore RedundantArgumentName

            // Assert
            result1.Count.Should().BeGreaterOrEqualTo(MinCount);
            result2.Count.Should().BeGreaterOrEqualTo(MinCount);
            result3.Count.Should().BeGreaterOrEqualTo(MinCount);
            result4.Count.Should().BeGreaterOrEqualTo(MinCount);
            result5.Count.Should().BeGreaterOrEqualTo(MinCount);
        }

        [Fact]
        public static void Whose___Should_return_dummy_that_meets_condition___When_referenceDummy_is_generated_via_SomeReadOnlyDummies_and_does_not_meet_condition_but_condition_can_be_satisfied_after_one_or_more_attempts()
        {
            // Arrange
            const int MinCount = 5;
            var referenceDummy = new SomeReadOnlyDummiesList<double>(new List<double>(), -1, CreateWith.NoNulls);
            Func<IReadOnlyList<double>, bool> condition = _ => _.Count >= MinCount;

            // Act
            // ReSharper disable RedundantArgumentName
            var result1 = referenceDummy.Whose(condition);
            var result2 = referenceDummy.Whose(condition, maxAttempts: 101);
            var result3 = referenceDummy.Whose(condition, maxAttempts: 0);
            var result4 = referenceDummy.Whose(condition, maxAttempts: -1);
            var result5 = referenceDummy.Whose(condition, maxAttempts: -1000);
            // ReSharper restore RedundantArgumentName

            // Assert
            result1.Count.Should().BeGreaterOrEqualTo(MinCount);
            result2.Count.Should().BeGreaterOrEqualTo(MinCount);
            result3.Count.Should().BeGreaterOrEqualTo(MinCount);
            result4.Count.Should().BeGreaterOrEqualTo(MinCount);
            result5.Count.Should().BeGreaterOrEqualTo(MinCount);
        }

        [Fact]
        public static void Whose___Should_return_IList_with_specified_numberOfElements_and_specified_createWith___When_referenceDummy_is_generated_via_SomeDummies_and_does_not_meet_condition_but_condition_can_be_satisfied_after_one_or_more_attempts()
        {
            // Arrange
            var expectedSize1 = ThreadSafeRandom.Next(1, 10);
            var referenceDummy1 = Some.Dummies<string>(expectedSize1, CreateWith.OneOrMoreNulls);
            referenceDummy1.Clear();
            Func<IList<string>, bool> trueOnFirstRetryCondition = _ => _.Count != 0;

            var expectedSize2 = ThreadSafeRandom.Next(2, 10);
            var referenceDummy2 = Some.Dummies<string>(expectedSize2, CreateWith.ZeroOrMoreNulls);
            referenceDummy2.Clear();
            Func<IList<string>, bool> trueAfterSeveralRetriesCondition = _ => _.Count(s => s == null) > 1;

            // Act
            // ReSharper disable RedundantArgumentName
            var result1 = referenceDummy1.Whose(trueOnFirstRetryCondition, maxAttempts: 2);
            var result2 = referenceDummy2.Whose(trueAfterSeveralRetriesCondition, maxAttempts: 0);
            // ReSharper restore RedundantArgumentName

            // Assert
            result1.Should().BeAssignableTo<IList<string>>();
            result1.Should().HaveCount(expectedSize1);
            result1.Should().Contain((string)null);

            result2.Should().BeAssignableTo<IList<string>>();
            result2.Should().HaveCount(expectedSize2);
            result2.Count(_ => _ == null).Should().BeGreaterThan(1);
        }

        [Fact]
        public static void Whose___Should_return_IReadOnlyList_with_specified_numberOfElements_and_specified_createWith___When_referenceDummy_is_generated_via_SomeReadOnlyDummies_and_does_not_meet_condition_but_condition_can_be_satisfied_after_one_or_more_attempts()
        {
            // Arrange
            var expectedSize1 = ThreadSafeRandom.Next(1, 10);
            var referenceDummy1 = new SomeReadOnlyDummiesList<string>(new List<string>(), expectedSize1, CreateWith.OneOrMoreNulls);
            Func<IReadOnlyList<string>, bool> trueOnFirstRetryCondition = _ => _.Count != 0;

            var expectedSize2 = ThreadSafeRandom.Next(2, 10);
            var referenceDummy2 = new SomeReadOnlyDummiesList<string>(new List<string>(), expectedSize2, CreateWith.ZeroOrMoreNulls);
            Func<IReadOnlyList<string>, bool> trueAfterSeveralRetriesCondition = _ => _.Count(s => s == null) > 1;

            // Act
            // ReSharper disable RedundantArgumentName
            var result1 = referenceDummy1.Whose(trueOnFirstRetryCondition, maxAttempts: 2);
            var result2 = referenceDummy2.Whose(trueAfterSeveralRetriesCondition, maxAttempts: 0);
            // ReSharper restore RedundantArgumentName

            // Assert
            result1.Should().BeAssignableTo<IReadOnlyList<string>>();
            result1.Should().HaveCount(expectedSize1);
            result1.Should().Contain((string)null);

            result2.Should().BeAssignableTo<IReadOnlyList<string>>();
            result2.Should().HaveCount(expectedSize2);
            result2.Count(_ => _ == null).Should().BeGreaterThan(1);
        }

        [Fact]
        public static void ThatIsNot___Should_throw_InvalidOperation___When_maxAttempts_is_1_and_referenceDummy_equals_comparisonDummy()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<string>();
            var referenceDummy2 = Some.Dummies<string>();
            var referenceDummy3 = Some.ReadOnlyDummies<string>();

            // Act
            var ex1 = Record.Exception(() => referenceDummy1.ThatIsNot(referenceDummy1, maxAttempts: 1));
            var ex2 = Record.Exception(() => referenceDummy2.ThatIsNot(referenceDummy2, maxAttempts: 1));
            var ex3 = Record.Exception(() => referenceDummy3.ThatIsNot(referenceDummy3, maxAttempts: 1));

            // Assert
            ex1.Should().BeOfType<InvalidOperationException>();
            ex2.Should().BeOfType<InvalidOperationException>();
            ex3.Should().BeOfType<InvalidOperationException>();
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
            var referenceDummy1 = A.Dummy<NoInstancesAreEqual>();
            var comparisonDummy1 = A.Dummy<NoInstancesAreEqual>();

            var referenceDummy2 = Some.Dummies<double>();
            var comparisonDummy2 = Some.Dummies<double>();

            var referenceDummy3 = Some.ReadOnlyDummies<double>();
            var comparisonDummy3 = Some.ReadOnlyDummies<double>();

            // Act
            // ReSharper disable RedundantArgumentName
            var result1a = referenceDummy1.ThatIsNot(comparisonDummy1);
            var result1b = referenceDummy1.ThatIsNot(comparisonDummy1, maxAttempts: 101);
            var result1c = referenceDummy1.ThatIsNot(comparisonDummy1, maxAttempts: 0);
            var result1d = referenceDummy1.ThatIsNot(comparisonDummy1, maxAttempts: -1);
            var result1e = referenceDummy1.ThatIsNot(comparisonDummy1, maxAttempts: -1000);

            var result2a = referenceDummy2.ThatIsNot(comparisonDummy2);
            var result2b = referenceDummy2.ThatIsNot(comparisonDummy2, maxAttempts: 101);
            var result2c = referenceDummy2.ThatIsNot(comparisonDummy2, maxAttempts: 0);
            var result2d = referenceDummy2.ThatIsNot(comparisonDummy2, maxAttempts: -1);
            var result2e = referenceDummy2.ThatIsNot(comparisonDummy2, maxAttempts: -1000);

            var result3a = referenceDummy3.ThatIsNot(comparisonDummy3);
            var result3b = referenceDummy3.ThatIsNot(comparisonDummy3, maxAttempts: 101);
            var result3c = referenceDummy3.ThatIsNot(comparisonDummy3, maxAttempts: 0);
            var result3d = referenceDummy3.ThatIsNot(comparisonDummy3, maxAttempts: -1);
            var result3e = referenceDummy3.ThatIsNot(comparisonDummy3, maxAttempts: -1000);
            // ReSharper restore RedundantArgumentName

            // Assert
            result1a.Should().BeSameAs(referenceDummy1);
            result1b.Should().BeSameAs(referenceDummy1);
            result1c.Should().BeSameAs(referenceDummy1);
            result1d.Should().BeSameAs(referenceDummy1);
            result1e.Should().BeSameAs(referenceDummy1);

            result2a.Should().BeSameAs(referenceDummy2);
            result2b.Should().BeSameAs(referenceDummy2);
            result2c.Should().BeSameAs(referenceDummy2);
            result2d.Should().BeSameAs(referenceDummy2);
            result2e.Should().BeSameAs(referenceDummy2);

            result3a.Should().BeSameAs(referenceDummy3);
            result3b.Should().BeSameAs(referenceDummy3);
            result3c.Should().BeSameAs(referenceDummy3);
            result3d.Should().BeSameAs(referenceDummy3);
            result3e.Should().BeSameAs(referenceDummy3);
        }

        [Fact]
        public static void ThatIsNot___Should_return_new_dummy___When_referenceDummy_reference_equals_comparisonDummy_regardless_of_maxAttempts()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<NoInstancesAreEqual>();
            var referenceDummy2 = Some.Dummies<double>();
            var referenceDummy3 = Some.ReadOnlyDummies<double>();

            // Act
            // ReSharper disable RedundantArgumentName
            var result1a = referenceDummy1.ThatIsNot(referenceDummy1);
            var result1b = referenceDummy1.ThatIsNot(referenceDummy1, maxAttempts: 101);
            var result1c = referenceDummy1.ThatIsNot(referenceDummy1, maxAttempts: 0);
            var result1d = referenceDummy1.ThatIsNot(referenceDummy1, maxAttempts: -1);
            var result1e = referenceDummy1.ThatIsNot(referenceDummy1, maxAttempts: -1000);

            var result2a = referenceDummy2.ThatIsNot(referenceDummy2);
            var result2b = referenceDummy2.ThatIsNot(referenceDummy2, maxAttempts: 101);
            var result2c = referenceDummy2.ThatIsNot(referenceDummy2, maxAttempts: 0);
            var result2d = referenceDummy2.ThatIsNot(referenceDummy2, maxAttempts: -1);
            var result2e = referenceDummy2.ThatIsNot(referenceDummy2, maxAttempts: -1000);

            var result3a = referenceDummy3.ThatIsNot(referenceDummy3);
            var result3b = referenceDummy3.ThatIsNot(referenceDummy3, maxAttempts: 101);
            var result3c = referenceDummy3.ThatIsNot(referenceDummy3, maxAttempts: 0);
            var result3d = referenceDummy3.ThatIsNot(referenceDummy3, maxAttempts: -1);
            var result3e = referenceDummy3.ThatIsNot(referenceDummy3, maxAttempts: -1000);
            // ReSharper restore RedundantArgumentName

            // Assert
            result1a.Should().NotBeNull().And.NotBeSameAs(referenceDummy1);
            result1b.Should().NotBeNull().And.NotBeSameAs(referenceDummy1);
            result1c.Should().NotBeNull().And.NotBeSameAs(referenceDummy1);
            result1d.Should().NotBeNull().And.NotBeSameAs(referenceDummy1);
            result1e.Should().NotBeNull().And.NotBeSameAs(referenceDummy1);

            result2a.Should().NotBeNull().And.NotBeSameAs(referenceDummy2);
            result2b.Should().NotBeNull().And.NotBeSameAs(referenceDummy2);
            result2c.Should().NotBeNull().And.NotBeSameAs(referenceDummy2);
            result2d.Should().NotBeNull().And.NotBeSameAs(referenceDummy2);
            result2e.Should().NotBeNull().And.NotBeSameAs(referenceDummy2);

            result3a.Should().NotBeNull().And.NotBeSameAs(referenceDummy3);
            result3b.Should().NotBeNull().And.NotBeSameAs(referenceDummy3);
            result3c.Should().NotBeNull().And.NotBeSameAs(referenceDummy3);
            result3d.Should().NotBeNull().And.NotBeSameAs(referenceDummy3);
            result3e.Should().NotBeNull().And.NotBeSameAs(referenceDummy3);
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
            var referenceDummy1 = A.Dummy<NoInstancesAreEqual>();
            var referenceDummy2 = Some.Dummies<double>();
            var referenceDummy3 = Some.ReadOnlyDummies<double>();

            // Act
            // ReSharper disable RedundantArgumentName
            // ReSharper disable ExpressionIsAlwaysNull
            var result1a = referenceDummy1.ThatIsNot(null);
            var result1b = referenceDummy1.ThatIsNot(null, maxAttempts: 101);
            var result1c = referenceDummy1.ThatIsNot(null, maxAttempts: 0);
            var result1d = referenceDummy1.ThatIsNot(null, maxAttempts: -1);
            var result1e = referenceDummy1.ThatIsNot(null, maxAttempts: -1000);

            var result2a = referenceDummy2.ThatIsNot(null);
            var result2b = referenceDummy2.ThatIsNot(null, maxAttempts: 101);
            var result2c = referenceDummy2.ThatIsNot(null, maxAttempts: 0);
            var result2d = referenceDummy2.ThatIsNot(null, maxAttempts: -1);
            var result2e = referenceDummy2.ThatIsNot(null, maxAttempts: -1000);

            var result3a = referenceDummy3.ThatIsNot(null);
            var result3b = referenceDummy3.ThatIsNot(null, maxAttempts: 101);
            var result3c = referenceDummy3.ThatIsNot(null, maxAttempts: 0);
            var result3d = referenceDummy3.ThatIsNot(null, maxAttempts: -1);
            var result3e = referenceDummy3.ThatIsNot(null, maxAttempts: -1000);
            // ReSharper restore ExpressionIsAlwaysNull
            // ReSharper restore RedundantArgumentName

            // Assert
            result1a.Should().BeSameAs(referenceDummy1);
            result1b.Should().BeSameAs(referenceDummy1);
            result1c.Should().BeSameAs(referenceDummy1);
            result1d.Should().BeSameAs(referenceDummy1);
            result1e.Should().BeSameAs(referenceDummy1);

            result2a.Should().BeSameAs(referenceDummy2);
            result2b.Should().BeSameAs(referenceDummy2);
            result2c.Should().BeSameAs(referenceDummy2);
            result2d.Should().BeSameAs(referenceDummy2);
            result2e.Should().BeSameAs(referenceDummy2);

            result3a.Should().BeSameAs(referenceDummy3);
            result3b.Should().BeSameAs(referenceDummy3);
            result3c.Should().BeSameAs(referenceDummy3);
            result3d.Should().BeSameAs(referenceDummy3);
            result3e.Should().BeSameAs(referenceDummy3);
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

        [Fact]
        public static void ThatIsNot___Should_return_new_IList_with_specified_numberOfElements_and_specified_createWith___When_referenceDummy_was_created_by_a_called_to_SomeDummies_and_reference_equals_comparisonDummy()
        {
            // Arrange
            var expectedSize = ThreadSafeRandom.Next(1, 10);
            var referenceDummy = Some.Dummies<string>(expectedSize, CreateWith.OneOrMoreNulls);

            // Act
            // ReSharper disable RedundantArgumentName
            var result = referenceDummy.ThatIsNot(referenceDummy, maxAttempts: 2);
            // ReSharper restore RedundantArgumentName

            // Assert
            result.Should().BeAssignableTo<IList<string>>();
            result.Should().NotBeNull().And.NotBeSameAs(referenceDummy);
            result.Should().HaveCount(expectedSize);
            result.Should().Contain((string)null);
        }

        [Fact]
        public static void ThatIsNot___Should_return_new_IReadOnlyList_with_specified_numberOfElements_and_specified_createWith___When_referenceDummy_was_created_by_a_call_to_SomeReadOnlyDummies_and_reference_equals_comparisonDummy()
        {
            // Arrange
            var expectedSize = ThreadSafeRandom.Next(1, 10);
            var referenceDummy = Some.ReadOnlyDummies<string>(expectedSize, CreateWith.OneOrMoreNulls);

            // Act
            // ReSharper disable RedundantArgumentName
            var result = referenceDummy.ThatIsNot(referenceDummy, maxAttempts: 2);
            // ReSharper restore RedundantArgumentName

            // Assert
            result.Should().BeAssignableTo<IReadOnlyList<string>>();
            result.Should().NotBeNull().And.NotBeSameAs(referenceDummy);
            result.Should().HaveCount(expectedSize);
            result.Should().Contain((string)null);
        }

        private static bool ConditionThatCannnotBeMet(string input)
        {
            return false;
        }

        private static bool ConditionThatCannnotBeMet(IList<string> input)
        {
            return false;
        }

        private static bool ConditionThatCannnotBeMet(IReadOnlyList<string> input)
        {
            return false;
        }

        private static bool ConditionThatsAlwaysMet(string input)
        {
            return true;
        }

        private static bool ConditionThatsAlwaysMet(IList<string> input)
        {
            return true;
        }

        private static bool ConditionThatsAlwaysMet(IReadOnlyList<string> input)
        {
            return true;
        }

        // ReSharper restore InconsistentNaming
    }
}
