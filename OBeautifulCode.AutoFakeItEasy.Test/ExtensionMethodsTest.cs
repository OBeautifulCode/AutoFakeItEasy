// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtensionMethodsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
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

    using OBeautifulCode.Math.Recipes;

    using Xunit;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "there are a lot of methods to test")]
    public static class ExtensionMethodsTest
    {
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
            var ex1 = Record.Exception(() => A.Dummy<string>().ThatIs(ConditionThatCannnotBeMet, maxAttempts: 101));
            var ex2 = Record.Exception(() => Some.Dummies<string>().ThatIs(ConditionThatCannnotBeMet, maxAttempts: 101));
            var ex3 = Record.Exception(() => Some.ReadOnlyDummies<string>().ThatIs(ConditionThatCannnotBeMet, maxAttempts: 101));

            // Assert
            ex1.Should().BeOfType<InvalidOperationException>();
            ex2.Should().BeOfType<InvalidOperationException>();
            ex3.Should().BeOfType<InvalidOperationException>();
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
            var result1a = referenceDummy1.ThatIs(ConditionThatsAlwaysMet);
            var result1b = referenceDummy1.ThatIs(ConditionThatsAlwaysMet, maxAttempts: 101);
            var result1c = referenceDummy1.ThatIs(ConditionThatsAlwaysMet, maxAttempts: 0);
            var result1d = referenceDummy1.ThatIs(ConditionThatsAlwaysMet, maxAttempts: -1);

            var result2a = referenceDummy2.ThatIs(ConditionThatsAlwaysMet);
            var result2b = referenceDummy2.ThatIs(ConditionThatsAlwaysMet, maxAttempts: 101);
            var result2c = referenceDummy2.ThatIs(ConditionThatsAlwaysMet, maxAttempts: 0);
            var result2d = referenceDummy2.ThatIs(ConditionThatsAlwaysMet, maxAttempts: -1);

            var result3a = referenceDummy3.ThatIs(ConditionThatsAlwaysMet);
            var result3b = referenceDummy3.ThatIs(ConditionThatsAlwaysMet, maxAttempts: 101);
            var result3c = referenceDummy3.ThatIs(ConditionThatsAlwaysMet, maxAttempts: 0);
            var result3d = referenceDummy3.ThatIs(ConditionThatsAlwaysMet, maxAttempts: -1);

            // Assert
            result1a.Should().Be(referenceDummy1);
            result1b.Should().Be(referenceDummy1);
            result1c.Should().Be(referenceDummy1);
            result1d.Should().Be(referenceDummy1);

            result2a.Should().Equal(referenceDummy2);
            result2b.Should().Equal(referenceDummy2);
            result2c.Should().Equal(referenceDummy2);
            result2d.Should().Equal(referenceDummy2);

            result3a.Should().Equal(referenceDummy3);
            result3b.Should().Equal(referenceDummy3);
            result3c.Should().Equal(referenceDummy3);
            result3d.Should().Equal(referenceDummy3);
        }

        [Fact]
        public static void ThatIs___Should_return_new_dummy_that_meets_condition___When_referenceDummy_is_null_regardless_of_maxAttempts()
        {
            // Arrange
            const string ExpectedCharacter = "a";
            string referenceDummy = null;
            Func<string, bool> condition = _ => (_ != null) && _.Contains(ExpectedCharacter);

            // Act
            var result1 = referenceDummy.ThatIs(condition);
            var result2 = referenceDummy.ThatIs(condition, maxAttempts: 101);
            var result3 = referenceDummy.ThatIs(condition, maxAttempts: 0);
            var result4 = referenceDummy.ThatIs(condition, maxAttempts: -1);

            // Assert
            result1.Should().Contain(ExpectedCharacter);
            result2.Should().Contain(ExpectedCharacter);
            result3.Should().Contain(ExpectedCharacter);
            result4.Should().Contain(ExpectedCharacter);
        }

        [Fact]
        public static void ThatIs___Should_return_dummy_that_meets_condition___When_referenceDummy_is_generated_via_ADummy_and_does_not_meet_condition_but_condition_can_be_satisfied_after_one_or_more_attempts()
        {
            // Arrange
            const string ExpectedCharacter = "a";
            string referenceDummy = A.Dummy<string>().Replace(ExpectedCharacter, string.Empty);
            Func<string, bool> condition = _ => _.Contains(ExpectedCharacter);

            // Act
            var result1 = referenceDummy.ThatIs(condition);
            var result2 = referenceDummy.ThatIs(condition, maxAttempts: 101);
            var result3 = referenceDummy.ThatIs(condition, maxAttempts: 0);
            var result4 = referenceDummy.ThatIs(condition, maxAttempts: -1);

            // Assert
            result1.Should().NotBe(referenceDummy);
            result2.Should().NotBe(referenceDummy);
            result3.Should().NotBe(referenceDummy);
            result4.Should().NotBe(referenceDummy);

            result1.Should().Contain(ExpectedCharacter);
            result2.Should().Contain(ExpectedCharacter);
            result3.Should().Contain(ExpectedCharacter);
            result4.Should().Contain(ExpectedCharacter);
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
            var result1 = referenceDummy.ThatIs(condition);
            var result2 = referenceDummy.ThatIs(condition, maxAttempts: 101);
            var result3 = referenceDummy.ThatIs(condition, maxAttempts: 0);
            var result4 = referenceDummy.ThatIs(condition, maxAttempts: -1);

            // Assert
            result1.Count.Should().BeGreaterOrEqualTo(MinCount);
            result2.Count.Should().BeGreaterOrEqualTo(MinCount);
            result3.Count.Should().BeGreaterOrEqualTo(MinCount);
            result4.Count.Should().BeGreaterOrEqualTo(MinCount);
        }

        [Fact]
        public static void ThatIs___Should_return_dummy_that_meets_condition___When_referenceDummy_is_generated_via_SomeReadOnlyDummies_and_does_not_meet_condition_but_condition_can_be_satisfied_after_one_or_more_attempts()
        {
            // Arrange
            const int MinCount = 5;
            var referenceDummy = new SomeReadOnlyDummiesList<double>(new List<double>(), -1, CreateWith.NoNulls);
            Func<IReadOnlyList<double>, bool> condition = _ => _.Count >= MinCount;

            // Act
            var result1 = referenceDummy.ThatIs(condition);
            var result2 = referenceDummy.ThatIs(condition, maxAttempts: 101);
            var result3 = referenceDummy.ThatIs(condition, maxAttempts: 0);
            var result4 = referenceDummy.ThatIs(condition, maxAttempts: -1);

            // Assert
            result1.Count.Should().BeGreaterOrEqualTo(MinCount);
            result2.Count.Should().BeGreaterOrEqualTo(MinCount);
            result3.Count.Should().BeGreaterOrEqualTo(MinCount);
            result4.Count.Should().BeGreaterOrEqualTo(MinCount);
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
            var result1 = referenceDummy1.ThatIs(trueOnFirstRetryCondition, maxAttempts: 2);
            var result2 = referenceDummy2.ThatIs(trueAfterSeveralRetriesCondition, maxAttempts: 0);

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
            var result1 = referenceDummy1.ThatIs(trueOnFirstRetryCondition, maxAttempts: 2);
            var result2 = referenceDummy2.ThatIs(trueAfterSeveralRetriesCondition, maxAttempts: 0);

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
            var ex1 = Record.Exception(() => A.Dummy<string>().Whose(ConditionThatCannnotBeMet, maxAttempts: 101));
            var ex2 = Record.Exception(() => Some.Dummies<string>().Whose(ConditionThatCannnotBeMet, maxAttempts: 101));
            var ex3 = Record.Exception(() => Some.ReadOnlyDummies<string>().Whose(ConditionThatCannnotBeMet, maxAttempts: 101));

            // Assert
            ex1.Should().BeOfType<InvalidOperationException>();
            ex2.Should().BeOfType<InvalidOperationException>();
            ex3.Should().BeOfType<InvalidOperationException>();
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
            var result1a = referenceDummy1.Whose(ConditionThatsAlwaysMet);
            var result1b = referenceDummy1.Whose(ConditionThatsAlwaysMet, maxAttempts: 101);
            var result1c = referenceDummy1.Whose(ConditionThatsAlwaysMet, maxAttempts: 0);
            var result1d = referenceDummy1.Whose(ConditionThatsAlwaysMet, maxAttempts: -1);

            var result2a = referenceDummy2.Whose(ConditionThatsAlwaysMet);
            var result2b = referenceDummy2.Whose(ConditionThatsAlwaysMet, maxAttempts: 101);
            var result2c = referenceDummy2.Whose(ConditionThatsAlwaysMet, maxAttempts: 0);
            var result2d = referenceDummy2.Whose(ConditionThatsAlwaysMet, maxAttempts: -1);

            var result3a = referenceDummy3.Whose(ConditionThatsAlwaysMet);
            var result3b = referenceDummy3.Whose(ConditionThatsAlwaysMet, maxAttempts: 101);
            var result3c = referenceDummy3.Whose(ConditionThatsAlwaysMet, maxAttempts: 0);
            var result3d = referenceDummy3.Whose(ConditionThatsAlwaysMet, maxAttempts: -1);

            // Assert
            result1a.Should().Be(referenceDummy1);
            result1b.Should().Be(referenceDummy1);
            result1c.Should().Be(referenceDummy1);
            result1d.Should().Be(referenceDummy1);

            result2a.Should().Equal(referenceDummy2);
            result2b.Should().Equal(referenceDummy2);
            result2c.Should().Equal(referenceDummy2);
            result2d.Should().Equal(referenceDummy2);

            result3a.Should().Equal(referenceDummy3);
            result3b.Should().Equal(referenceDummy3);
            result3c.Should().Equal(referenceDummy3);
            result3d.Should().Equal(referenceDummy3);
        }

        [Fact]
        public static void Whose___Should_return_new_dummy_that_meets_condition___When_referenceDummy_is_null_regardless_of_maxAttempts()
        {
            // Arrange
            const string ExpectedCharacter = "a";
            string referenceDummy = null;
            Func<string, bool> condition = _ => (_ != null) && _.Contains(ExpectedCharacter);

            // Act
            var result1 = referenceDummy.Whose(condition);
            var result2 = referenceDummy.Whose(condition, maxAttempts: 101);
            var result3 = referenceDummy.Whose(condition, maxAttempts: 0);
            var result4 = referenceDummy.Whose(condition, maxAttempts: -1);

            // Assert
            result1.Should().Contain(ExpectedCharacter);
            result2.Should().Contain(ExpectedCharacter);
            result3.Should().Contain(ExpectedCharacter);
            result4.Should().Contain(ExpectedCharacter);
        }

        [Fact]
        public static void Whose___Should_return_dummy_that_meets_condition___When_referenceDummy_is_generated_via_ADummy_and_does_not_meet_condition_but_condition_can_be_satisfied_after_one_or_more_attempts()
        {
            // Arrange
            const string ExpectedCharacter = "a";
            string referenceDummy = A.Dummy<string>().Replace(ExpectedCharacter, string.Empty);
            Func<string, bool> condition = _ => _.Contains(ExpectedCharacter);

            // Act
            var result1 = referenceDummy.Whose(condition);
            var result2 = referenceDummy.Whose(condition, maxAttempts: 101);
            var result3 = referenceDummy.Whose(condition, maxAttempts: 0);
            var result4 = referenceDummy.Whose(condition, maxAttempts: -1);

            // Assert
            result1.Should().NotBe(referenceDummy);
            result2.Should().NotBe(referenceDummy);
            result3.Should().NotBe(referenceDummy);
            result4.Should().NotBe(referenceDummy);

            result1.Should().Contain(ExpectedCharacter);
            result2.Should().Contain(ExpectedCharacter);
            result3.Should().Contain(ExpectedCharacter);
            result4.Should().Contain(ExpectedCharacter);
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
            var result1 = referenceDummy.Whose(condition);
            var result2 = referenceDummy.Whose(condition, maxAttempts: 101);
            var result3 = referenceDummy.Whose(condition, maxAttempts: 0);
            var result4 = referenceDummy.Whose(condition, maxAttempts: -1);

            // Assert
            result1.Count.Should().BeGreaterOrEqualTo(MinCount);
            result2.Count.Should().BeGreaterOrEqualTo(MinCount);
            result3.Count.Should().BeGreaterOrEqualTo(MinCount);
            result4.Count.Should().BeGreaterOrEqualTo(MinCount);
        }

        [Fact]
        public static void Whose___Should_return_dummy_that_meets_condition___When_referenceDummy_is_generated_via_SomeReadOnlyDummies_and_does_not_meet_condition_but_condition_can_be_satisfied_after_one_or_more_attempts()
        {
            // Arrange
            const int MinCount = 5;
            var referenceDummy = new SomeReadOnlyDummiesList<double>(new List<double>(), -1, CreateWith.NoNulls);
            Func<IReadOnlyList<double>, bool> condition = _ => _.Count >= MinCount;

            // Act
            var result1 = referenceDummy.Whose(condition);
            var result2 = referenceDummy.Whose(condition, maxAttempts: 101);
            var result3 = referenceDummy.Whose(condition, maxAttempts: 0);
            var result4 = referenceDummy.Whose(condition, maxAttempts: -1);

            // Assert
            result1.Count.Should().BeGreaterOrEqualTo(MinCount);
            result2.Count.Should().BeGreaterOrEqualTo(MinCount);
            result3.Count.Should().BeGreaterOrEqualTo(MinCount);
            result4.Count.Should().BeGreaterOrEqualTo(MinCount);
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
            var result1 = referenceDummy1.Whose(trueOnFirstRetryCondition, maxAttempts: 2);
            var result2 = referenceDummy2.Whose(trueAfterSeveralRetriesCondition, maxAttempts: 0);

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
            var result1 = referenceDummy1.Whose(trueOnFirstRetryCondition, maxAttempts: 2);
            var result2 = referenceDummy2.Whose(trueAfterSeveralRetriesCondition, maxAttempts: 0);

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
            var ex = Record.Exception(() => A.Dummy<AllInstancesEqual>().ThatIsNot(comparisonDummy, maxAttempts: 101));

            // Assert
            ex.Should().BeOfType<InvalidOperationException>();
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
            var result1a = referenceDummy1.ThatIsNot(comparisonDummy1);
            var result1b = referenceDummy1.ThatIsNot(comparisonDummy1, maxAttempts: 101);
            var result1c = referenceDummy1.ThatIsNot(comparisonDummy1, maxAttempts: 0);
            var result1d = referenceDummy1.ThatIsNot(comparisonDummy1, maxAttempts: -1);

            var result2a = referenceDummy2.ThatIsNot(comparisonDummy2);
            var result2b = referenceDummy2.ThatIsNot(comparisonDummy2, maxAttempts: 101);
            var result2c = referenceDummy2.ThatIsNot(comparisonDummy2, maxAttempts: 0);
            var result2d = referenceDummy2.ThatIsNot(comparisonDummy2, maxAttempts: -1);

            var result3a = referenceDummy3.ThatIsNot(comparisonDummy3);
            var result3b = referenceDummy3.ThatIsNot(comparisonDummy3, maxAttempts: 101);
            var result3c = referenceDummy3.ThatIsNot(comparisonDummy3, maxAttempts: 0);
            var result3d = referenceDummy3.ThatIsNot(comparisonDummy3, maxAttempts: -1);

            // Assert
            result1a.Should().BeSameAs(referenceDummy1);
            result1b.Should().BeSameAs(referenceDummy1);
            result1c.Should().BeSameAs(referenceDummy1);
            result1d.Should().BeSameAs(referenceDummy1);

            result2a.Should().BeSameAs(referenceDummy2);
            result2b.Should().BeSameAs(referenceDummy2);
            result2c.Should().BeSameAs(referenceDummy2);
            result2d.Should().BeSameAs(referenceDummy2);

            result3a.Should().BeSameAs(referenceDummy3);
            result3b.Should().BeSameAs(referenceDummy3);
            result3c.Should().BeSameAs(referenceDummy3);
            result3d.Should().BeSameAs(referenceDummy3);
        }

        [Fact]
        public static void ThatIsNot___Should_return_new_dummy___When_referenceDummy_reference_equals_comparisonDummy_regardless_of_maxAttempts()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<NoInstancesAreEqual>();
            var referenceDummy2 = Some.Dummies<double>();
            var referenceDummy3 = Some.ReadOnlyDummies<double>();

            // Act
            var result1a = referenceDummy1.ThatIsNot(referenceDummy1);
            var result1b = referenceDummy1.ThatIsNot(referenceDummy1, maxAttempts: 101);
            var result1c = referenceDummy1.ThatIsNot(referenceDummy1, maxAttempts: 0);
            var result1d = referenceDummy1.ThatIsNot(referenceDummy1, maxAttempts: -1);

            var result2a = referenceDummy2.ThatIsNot(referenceDummy2);
            var result2b = referenceDummy2.ThatIsNot(referenceDummy2, maxAttempts: 101);
            var result2c = referenceDummy2.ThatIsNot(referenceDummy2, maxAttempts: 0);
            var result2d = referenceDummy2.ThatIsNot(referenceDummy2, maxAttempts: -1);

            var result3a = referenceDummy3.ThatIsNot(referenceDummy3);
            var result3b = referenceDummy3.ThatIsNot(referenceDummy3, maxAttempts: 101);
            var result3c = referenceDummy3.ThatIsNot(referenceDummy3, maxAttempts: 0);
            var result3d = referenceDummy3.ThatIsNot(referenceDummy3, maxAttempts: -1);

            // Assert
            result1a.Should().NotBeNull().And.NotBeSameAs(referenceDummy1);
            result1b.Should().NotBeNull().And.NotBeSameAs(referenceDummy1);
            result1c.Should().NotBeNull().And.NotBeSameAs(referenceDummy1);
            result1d.Should().NotBeNull().And.NotBeSameAs(referenceDummy1);

            result2a.Should().NotBeNull().And.NotBeSameAs(referenceDummy2);
            result2b.Should().NotBeNull().And.NotBeSameAs(referenceDummy2);
            result2c.Should().NotBeNull().And.NotBeSameAs(referenceDummy2);
            result2d.Should().NotBeNull().And.NotBeSameAs(referenceDummy2);

            result3a.Should().NotBeNull().And.NotBeSameAs(referenceDummy3);
            result3b.Should().NotBeNull().And.NotBeSameAs(referenceDummy3);
            result3c.Should().NotBeNull().And.NotBeSameAs(referenceDummy3);
            result3d.Should().NotBeNull().And.NotBeSameAs(referenceDummy3);
        }

        [Fact]
        public static void ThatIsNot___Should_return_new_dummy___When_referenceDummy_value_equals_comparisonDummy_regardless_of_maxAttempts()
        {
            // Arrange
            var now = DateTime.Now;
            var referenceDummy = now.ToString(CultureInfo.CurrentCulture);
            var comparisonDummy = now.ToString(CultureInfo.CurrentCulture);

            // Act
            var result1 = referenceDummy.ThatIsNot(comparisonDummy);
            var result2 = referenceDummy.ThatIsNot(comparisonDummy, maxAttempts: 101);
            var result3 = referenceDummy.ThatIsNot(comparisonDummy, maxAttempts: 0);
            var result4 = referenceDummy.ThatIsNot(comparisonDummy, maxAttempts: -1);

            // Assert
            referenceDummy.Should().NotBeSameAs(comparisonDummy);
            result1.Should().NotBe(referenceDummy);
            result2.Should().NotBe(referenceDummy);
            result3.Should().NotBe(referenceDummy);
            result4.Should().NotBe(referenceDummy);
        }

        [Fact]
        public static void ThatIsNot___Should_return_referenceDummy___When_referenceDummy_is_not_null_and_comparisonDummy_is_null_regardless_of_maxAttempts()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<NoInstancesAreEqual>();
            var referenceDummy2 = Some.Dummies<double>();
            var referenceDummy3 = Some.ReadOnlyDummies<double>();

            // Act
            var result1a = referenceDummy1.ThatIsNot(null);
            var result1b = referenceDummy1.ThatIsNot(null, maxAttempts: 101);
            var result1c = referenceDummy1.ThatIsNot(null, maxAttempts: 0);
            var result1d = referenceDummy1.ThatIsNot(null, maxAttempts: -1);

            var result2a = referenceDummy2.ThatIsNot(null);
            var result2b = referenceDummy2.ThatIsNot(null, maxAttempts: 101);
            var result2c = referenceDummy2.ThatIsNot(null, maxAttempts: 0);
            var result2d = referenceDummy2.ThatIsNot(null, maxAttempts: -1);

            var result3a = referenceDummy3.ThatIsNot(null);
            var result3b = referenceDummy3.ThatIsNot(null, maxAttempts: 101);
            var result3c = referenceDummy3.ThatIsNot(null, maxAttempts: 0);
            var result3d = referenceDummy3.ThatIsNot(null, maxAttempts: -1);

            // Assert
            result1a.Should().BeSameAs(referenceDummy1);
            result1b.Should().BeSameAs(referenceDummy1);
            result1c.Should().BeSameAs(referenceDummy1);
            result1d.Should().BeSameAs(referenceDummy1);

            result2a.Should().BeSameAs(referenceDummy2);
            result2b.Should().BeSameAs(referenceDummy2);
            result2c.Should().BeSameAs(referenceDummy2);
            result2d.Should().BeSameAs(referenceDummy2);

            result3a.Should().BeSameAs(referenceDummy3);
            result3b.Should().BeSameAs(referenceDummy3);
            result3c.Should().BeSameAs(referenceDummy3);
            result3d.Should().BeSameAs(referenceDummy3);
        }

        [Fact]
        public static void ThatIsNot___Should_return_null___When_referenceDummy_is_null_and_comparisonDummy_is_not_null_regardless_of_maxAttempts()
        {
            // Arrange
            NoInstancesAreEqual referenceDummy = null;
            var comparisonDummy = A.Dummy<NoInstancesAreEqual>();

            // Act
            var result1 = referenceDummy.ThatIsNot(comparisonDummy);
            var result2 = referenceDummy.ThatIsNot(comparisonDummy, maxAttempts: 101);
            var result3 = referenceDummy.ThatIsNot(comparisonDummy, maxAttempts: 0);
            var result4 = referenceDummy.ThatIsNot(comparisonDummy, maxAttempts: -1);

            // Assert
            result1.Should().BeNull();
            result2.Should().BeNull();
            result3.Should().BeNull();
            result4.Should().BeNull();
        }

        [Fact]
        public static void ThatIsNot___Should_return_new_IList_with_specified_numberOfElements_and_specified_createWith___When_referenceDummy_was_created_by_a_called_to_SomeDummies_and_reference_equals_comparisonDummy()
        {
            // Arrange
            var expectedSize = ThreadSafeRandom.Next(1, 10);
            var referenceDummy = Some.Dummies<string>(expectedSize, CreateWith.OneOrMoreNulls);

            // Act
            var result = referenceDummy.ThatIsNot(referenceDummy, maxAttempts: 2);

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
            var result = referenceDummy.ThatIsNot(referenceDummy, maxAttempts: 2);

            // Assert
            result.Should().BeAssignableTo<IReadOnlyList<string>>();
            result.Should().NotBeNull().And.NotBeSameAs(referenceDummy);
            result.Should().HaveCount(expectedSize);
            result.Should().Contain((string)null);
        }

        [Fact]
        public static void ThatIsNotIn_without_IEqualityComparer___Should_throw_ArgumentNullException___When_comparisonDummies_is_null_regardless_of_maxAttempts()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<NoInstancesAreEqual>();
            var referenceDummy2 = Some.Dummies<double>();
            var referenceDummy3 = Some.ReadOnlyDummies<double>();

            // Act
            var ex1a = Record.Exception(() => referenceDummy1.ThatIsNotIn(null));
            var ex1b = Record.Exception(() => referenceDummy1.ThatIsNotIn(null, maxAttempts: 101));
            var ex1c = Record.Exception(() => referenceDummy1.ThatIsNotIn(null, maxAttempts: 0));
            var ex1d = Record.Exception(() => referenceDummy1.ThatIsNotIn(null, maxAttempts: -1));

            var ex2a = Record.Exception(() => referenceDummy2.ThatIsNotIn(null));
            var ex2b = Record.Exception(() => referenceDummy2.ThatIsNotIn(null, maxAttempts: 101));
            var ex2c = Record.Exception(() => referenceDummy2.ThatIsNotIn(null, maxAttempts: 0));
            var ex2d = Record.Exception(() => referenceDummy2.ThatIsNotIn(null, maxAttempts: -1));

            var ex3a = Record.Exception(() => referenceDummy3.ThatIsNotIn(null));
            var ex3b = Record.Exception(() => referenceDummy3.ThatIsNotIn(null, maxAttempts: 101));
            var ex3c = Record.Exception(() => referenceDummy3.ThatIsNotIn(null, maxAttempts: 0));
            var ex3d = Record.Exception(() => referenceDummy3.ThatIsNotIn(null, maxAttempts: -1));

            // Assert
            ex1a.Should().BeOfType<ArgumentNullException>();
            ex1b.Should().BeOfType<ArgumentNullException>();
            ex1c.Should().BeOfType<ArgumentNullException>();
            ex1d.Should().BeOfType<ArgumentNullException>();

            ex2a.Should().BeOfType<ArgumentNullException>();
            ex2b.Should().BeOfType<ArgumentNullException>();
            ex2c.Should().BeOfType<ArgumentNullException>();
            ex2d.Should().BeOfType<ArgumentNullException>();

            ex3a.Should().BeOfType<ArgumentNullException>();
            ex3b.Should().BeOfType<ArgumentNullException>();
            ex3c.Should().BeOfType<ArgumentNullException>();
            ex3d.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void ThatIsNotIn_without_IEqualityComparer___Should_throw_InvalidOperation___When_maxAttempts_is_1_and_referenceDummy_is_in_set_of_comparisonDummies()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<string>();
            var referenceDummy2 = Some.Dummies<string>();
            var referenceDummy3 = Some.ReadOnlyDummies<string>();

            // Act
            var ex1 = Record.Exception(() => referenceDummy1.ThatIsNotIn(new[] { referenceDummy1 }, maxAttempts: 1));
            var ex2 = Record.Exception(() => referenceDummy2.ThatIsNotIn(new[] { referenceDummy2 }, maxAttempts: 1));
            var ex3 = Record.Exception(() => referenceDummy3.ThatIsNotIn(new[] { referenceDummy3 }, maxAttempts: 1));

            // Assert
            ex1.Should().BeOfType<InvalidOperationException>();
            ex2.Should().BeOfType<InvalidOperationException>();
            ex3.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void ThatIsNotIn_without_IEqualityComparer___Should_throw_InvalidOperation___When_all_possible_dummies_are_in_the_set_of_comparisonDummies_and_maxAttempts_is_greater_than_1()
        {
            // Arrange
            var comparisonDummy = A.Dummy<AllInstancesEqual>();

            // Act
            var ex = Record.Exception(() => A.Dummy<AllInstancesEqual>().ThatIsNotIn(new[] { comparisonDummy }, maxAttempts: 101));

            // Assert
            ex.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void ThatIsNotIn_without_IEqualityComparer___Should_return_null___When_referenceDummy_is_null_and_comparisonDummies_is_empty_regardless_of_maxAttempts()
        {
            // Arrange
            NoInstancesAreEqual referenceDummy = null;
            var comparisonDummies = new NoInstancesAreEqual[] { };

            // Act
            var result1 = referenceDummy.ThatIsNotIn(comparisonDummies);
            var result2 = referenceDummy.ThatIsNotIn(comparisonDummies, maxAttempts: 101);
            var result3 = referenceDummy.ThatIsNotIn(comparisonDummies, maxAttempts: 0);
            var result4 = referenceDummy.ThatIsNotIn(comparisonDummies, maxAttempts: -1);

            // Assert
            result1.Should().BeNull();
            result2.Should().BeNull();
            result3.Should().BeNull();
            result4.Should().BeNull();
        }

        [Fact]
        public static void ThatIsNotIn_without_IEqualityComparer___Should_return_null___When_referenceDummy_is_null_and_comparisonDummies_does_not_contain_null_regardless_of_maxAttempts()
        {
            // Arrange
            NoInstancesAreEqual referenceDummy = null;
            var comparisonDummy = A.Dummy<NoInstancesAreEqual>();

            // Act
            var result1 = referenceDummy.ThatIsNotIn(new[] { comparisonDummy });
            var result2 = referenceDummy.ThatIsNotIn(new[] { comparisonDummy }, maxAttempts: 101);
            var result3 = referenceDummy.ThatIsNotIn(new[] { comparisonDummy }, maxAttempts: 0);
            var result4 = referenceDummy.ThatIsNotIn(new[] { comparisonDummy }, maxAttempts: -1);

            // Assert
            result1.Should().BeNull();
            result2.Should().BeNull();
            result3.Should().BeNull();
            result4.Should().BeNull();
        }

        [Fact]
        public static void ThatIsNotIn_without_IEqualityComparer___Should_return_referenceDummy___When_comparisonDummies_is_empty()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<NoInstancesAreEqual>();
            var comparisonDummies1 = new NoInstancesAreEqual[] { };

            var referenceDummy2 = Some.Dummies<double>();
            var comparisonDummies2 = new IList<double>[] { };

            var referenceDummy3 = Some.ReadOnlyDummies<double>();
            var comparisonDummies3 = new IReadOnlyList<double>[] { };

            // Act
            var result1a = referenceDummy1.ThatIsNotIn(comparisonDummies1);
            var result1b = referenceDummy1.ThatIsNotIn(comparisonDummies1, maxAttempts: 101);
            var result1c = referenceDummy1.ThatIsNotIn(comparisonDummies1, maxAttempts: 0);
            var result1d = referenceDummy1.ThatIsNotIn(comparisonDummies1, maxAttempts: -1);

            var result2a = referenceDummy2.ThatIsNotIn(comparisonDummies2);
            var result2b = referenceDummy2.ThatIsNotIn(comparisonDummies2, maxAttempts: 101);
            var result2c = referenceDummy2.ThatIsNotIn(comparisonDummies2, maxAttempts: 0);
            var result2d = referenceDummy2.ThatIsNotIn(comparisonDummies2, maxAttempts: -1);

            var result3a = referenceDummy3.ThatIsNotIn(comparisonDummies3);
            var result3b = referenceDummy3.ThatIsNotIn(comparisonDummies3, maxAttempts: 101);
            var result3c = referenceDummy3.ThatIsNotIn(comparisonDummies3, maxAttempts: 0);
            var result3d = referenceDummy3.ThatIsNotIn(comparisonDummies3, maxAttempts: -1);

            // Assert
            result1a.Should().BeSameAs(referenceDummy1);
            result1b.Should().BeSameAs(referenceDummy1);
            result1c.Should().BeSameAs(referenceDummy1);
            result1d.Should().BeSameAs(referenceDummy1);

            result2a.Should().BeSameAs(referenceDummy2);
            result2b.Should().BeSameAs(referenceDummy2);
            result2c.Should().BeSameAs(referenceDummy2);
            result2d.Should().BeSameAs(referenceDummy2);

            result3a.Should().BeSameAs(referenceDummy3);
            result3b.Should().BeSameAs(referenceDummy3);
            result3c.Should().BeSameAs(referenceDummy3);
            result3d.Should().BeSameAs(referenceDummy3);
        }

        [Fact]
        public static void ThatIsNotIn_without_IEqualityComparer___Should_return_referenceDummy___When_referenceDummy_is_not_in_set_of_comparisonDummies_regardless_of_maxAttempts()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<NoInstancesAreEqual>();
            var comparisonDummy1 = A.Dummy<NoInstancesAreEqual>();

            var referenceDummy2 = Some.Dummies<double>();
            var comparisonDummy2 = Some.Dummies<double>();

            var referenceDummy3 = Some.ReadOnlyDummies<double>();
            var comparisonDummy3 = Some.ReadOnlyDummies<double>();

            // Act
            var result1a = referenceDummy1.ThatIsNotIn(new[] { comparisonDummy1 });
            var result1b = referenceDummy1.ThatIsNotIn(new[] { comparisonDummy1 }, maxAttempts: 101);
            var result1c = referenceDummy1.ThatIsNotIn(new[] { comparisonDummy1 }, maxAttempts: 0);
            var result1d = referenceDummy1.ThatIsNotIn(new[] { comparisonDummy1 }, maxAttempts: -1);

            var result2a = referenceDummy2.ThatIsNotIn(new[] { comparisonDummy2 });
            var result2b = referenceDummy2.ThatIsNotIn(new[] { comparisonDummy2 }, maxAttempts: 101);
            var result2c = referenceDummy2.ThatIsNotIn(new[] { comparisonDummy2 }, maxAttempts: 0);
            var result2d = referenceDummy2.ThatIsNotIn(new[] { comparisonDummy2 }, maxAttempts: -1);

            var result3a = referenceDummy3.ThatIsNotIn(new[] { comparisonDummy3 });
            var result3b = referenceDummy3.ThatIsNotIn(new[] { comparisonDummy3 }, maxAttempts: 101);
            var result3c = referenceDummy3.ThatIsNotIn(new[] { comparisonDummy3 }, maxAttempts: 0);
            var result3d = referenceDummy3.ThatIsNotIn(new[] { comparisonDummy3 }, maxAttempts: -1);

            // Assert
            result1a.Should().BeSameAs(referenceDummy1);
            result1b.Should().BeSameAs(referenceDummy1);
            result1c.Should().BeSameAs(referenceDummy1);
            result1d.Should().BeSameAs(referenceDummy1);

            result2a.Should().BeSameAs(referenceDummy2);
            result2b.Should().BeSameAs(referenceDummy2);
            result2c.Should().BeSameAs(referenceDummy2);
            result2d.Should().BeSameAs(referenceDummy2);

            result3a.Should().BeSameAs(referenceDummy3);
            result3b.Should().BeSameAs(referenceDummy3);
            result3c.Should().BeSameAs(referenceDummy3);
            result3d.Should().BeSameAs(referenceDummy3);
        }

        [Fact]
        public static void ThatIsNotIn_without_IEqualityComparer___Should_return_referenceDummy___When_referenceDummy_is_not_null_and_comparisonDummies_contains_only_null_regardless_of_maxAttempts()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<NoInstancesAreEqual>();
            var referenceDummy2 = Some.Dummies<double>();
            var referenceDummy3 = Some.ReadOnlyDummies<double>();

            // Act
            var result1a = referenceDummy1.ThatIsNotIn(new NoInstancesAreEqual[] { null });
            var result1b = referenceDummy1.ThatIsNotIn(new NoInstancesAreEqual[] { null }, maxAttempts: 101);
            var result1c = referenceDummy1.ThatIsNotIn(new NoInstancesAreEqual[] { null }, maxAttempts: 0);
            var result1d = referenceDummy1.ThatIsNotIn(new NoInstancesAreEqual[] { null }, maxAttempts: -1);

            var result2a = referenceDummy2.ThatIsNotIn(new IList<double>[] { null });
            var result2b = referenceDummy2.ThatIsNotIn(new IList<double>[] { null }, maxAttempts: 101);
            var result2c = referenceDummy2.ThatIsNotIn(new IList<double>[] { null }, maxAttempts: 0);
            var result2d = referenceDummy2.ThatIsNotIn(new IList<double>[] { null }, maxAttempts: -1);

            var result3a = referenceDummy3.ThatIsNotIn(new IReadOnlyList<double>[] { null });
            var result3b = referenceDummy3.ThatIsNotIn(new IReadOnlyList<double>[] { null }, maxAttempts: 101);
            var result3c = referenceDummy3.ThatIsNotIn(new IReadOnlyList<double>[] { null }, maxAttempts: 0);
            var result3d = referenceDummy3.ThatIsNotIn(new IReadOnlyList<double>[] { null }, maxAttempts: -1);

            // Assert
            result1a.Should().BeSameAs(referenceDummy1);
            result1b.Should().BeSameAs(referenceDummy1);
            result1c.Should().BeSameAs(referenceDummy1);
            result1d.Should().BeSameAs(referenceDummy1);

            result2a.Should().BeSameAs(referenceDummy2);
            result2b.Should().BeSameAs(referenceDummy2);
            result2c.Should().BeSameAs(referenceDummy2);
            result2d.Should().BeSameAs(referenceDummy2);

            result3a.Should().BeSameAs(referenceDummy3);
            result3b.Should().BeSameAs(referenceDummy3);
            result3c.Should().BeSameAs(referenceDummy3);
            result3d.Should().BeSameAs(referenceDummy3);
        }

        [Fact]
        public static void ThatIsNotIn_without_IEqualityComparer___Should_return_new_dummy___When_referenceDummy_is_in_the_set_of_comparisonDummies_using_reference_equality_regardless_of_maxAttempts()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<NoInstancesAreEqual>();
            var referenceDummy2 = Some.Dummies<double>();
            var referenceDummy3 = Some.ReadOnlyDummies<double>();

            // Act
            var result1a = referenceDummy1.ThatIsNotIn(new[] { referenceDummy1 });
            var result1b = referenceDummy1.ThatIsNotIn(new[] { referenceDummy1 }, maxAttempts: 101);
            var result1c = referenceDummy1.ThatIsNotIn(new[] { referenceDummy1 }, maxAttempts: 0);
            var result1d = referenceDummy1.ThatIsNotIn(new[] { referenceDummy1 }, maxAttempts: -1);

            var result2a = referenceDummy2.ThatIsNotIn(new[] { referenceDummy2 });
            var result2b = referenceDummy2.ThatIsNotIn(new[] { referenceDummy2 }, maxAttempts: 101);
            var result2c = referenceDummy2.ThatIsNotIn(new[] { referenceDummy2 }, maxAttempts: 0);
            var result2d = referenceDummy2.ThatIsNotIn(new[] { referenceDummy2 }, maxAttempts: -1);

            var result3a = referenceDummy3.ThatIsNotIn(new[] { referenceDummy3 });
            var result3b = referenceDummy3.ThatIsNotIn(new[] { referenceDummy3 }, maxAttempts: 101);
            var result3c = referenceDummy3.ThatIsNotIn(new[] { referenceDummy3 }, maxAttempts: 0);
            var result3d = referenceDummy3.ThatIsNotIn(new[] { referenceDummy3 }, maxAttempts: -1);

            // Assert
            result1a.Should().NotBeNull().And.NotBeSameAs(referenceDummy1);
            result1b.Should().NotBeNull().And.NotBeSameAs(referenceDummy1);
            result1c.Should().NotBeNull().And.NotBeSameAs(referenceDummy1);
            result1d.Should().NotBeNull().And.NotBeSameAs(referenceDummy1);

            result2a.Should().NotBeNull().And.NotBeSameAs(referenceDummy2);
            result2b.Should().NotBeNull().And.NotBeSameAs(referenceDummy2);
            result2c.Should().NotBeNull().And.NotBeSameAs(referenceDummy2);
            result2d.Should().NotBeNull().And.NotBeSameAs(referenceDummy2);

            result3a.Should().NotBeNull().And.NotBeSameAs(referenceDummy3);
            result3b.Should().NotBeNull().And.NotBeSameAs(referenceDummy3);
            result3c.Should().NotBeNull().And.NotBeSameAs(referenceDummy3);
            result3d.Should().NotBeNull().And.NotBeSameAs(referenceDummy3);
        }

        [Fact]
        public static void ThatIsNotIn_without_IEqualityComparer___Should_return_new_dummy___When_referenceDummy_is_in_the_set_of_comparisonDummies_using_value_equality_regardless_of_maxAttempts()
        {
            // Arrange
            var now = DateTime.Now;
            var referenceDummy1 = now.ToString(CultureInfo.CurrentCulture);
            var comparisonDummy1 = now.ToString(CultureInfo.CurrentCulture);

            var comparisonDummies2 = new[] { ThatIsInOrNotInSet.One, ThatIsInOrNotInSet.Three, ThatIsInOrNotInSet.Five };

            // Act
            var result1a = referenceDummy1.ThatIsNotIn(new[] { comparisonDummy1 });
            var result1b = referenceDummy1.ThatIsNotIn(new[] { comparisonDummy1 }, maxAttempts: 101);
            var result1c = referenceDummy1.ThatIsNotIn(new[] { comparisonDummy1 }, maxAttempts: 0);
            var result1d = referenceDummy1.ThatIsNotIn(new[] { comparisonDummy1 }, maxAttempts: -1);

            var result2a = ThatIsInOrNotInSet.One.ThatIsNotIn(comparisonDummies2);
            var result2b = ThatIsInOrNotInSet.Three.ThatIsNotIn(comparisonDummies2, maxAttempts: 1001);
            var result2c = ThatIsInOrNotInSet.Five.ThatIsNotIn(comparisonDummies2, maxAttempts: 0);
            var result2d = ThatIsInOrNotInSet.One.ThatIsNotIn(comparisonDummies2, maxAttempts: -1);

            // Assert
            referenceDummy1.Should().NotBeSameAs(comparisonDummy1);
            result1a.Should().NotBe(referenceDummy1);
            result1b.Should().NotBe(referenceDummy1);
            result1c.Should().NotBe(referenceDummy1);
            result1d.Should().NotBe(referenceDummy1);

            result2a.Should().NotBe(ThatIsInOrNotInSet.One).And.NotBe(ThatIsInOrNotInSet.Three).And.NotBe(ThatIsInOrNotInSet.Five);
            result2b.Should().NotBe(ThatIsInOrNotInSet.One).And.NotBe(ThatIsInOrNotInSet.Three).And.NotBe(ThatIsInOrNotInSet.Five);
            result2c.Should().NotBe(ThatIsInOrNotInSet.One).And.NotBe(ThatIsInOrNotInSet.Three).And.NotBe(ThatIsInOrNotInSet.Five);
            result2d.Should().NotBe(ThatIsInOrNotInSet.One).And.NotBe(ThatIsInOrNotInSet.Three).And.NotBe(ThatIsInOrNotInSet.Five);
        }

        [Fact]
        public static void ThatIsNotIn_without_IEqualityComparer___Should_return_all_enum_values_not_in_comparisonDummies___When_called_many_times_on_referenceDummy_is_in_the_set_of_comparisonDummies()
        {
            // Arrange
            var comparisonDummies = new[] { ThatIsInOrNotInSet.One, ThatIsInOrNotInSet.Three, ThatIsInOrNotInSet.Five };

            // Act
            var actualDummies = new List<ThatIsInOrNotInSet>();
            for (int i = 0; i < 1000; i++)
            {
                actualDummies.Add(ThatIsInOrNotInSet.One.ThatIsNotIn(comparisonDummies));
                actualDummies.Add(ThatIsInOrNotInSet.Three.ThatIsNotIn(comparisonDummies));
                actualDummies.Add(ThatIsInOrNotInSet.Five.ThatIsNotIn(comparisonDummies));
            }

            // Assert
            actualDummies = actualDummies.Distinct().OrderBy(_ => _).ToList();
            actualDummies.Should().HaveCount(3);
            actualDummies.Should().ContainInOrder(ThatIsInOrNotInSet.None, ThatIsInOrNotInSet.Two, ThatIsInOrNotInSet.Four);
        }

        [Fact]
        public static void ThatIsNotIn_without_IEqualityComparer___Should_return_new_IList_with_specified_numberOfElements_and_specified_createWith___When_referenceDummy_was_created_by_a_called_to_SomeDummies_and_referenceDummy_contained_within_comparisonDummy()
        {
            // Arrange
            var expectedSize = ThreadSafeRandom.Next(1, 10);
            var referenceDummy = Some.Dummies<string>(expectedSize, CreateWith.OneOrMoreNulls);

            // Act
            var result = referenceDummy.ThatIsNotIn(new[] { referenceDummy }, maxAttempts: 2);

            // Assert
            result.Should().BeAssignableTo<IList<string>>();
            result.Should().NotBeNull().And.NotBeSameAs(referenceDummy);
            result.Should().HaveCount(expectedSize);
            result.Should().Contain((string)null);
        }

        [Fact]
        public static void ThatIsNotIn_without_IEqualityComparer___Should_return_new_IReadOnlyList_with_specified_numberOfElements_and_specified_createWith___When_referenceDummy_was_created_by_a_call_to_SomeReadOnlyDummies_and_referenceDummy_contained_within_comparisonDummy()
        {
            // Arrange
            var expectedSize = ThreadSafeRandom.Next(1, 10);
            var referenceDummy = Some.ReadOnlyDummies<string>(expectedSize, CreateWith.OneOrMoreNulls);

            // Act
            var result = referenceDummy.ThatIsNotIn(new[] { referenceDummy }, maxAttempts: 2);

            // Assert
            result.Should().BeAssignableTo<IReadOnlyList<string>>();
            result.Should().NotBeNull().And.NotBeSameAs(referenceDummy);
            result.Should().HaveCount(expectedSize);
            result.Should().Contain((string)null);
        }

        [Fact]
        public static void ThatIsNotIn_with_IEqualityComparer___Should_throw_ArgumentNullException___When_comparisonDummies_is_null_regardless_of_maxAttempts()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<NoInstancesAreEqual>();
            var referenceDummy2 = Some.Dummies<double>();
            var referenceDummy3 = Some.ReadOnlyDummies<double>();

            // Act
            var ex1a = Record.Exception(() => referenceDummy1.ThatIsNotIn(null, new NoInstancesAreEqualEqualityComparer()));
            var ex1b = Record.Exception(() => referenceDummy1.ThatIsNotIn(null, new NoInstancesAreEqualEqualityComparer(), maxAttempts: 101));
            var ex1c = Record.Exception(() => referenceDummy1.ThatIsNotIn(null, new NoInstancesAreEqualEqualityComparer(), maxAttempts: 0));
            var ex1d = Record.Exception(() => referenceDummy1.ThatIsNotIn(null, new NoInstancesAreEqualEqualityComparer(), maxAttempts: -1));

            var ex2a = Record.Exception(() => referenceDummy2.ThatIsNotIn(null, EqualityComparer<IList<double>>.Default));
            var ex2b = Record.Exception(() => referenceDummy2.ThatIsNotIn(null, EqualityComparer<IList<double>>.Default, maxAttempts: 101));
            var ex2c = Record.Exception(() => referenceDummy2.ThatIsNotIn(null, EqualityComparer<IList<double>>.Default, maxAttempts: 0));
            var ex2d = Record.Exception(() => referenceDummy2.ThatIsNotIn(null, EqualityComparer<IList<double>>.Default, maxAttempts: -1));

            var ex3a = Record.Exception(() => referenceDummy3.ThatIsNotIn(null, EqualityComparer<IReadOnlyList<double>>.Default));
            var ex3b = Record.Exception(() => referenceDummy3.ThatIsNotIn(null, EqualityComparer<IReadOnlyList<double>>.Default, maxAttempts: 101));
            var ex3c = Record.Exception(() => referenceDummy3.ThatIsNotIn(null, EqualityComparer<IReadOnlyList<double>>.Default, maxAttempts: 0));
            var ex3d = Record.Exception(() => referenceDummy3.ThatIsNotIn(null, EqualityComparer<IReadOnlyList<double>>.Default, maxAttempts: -1));

            // Assert
            ex1a.Should().BeOfType<ArgumentNullException>();
            ex1b.Should().BeOfType<ArgumentNullException>();
            ex1c.Should().BeOfType<ArgumentNullException>();
            ex1d.Should().BeOfType<ArgumentNullException>();

            ex2a.Should().BeOfType<ArgumentNullException>();
            ex2b.Should().BeOfType<ArgumentNullException>();
            ex2c.Should().BeOfType<ArgumentNullException>();
            ex2d.Should().BeOfType<ArgumentNullException>();

            ex3a.Should().BeOfType<ArgumentNullException>();
            ex3b.Should().BeOfType<ArgumentNullException>();
            ex3c.Should().BeOfType<ArgumentNullException>();
            ex3d.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "purposely using lower-case to test equality comparer")]
        public static void ThatIsNotIn_with_IEqualityComparer___Should_throw_InvalidOperation___When_maxAttempts_is_1_and_referenceDummy_is_in_set_of_comparisonDummies()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<string>().ToUpper(CultureInfo.InvariantCulture);
            var referenceDummy2 = Some.Dummies<string>();
            var referenceDummy3 = Some.ReadOnlyDummies<string>();

            // Act
            var ex1 = Record.Exception(() => referenceDummy1.ThatIsNotIn(new[] { referenceDummy1.ToLowerInvariant() }, StringComparer.OrdinalIgnoreCase, maxAttempts: 1));
            var ex2 = Record.Exception(() => referenceDummy2.ThatIsNotIn(new[] { referenceDummy2 }, EqualityComparer<IList<string>>.Default, maxAttempts: 1));
            var ex3 = Record.Exception(() => referenceDummy3.ThatIsNotIn(new[] { referenceDummy3 }, EqualityComparer<IReadOnlyList<string>>.Default, maxAttempts: 1));

            // Assert
            ex1.Should().BeOfType<InvalidOperationException>();
            ex2.Should().BeOfType<InvalidOperationException>();
            ex3.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void ThatIsNotIn_with_IEqualityComparer___Should_throw_InvalidOperation___When_all_possible_dummies_are_in_the_set_of_comparisonDummies_and_maxAttempts_is_greater_than_1()
        {
            // Arrange
            var comparisonDummy = A.Dummy<AllInstancesEqual>();

            // Act
            var ex = Record.Exception(() => A.Dummy<AllInstancesEqual>().ThatIsNotIn(new[] { comparisonDummy }, new AllInstancesEqualEqualityComparer(), maxAttempts: 101));

            // Assert
            ex.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void ThatIsNotIn_with_IEqualityComparer___Should_return_null___When_referenceDummy_is_null_and_comparisonDummies_is_empty_regardless_of_maxAttempts()
        {
            // Arrange
            NoInstancesAreEqual referenceDummy = null;
            var comparisonDummies = new NoInstancesAreEqual[] { };

            // Act
            var result1 = referenceDummy.ThatIsNotIn(comparisonDummies, new NoInstancesAreEqualEqualityComparer());
            var result2 = referenceDummy.ThatIsNotIn(comparisonDummies, new NoInstancesAreEqualEqualityComparer(), maxAttempts: 101);
            var result3 = referenceDummy.ThatIsNotIn(comparisonDummies, new NoInstancesAreEqualEqualityComparer(), maxAttempts: 0);
            var result4 = referenceDummy.ThatIsNotIn(comparisonDummies, new NoInstancesAreEqualEqualityComparer(), maxAttempts: -1);

            // Assert
            result1.Should().BeNull();
            result2.Should().BeNull();
            result3.Should().BeNull();
            result4.Should().BeNull();
        }

        [Fact]
        public static void ThatIsNotIn_with_IEqualityComparer___Should_return_null___When_referenceDummy_is_null_and_comparisonDummies_does_not_contain_null_regardless_of_maxAttempts()
        {
            // Arrange
            NoInstancesAreEqual referenceDummy = null;
            var comparisonDummy = A.Dummy<NoInstancesAreEqual>();

            // Act
            var result1 = referenceDummy.ThatIsNotIn(new[] { comparisonDummy }, new NoInstancesAreEqualEqualityComparer());
            var result2 = referenceDummy.ThatIsNotIn(new[] { comparisonDummy }, new NoInstancesAreEqualEqualityComparer(), maxAttempts: 101);
            var result3 = referenceDummy.ThatIsNotIn(new[] { comparisonDummy }, new NoInstancesAreEqualEqualityComparer(), maxAttempts: 0);
            var result4 = referenceDummy.ThatIsNotIn(new[] { comparisonDummy }, new NoInstancesAreEqualEqualityComparer(), maxAttempts: -1);

            // Assert
            result1.Should().BeNull();
            result2.Should().BeNull();
            result3.Should().BeNull();
            result4.Should().BeNull();
        }

        [Fact]
        public static void ThatIsNotIn_with_IEqualityComparer___Should_return_referenceDummy___When_comparisonDummies_is_empty()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<NoInstancesAreEqual>();
            var comparisonDummies1 = new NoInstancesAreEqual[] { };

            var referenceDummy2 = Some.Dummies<double>();
            var comparisonDummies2 = new IList<double>[] { };

            var referenceDummy3 = Some.ReadOnlyDummies<double>();
            var comparisonDummies3 = new IReadOnlyList<double>[] { };

            // Act
            var result1a = referenceDummy1.ThatIsNotIn(comparisonDummies1, new NoInstancesAreEqualEqualityComparer());
            var result1b = referenceDummy1.ThatIsNotIn(comparisonDummies1, new NoInstancesAreEqualEqualityComparer(), maxAttempts: 101);
            var result1c = referenceDummy1.ThatIsNotIn(comparisonDummies1, new NoInstancesAreEqualEqualityComparer(), maxAttempts: 0);
            var result1d = referenceDummy1.ThatIsNotIn(comparisonDummies1, new NoInstancesAreEqualEqualityComparer(), maxAttempts: -1);

            var result2a = referenceDummy2.ThatIsNotIn(comparisonDummies2, EqualityComparer<IList<double>>.Default);
            var result2b = referenceDummy2.ThatIsNotIn(comparisonDummies2, EqualityComparer<IList<double>>.Default, maxAttempts: 101);
            var result2c = referenceDummy2.ThatIsNotIn(comparisonDummies2, EqualityComparer<IList<double>>.Default, maxAttempts: 0);
            var result2d = referenceDummy2.ThatIsNotIn(comparisonDummies2, EqualityComparer<IList<double>>.Default, maxAttempts: -1);

            var result3a = referenceDummy3.ThatIsNotIn(comparisonDummies3, EqualityComparer<IReadOnlyList<double>>.Default);
            var result3b = referenceDummy3.ThatIsNotIn(comparisonDummies3, EqualityComparer<IReadOnlyList<double>>.Default, maxAttempts: 101);
            var result3c = referenceDummy3.ThatIsNotIn(comparisonDummies3, EqualityComparer<IReadOnlyList<double>>.Default, maxAttempts: 0);
            var result3d = referenceDummy3.ThatIsNotIn(comparisonDummies3, EqualityComparer<IReadOnlyList<double>>.Default, maxAttempts: -1);

            // Assert
            result1a.Should().BeSameAs(referenceDummy1);
            result1b.Should().BeSameAs(referenceDummy1);
            result1c.Should().BeSameAs(referenceDummy1);
            result1d.Should().BeSameAs(referenceDummy1);

            result2a.Should().BeSameAs(referenceDummy2);
            result2b.Should().BeSameAs(referenceDummy2);
            result2c.Should().BeSameAs(referenceDummy2);
            result2d.Should().BeSameAs(referenceDummy2);

            result3a.Should().BeSameAs(referenceDummy3);
            result3b.Should().BeSameAs(referenceDummy3);
            result3c.Should().BeSameAs(referenceDummy3);
            result3d.Should().BeSameAs(referenceDummy3);
        }

        [Fact]
        public static void ThatIsNotIn_with_IEqualityComparer___Should_return_referenceDummy___When_referenceDummy_is_not_in_set_of_comparisonDummies_regardless_of_maxAttempts()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<NoInstancesAreEqual>();
            var comparisonDummy1 = A.Dummy<NoInstancesAreEqual>();

            var referenceDummy2 = Some.Dummies<double>();
            var comparisonDummy2 = Some.Dummies<double>();

            var referenceDummy3 = Some.ReadOnlyDummies<double>();
            var comparisonDummy3 = Some.ReadOnlyDummies<double>();

            // Act
            var result1a = referenceDummy1.ThatIsNotIn(new[] { comparisonDummy1 }, new NoInstancesAreEqualEqualityComparer());
            var result1b = referenceDummy1.ThatIsNotIn(new[] { comparisonDummy1 }, new NoInstancesAreEqualEqualityComparer(), maxAttempts: 101);
            var result1c = referenceDummy1.ThatIsNotIn(new[] { comparisonDummy1 }, new NoInstancesAreEqualEqualityComparer(), maxAttempts: 0);
            var result1d = referenceDummy1.ThatIsNotIn(new[] { comparisonDummy1 }, new NoInstancesAreEqualEqualityComparer(), maxAttempts: -1);

            var result2a = referenceDummy2.ThatIsNotIn(new[] { comparisonDummy2 }, EqualityComparer<IList<double>>.Default);
            var result2b = referenceDummy2.ThatIsNotIn(new[] { comparisonDummy2 }, EqualityComparer<IList<double>>.Default, maxAttempts: 101);
            var result2c = referenceDummy2.ThatIsNotIn(new[] { comparisonDummy2 }, EqualityComparer<IList<double>>.Default, maxAttempts: 0);
            var result2d = referenceDummy2.ThatIsNotIn(new[] { comparisonDummy2 }, EqualityComparer<IList<double>>.Default, maxAttempts: -1);

            var result3a = referenceDummy3.ThatIsNotIn(new[] { comparisonDummy3 }, EqualityComparer<IReadOnlyList<double>>.Default);
            var result3b = referenceDummy3.ThatIsNotIn(new[] { comparisonDummy3 }, EqualityComparer<IReadOnlyList<double>>.Default, maxAttempts: 101);
            var result3c = referenceDummy3.ThatIsNotIn(new[] { comparisonDummy3 }, EqualityComparer<IReadOnlyList<double>>.Default, maxAttempts: 0);
            var result3d = referenceDummy3.ThatIsNotIn(new[] { comparisonDummy3 }, EqualityComparer<IReadOnlyList<double>>.Default, maxAttempts: -1);

            // Assert
            result1a.Should().BeSameAs(referenceDummy1);
            result1b.Should().BeSameAs(referenceDummy1);
            result1c.Should().BeSameAs(referenceDummy1);
            result1d.Should().BeSameAs(referenceDummy1);

            result2a.Should().BeSameAs(referenceDummy2);
            result2b.Should().BeSameAs(referenceDummy2);
            result2c.Should().BeSameAs(referenceDummy2);
            result2d.Should().BeSameAs(referenceDummy2);

            result3a.Should().BeSameAs(referenceDummy3);
            result3b.Should().BeSameAs(referenceDummy3);
            result3c.Should().BeSameAs(referenceDummy3);
            result3d.Should().BeSameAs(referenceDummy3);
        }

        [Fact]
        public static void ThatIsNotIn_with_IEqualityComparer___Should_return_referenceDummy___When_referenceDummy_is_not_null_and_comparisonDummies_contains_null_regardless_of_maxAttempts()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<NoInstancesAreEqual>();
            var referenceDummy2 = Some.Dummies<double>();
            var referenceDummy3 = Some.ReadOnlyDummies<double>();

            // Act
            var result1a = referenceDummy1.ThatIsNotIn(new NoInstancesAreEqual[] { null }, new NoInstancesAreEqualEqualityComparer());
            var result1b = referenceDummy1.ThatIsNotIn(new NoInstancesAreEqual[] { null }, new NoInstancesAreEqualEqualityComparer(), maxAttempts: 101);
            var result1c = referenceDummy1.ThatIsNotIn(new NoInstancesAreEqual[] { null }, new NoInstancesAreEqualEqualityComparer(), maxAttempts: 0);
            var result1d = referenceDummy1.ThatIsNotIn(new NoInstancesAreEqual[] { null }, new NoInstancesAreEqualEqualityComparer(), maxAttempts: -1);

            var result2a = referenceDummy2.ThatIsNotIn(new IList<double>[] { null }, EqualityComparer<IList<double>>.Default);
            var result2b = referenceDummy2.ThatIsNotIn(new IList<double>[] { null }, EqualityComparer<IList<double>>.Default, maxAttempts: 101);
            var result2c = referenceDummy2.ThatIsNotIn(new IList<double>[] { null }, EqualityComparer<IList<double>>.Default, maxAttempts: 0);
            var result2d = referenceDummy2.ThatIsNotIn(new IList<double>[] { null }, EqualityComparer<IList<double>>.Default, maxAttempts: -1);

            var result3a = referenceDummy3.ThatIsNotIn(new IReadOnlyList<double>[] { null }, EqualityComparer<IReadOnlyList<double>>.Default);
            var result3b = referenceDummy3.ThatIsNotIn(new IReadOnlyList<double>[] { null }, EqualityComparer<IReadOnlyList<double>>.Default, maxAttempts: 101);
            var result3c = referenceDummy3.ThatIsNotIn(new IReadOnlyList<double>[] { null }, EqualityComparer<IReadOnlyList<double>>.Default, maxAttempts: 0);
            var result3d = referenceDummy3.ThatIsNotIn(new IReadOnlyList<double>[] { null }, EqualityComparer<IReadOnlyList<double>>.Default, maxAttempts: -1);

            // Assert
            result1a.Should().BeSameAs(referenceDummy1);
            result1b.Should().BeSameAs(referenceDummy1);
            result1c.Should().BeSameAs(referenceDummy1);
            result1d.Should().BeSameAs(referenceDummy1);

            result2a.Should().BeSameAs(referenceDummy2);
            result2b.Should().BeSameAs(referenceDummy2);
            result2c.Should().BeSameAs(referenceDummy2);
            result2d.Should().BeSameAs(referenceDummy2);

            result3a.Should().BeSameAs(referenceDummy3);
            result3b.Should().BeSameAs(referenceDummy3);
            result3c.Should().BeSameAs(referenceDummy3);
            result3d.Should().BeSameAs(referenceDummy3);
        }

        [Fact]
        public static void ThatIsNotIn_with_IEqualityComparer___Should_return_new_dummy___When_referenceDummy_is_in_the_set_of_comparisonDummies_using_reference_equality_regardless_of_maxAttempts()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<NoInstancesAreEqual>();
            var referenceDummy2 = Some.Dummies<double>();
            var referenceDummy3 = Some.ReadOnlyDummies<double>();

            // Act
            var result1a = referenceDummy1.ThatIsNotIn(new[] { referenceDummy1 }, new NoInstancesAreEqualEqualityComparer());
            var result1b = referenceDummy1.ThatIsNotIn(new[] { referenceDummy1 }, new NoInstancesAreEqualEqualityComparer(), maxAttempts: 101);
            var result1c = referenceDummy1.ThatIsNotIn(new[] { referenceDummy1 }, new NoInstancesAreEqualEqualityComparer(), maxAttempts: 0);
            var result1d = referenceDummy1.ThatIsNotIn(new[] { referenceDummy1 }, new NoInstancesAreEqualEqualityComparer(), maxAttempts: -1);

            var result2a = referenceDummy2.ThatIsNotIn(new[] { referenceDummy2 }, EqualityComparer<IList<double>>.Default);
            var result2b = referenceDummy2.ThatIsNotIn(new[] { referenceDummy2 }, EqualityComparer<IList<double>>.Default, maxAttempts: 101);
            var result2c = referenceDummy2.ThatIsNotIn(new[] { referenceDummy2 }, EqualityComparer<IList<double>>.Default, maxAttempts: 0);
            var result2d = referenceDummy2.ThatIsNotIn(new[] { referenceDummy2 }, EqualityComparer<IList<double>>.Default, maxAttempts: -1);

            var result3a = referenceDummy3.ThatIsNotIn(new[] { referenceDummy3 }, EqualityComparer<IReadOnlyList<double>>.Default);
            var result3b = referenceDummy3.ThatIsNotIn(new[] { referenceDummy3 }, EqualityComparer<IReadOnlyList<double>>.Default, maxAttempts: 101);
            var result3c = referenceDummy3.ThatIsNotIn(new[] { referenceDummy3 }, EqualityComparer<IReadOnlyList<double>>.Default, maxAttempts: 0);
            var result3d = referenceDummy3.ThatIsNotIn(new[] { referenceDummy3 }, EqualityComparer<IReadOnlyList<double>>.Default, maxAttempts: -1);

            // Assert
            result1a.Should().NotBeNull().And.NotBeSameAs(referenceDummy1);
            result1b.Should().NotBeNull().And.NotBeSameAs(referenceDummy1);
            result1c.Should().NotBeNull().And.NotBeSameAs(referenceDummy1);
            result1d.Should().NotBeNull().And.NotBeSameAs(referenceDummy1);

            result2a.Should().NotBeNull().And.NotBeSameAs(referenceDummy2);
            result2b.Should().NotBeNull().And.NotBeSameAs(referenceDummy2);
            result2c.Should().NotBeNull().And.NotBeSameAs(referenceDummy2);
            result2d.Should().NotBeNull().And.NotBeSameAs(referenceDummy2);

            result3a.Should().NotBeNull().And.NotBeSameAs(referenceDummy3);
            result3b.Should().NotBeNull().And.NotBeSameAs(referenceDummy3);
            result3c.Should().NotBeNull().And.NotBeSameAs(referenceDummy3);
            result3d.Should().NotBeNull().And.NotBeSameAs(referenceDummy3);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "purposely using lower-case to test equality comparer")]
        public static void ThatIsNotIn_with_IEqualityComparer___Should_return_new_dummy___When_referenceDummy_is_in_the_set_of_comparisonDummies_using_value_equality_regardless_of_maxAttempts()
        {
            // Arrange
            var now = DateTime.Now;
            var referenceDummy1 = now.ToString(CultureInfo.CurrentCulture).ToUpperInvariant();
            var comparisonDummy1 = now.ToString(CultureInfo.CurrentCulture).ToLowerInvariant();

            var comparisonDummies2 = new[] { ThatIsInOrNotInSet.One, ThatIsInOrNotInSet.Three, ThatIsInOrNotInSet.Five };

            // Act
            var result1a = referenceDummy1.ThatIsNotIn(new[] { comparisonDummy1 }, StringComparer.OrdinalIgnoreCase);
            var result1b = referenceDummy1.ThatIsNotIn(new[] { comparisonDummy1 }, StringComparer.OrdinalIgnoreCase, maxAttempts: 101);
            var result1c = referenceDummy1.ThatIsNotIn(new[] { comparisonDummy1 }, StringComparer.OrdinalIgnoreCase, maxAttempts: 0);
            var result1d = referenceDummy1.ThatIsNotIn(new[] { comparisonDummy1 }, StringComparer.OrdinalIgnoreCase, maxAttempts: -1);

            var result2a = ThatIsInOrNotInSet.One.ThatIsNotIn(comparisonDummies2, EqualityComparer<ThatIsInOrNotInSet>.Default);
            var result2b = ThatIsInOrNotInSet.Three.ThatIsNotIn(comparisonDummies2, EqualityComparer<ThatIsInOrNotInSet>.Default, maxAttempts: 1001);
            var result2c = ThatIsInOrNotInSet.Five.ThatIsNotIn(comparisonDummies2, EqualityComparer<ThatIsInOrNotInSet>.Default, maxAttempts: 0);
            var result2d = ThatIsInOrNotInSet.One.ThatIsNotIn(comparisonDummies2, EqualityComparer<ThatIsInOrNotInSet>.Default, maxAttempts: -1);

            // Assert
            referenceDummy1.Should().NotBeSameAs(comparisonDummy1);
            result1a.ToUpperInvariant().Should().NotBe(referenceDummy1);
            result1b.ToUpperInvariant().Should().NotBe(referenceDummy1);
            result1c.ToUpperInvariant().Should().NotBe(referenceDummy1);
            result1d.ToUpperInvariant().Should().NotBe(referenceDummy1);

            result2a.Should().NotBe(ThatIsInOrNotInSet.One).And.NotBe(ThatIsInOrNotInSet.Three).And.NotBe(ThatIsInOrNotInSet.Five);
            result2b.Should().NotBe(ThatIsInOrNotInSet.One).And.NotBe(ThatIsInOrNotInSet.Three).And.NotBe(ThatIsInOrNotInSet.Five);
            result2c.Should().NotBe(ThatIsInOrNotInSet.One).And.NotBe(ThatIsInOrNotInSet.Three).And.NotBe(ThatIsInOrNotInSet.Five);
            result2d.Should().NotBe(ThatIsInOrNotInSet.One).And.NotBe(ThatIsInOrNotInSet.Three).And.NotBe(ThatIsInOrNotInSet.Five);
        }

        [Fact]
        public static void ThatIsNotIn_with_IEqualityComparer___Should_return_all_enum_values_not_in_comparisonDummies___When_called_many_times_on_referenceDummy_is_in_the_set_of_comparisonDummies()
        {
            // Arrange
            var comparisonDummies = new[] { ThatIsInOrNotInSet.One, ThatIsInOrNotInSet.Three, ThatIsInOrNotInSet.Five };

            // Act
            var actualDummies = new List<ThatIsInOrNotInSet>();
            for (int i = 0; i < 1000; i++)
            {
                actualDummies.Add(ThatIsInOrNotInSet.One.ThatIsNotIn(comparisonDummies, EqualityComparer<ThatIsInOrNotInSet>.Default));
                actualDummies.Add(ThatIsInOrNotInSet.Three.ThatIsNotIn(comparisonDummies, EqualityComparer<ThatIsInOrNotInSet>.Default));
                actualDummies.Add(ThatIsInOrNotInSet.Five.ThatIsNotIn(comparisonDummies, EqualityComparer<ThatIsInOrNotInSet>.Default));
            }

            // Assert
            actualDummies = actualDummies.Distinct().OrderBy(_ => _).ToList();
            actualDummies.Should().HaveCount(3);
            actualDummies.Should().ContainInOrder(ThatIsInOrNotInSet.None, ThatIsInOrNotInSet.Two, ThatIsInOrNotInSet.Four);
        }

        [Fact]
        public static void ThatIsNotIn_with_IEqualityComparer___Should_return_new_IList_with_specified_numberOfElements_and_specified_createWith___When_referenceDummy_was_created_by_a_called_to_SomeDummies_and_referenceDummy_contained_within_comparisonDummy()
        {
            // Arrange
            var expectedSize = ThreadSafeRandom.Next(1, 10);
            var referenceDummy = Some.Dummies<string>(expectedSize, CreateWith.OneOrMoreNulls);

            // Act
            var result = referenceDummy.ThatIsNotIn(new[] { referenceDummy }, EqualityComparer<IList<string>>.Default, maxAttempts: 2);

            // Assert
            result.Should().BeAssignableTo<IList<string>>();
            result.Should().NotBeNull().And.NotBeSameAs(referenceDummy);
            result.Should().HaveCount(expectedSize);
            result.Should().Contain((string)null);
        }

        [Fact]
        public static void ThatIsNotIn_with_IEqualityComparer___Should_return_new_IReadOnlyList_with_specified_numberOfElements_and_specified_createWith___When_referenceDummy_was_created_by_a_call_to_SomeReadOnlyDummies_and_referenceDummy_contained_within_comparisonDummy()
        {
            // Arrange
            var expectedSize = ThreadSafeRandom.Next(1, 10);
            var referenceDummy = Some.ReadOnlyDummies<string>(expectedSize, CreateWith.OneOrMoreNulls);

            // Act
            var result = referenceDummy.ThatIsNotIn(new[] { referenceDummy }, EqualityComparer<IReadOnlyList<string>>.Default, maxAttempts: 2);

            // Assert
            result.Should().BeAssignableTo<IReadOnlyList<string>>();
            result.Should().NotBeNull().And.NotBeSameAs(referenceDummy);
            result.Should().HaveCount(expectedSize);
            result.Should().Contain((string)null);
        }

        [Fact]
        public static void ThatIsIn_without_IEqualityComparer___Should_throw_ArgumentNullException___When_comparisonDummies_is_null_regardless_of_maxAttempts()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<NoInstancesAreEqual>();
            var referenceDummy2 = Some.Dummies<double>();
            var referenceDummy3 = Some.ReadOnlyDummies<double>();

            // Act
            var ex1a = Record.Exception(() => referenceDummy1.ThatIsIn(null));
            var ex1b = Record.Exception(() => referenceDummy1.ThatIsIn(null, maxAttempts: 101));
            var ex1c = Record.Exception(() => referenceDummy1.ThatIsIn(null, maxAttempts: 0));
            var ex1d = Record.Exception(() => referenceDummy1.ThatIsIn(null, maxAttempts: -1));

            var ex2a = Record.Exception(() => referenceDummy2.ThatIsIn(null));
            var ex2b = Record.Exception(() => referenceDummy2.ThatIsIn(null, maxAttempts: 101));
            var ex2c = Record.Exception(() => referenceDummy2.ThatIsIn(null, maxAttempts: 0));
            var ex2d = Record.Exception(() => referenceDummy2.ThatIsIn(null, maxAttempts: -1));

            var ex3a = Record.Exception(() => referenceDummy3.ThatIsIn(null));
            var ex3b = Record.Exception(() => referenceDummy3.ThatIsIn(null, maxAttempts: 101));
            var ex3c = Record.Exception(() => referenceDummy3.ThatIsIn(null, maxAttempts: 0));
            var ex3d = Record.Exception(() => referenceDummy3.ThatIsIn(null, maxAttempts: -1));

            // Assert
            ex1a.Should().BeOfType<ArgumentNullException>();
            ex1b.Should().BeOfType<ArgumentNullException>();
            ex1c.Should().BeOfType<ArgumentNullException>();
            ex1d.Should().BeOfType<ArgumentNullException>();

            ex2a.Should().BeOfType<ArgumentNullException>();
            ex2b.Should().BeOfType<ArgumentNullException>();
            ex2c.Should().BeOfType<ArgumentNullException>();
            ex2d.Should().BeOfType<ArgumentNullException>();

            ex3a.Should().BeOfType<ArgumentNullException>();
            ex3b.Should().BeOfType<ArgumentNullException>();
            ex3c.Should().BeOfType<ArgumentNullException>();
            ex3d.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void ThatIsIn_without_IEqualityComparer___Should_throw_InvalidOperationException___When_maxAttempts_is_1_and_referenceDummy_is_not_in_set_of_comparisonDummies()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<string>();
            var referenceDummy2 = Some.Dummies<string>();
            var referenceDummy3 = Some.ReadOnlyDummies<string>();

            // Act
            var ex1 = Record.Exception(() => referenceDummy1.ThatIsIn(new[] { A.Dummy<string>() }, maxAttempts: 1));
            var ex2 = Record.Exception(() => referenceDummy2.ThatIsIn(new[] { Some.Dummies<string>() }, maxAttempts: 1));
            var ex3 = Record.Exception(() => referenceDummy3.ThatIsIn(new[] { Some.ReadOnlyDummies<string>() }, maxAttempts: 1));

            // Assert
            ex1.Should().BeOfType<InvalidOperationException>();
            ex2.Should().BeOfType<InvalidOperationException>();
            ex3.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void ThatIsIn_without_IEqualityComparer___Should_throw_InvalidOperationException___When_referenceDummy_is_not_null_and_comparisonDummies_is_empty_and_max_attempts_is_greater_than_1()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<AllInstancesEqual>();
            var comparisonDummies1 = new AllInstancesEqual[] { };

            var referenceDummy2 = Some.Dummies<double>();
            var comparisonDummies2 = new IList<double>[] { };

            var referenceDummy3 = Some.ReadOnlyDummies<double>();
            var comparisonDummies3 = new IReadOnlyList<double>[] { };

            // Act
            var ex1 = Record.Exception(() => referenceDummy1.ThatIsIn(comparisonDummies1, maxAttempts: 101));
            var ex2 = Record.Exception(() => referenceDummy2.ThatIsIn(comparisonDummies2, maxAttempts: 101));
            var ex3 = Record.Exception(() => referenceDummy3.ThatIsIn(comparisonDummies3, maxAttempts: 101));

            // Assert
            ex1.Should().BeOfType<InvalidOperationException>();
            ex2.Should().BeOfType<InvalidOperationException>();
            ex3.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void ThatIsIn_without_IEqualityComparer___Should_throw_InvalidOperationException___When_referenceDummy_is_null_and_comparisonDummies_is_empty_and_max_attempts_is_greater_than_1()
        {
            // Arrange
            NoInstancesAreEqual referenceDummy = null;
            var comparisonDummies = new NoInstancesAreEqual[] { };

            // Act
            var ex = Record.Exception(() => referenceDummy.ThatIsIn(comparisonDummies, maxAttempts: 101));

            // Assert
            ex.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void ThatIsIn_without_IEqualityComparer___Should_throw_InvalidOperationException___When_all_possible_dummies_are_not_in_the_set_of_comparisonDummies_and_maxAttempts_is_greater_than_1()
        {
            // Arrange
            var comparisonDummy = A.Dummy<NoInstancesAreEqual>();

            // Act
            var ex = Record.Exception(() => A.Dummy<NoInstancesAreEqual>().ThatIsIn(new[] { comparisonDummy }, maxAttempts: 101));

            // Assert
            ex.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void ThatIsIn_without_IEqualityComparer___Should_throw_InvalidOperationException___When_referenceDummy_is_not_null_and_comparisonDummies_contains_only_null_and_maxAttempts_is_greater_than_1()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<NoInstancesAreEqual>();
            var referenceDummy2 = Some.Dummies<double>();
            var referenceDummy3 = Some.ReadOnlyDummies<double>();

            // Act
            var ex1 = Record.Exception(() => referenceDummy1.ThatIsIn(new NoInstancesAreEqual[] { null }, maxAttempts: 101));
            var ex2 = Record.Exception(() => referenceDummy2.ThatIsIn(new IList<double>[] { null }, maxAttempts: 101));
            var ex3 = Record.Exception(() => referenceDummy3.ThatIsIn(new IReadOnlyList<double>[] { null }, maxAttempts: 101));

            // Assert
            ex1.Should().BeOfType<InvalidOperationException>();
            ex2.Should().BeOfType<InvalidOperationException>();
            ex3.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void ThatIsIn_without_IEqualityComparer___Should_throw_InvalidOperationException___When_referenceDummy_is_null_and_comparisonDummies_does_not_contain_null_and_not_possible_create_a_dummy_that_is_in_comparisonDummies_and_maxAttempts_is_greater_than_1()
        {
            // Arrange
            NoInstancesAreEqual referenceDummy = null;
            var comparisonDummies = new[] { A.Dummy<NoInstancesAreEqual>() };

            // Act
            var ex1 = Record.Exception(() => referenceDummy.ThatIsIn(comparisonDummies, maxAttempts: 101));

            // Assert
            ex1.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void ThatIsIn_without_IEqualityComparer___Should_return_null___When_referenceDummy_is_null_and_comparisonDummies_contains_null_regardless_of_maxAttempts()
        {
            // Arrange
            NoInstancesAreEqual referenceDummy = null;
            var comparisonDummy = new NoInstancesAreEqual[] { null };

            // Act
            var result1 = referenceDummy.ThatIsIn(comparisonDummy);
            var result2 = referenceDummy.ThatIsIn(comparisonDummy, maxAttempts: 101);
            var result3 = referenceDummy.ThatIsIn(comparisonDummy, maxAttempts: 0);
            var result4 = referenceDummy.ThatIsIn(comparisonDummy, maxAttempts: -1);

            // Assert
            result1.Should().BeNull();
            result2.Should().BeNull();
            result3.Should().BeNull();
            result4.Should().BeNull();
        }

        [Fact]
        public static void ThatIsIn_without_IEqualityComparer___Should_return_referenceDummy___When_referenceDummy_is_in_set_of_comparisonDummies_regardless_of_maxAttempts()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<NoInstancesAreEqual>();
            var referenceDummy2 = Some.Dummies<double>();
            var referenceDummy3 = Some.ReadOnlyDummies<double>();

            // Act
            var result1a = referenceDummy1.ThatIsIn(new[] { referenceDummy1 });
            var result1b = referenceDummy1.ThatIsIn(new[] { referenceDummy1 }, maxAttempts: 101);
            var result1c = referenceDummy1.ThatIsIn(new[] { referenceDummy1 }, maxAttempts: 0);
            var result1d = referenceDummy1.ThatIsIn(new[] { referenceDummy1 }, maxAttempts: -1);

            var result2a = referenceDummy2.ThatIsIn(new[] { referenceDummy2 });
            var result2b = referenceDummy2.ThatIsIn(new[] { referenceDummy2 }, maxAttempts: 101);
            var result2c = referenceDummy2.ThatIsIn(new[] { referenceDummy2 }, maxAttempts: 0);
            var result2d = referenceDummy2.ThatIsIn(new[] { referenceDummy2 }, maxAttempts: -1);

            var result3a = referenceDummy3.ThatIsIn(new[] { referenceDummy3 });
            var result3b = referenceDummy3.ThatIsIn(new[] { referenceDummy3 }, maxAttempts: 101);
            var result3c = referenceDummy3.ThatIsIn(new[] { referenceDummy3 }, maxAttempts: 0);
            var result3d = referenceDummy3.ThatIsIn(new[] { referenceDummy3 }, maxAttempts: -1);

            // Assert
            result1a.Should().BeSameAs(referenceDummy1);
            result1b.Should().BeSameAs(referenceDummy1);
            result1c.Should().BeSameAs(referenceDummy1);
            result1d.Should().BeSameAs(referenceDummy1);

            result2a.Should().BeSameAs(referenceDummy2);
            result2b.Should().BeSameAs(referenceDummy2);
            result2c.Should().BeSameAs(referenceDummy2);
            result2d.Should().BeSameAs(referenceDummy2);

            result3a.Should().BeSameAs(referenceDummy3);
            result3b.Should().BeSameAs(referenceDummy3);
            result3c.Should().BeSameAs(referenceDummy3);
            result3d.Should().BeSameAs(referenceDummy3);
        }

        [Fact]
        public static void ThatIsIn_without_IEqualityComparer___Should_return_new_dummy___When_referenceDummy_is_in_not_in_the_set_of_comparisonDummies_regardless_of_maxAttempts()
        {
            // Arrange
            var comparisonDummies = new[] { ThatIsInOrNotInSet.One, ThatIsInOrNotInSet.Three, ThatIsInOrNotInSet.Five };

            // Act
            var result1 = ThatIsInOrNotInSet.None.ThatIsIn(comparisonDummies);
            var result2 = ThatIsInOrNotInSet.Two.ThatIsIn(comparisonDummies, maxAttempts: 101);
            var result3 = ThatIsInOrNotInSet.Four.ThatIsIn(comparisonDummies, maxAttempts: 0);
            var result4 = ThatIsInOrNotInSet.None.ThatIsIn(comparisonDummies, maxAttempts: -1);

            // Assert
            result1.Should().NotBe(ThatIsInOrNotInSet.None).And.NotBe(ThatIsInOrNotInSet.Two).And.NotBe(ThatIsInOrNotInSet.Four);
            result2.Should().NotBe(ThatIsInOrNotInSet.None).And.NotBe(ThatIsInOrNotInSet.Two).And.NotBe(ThatIsInOrNotInSet.Four);
            result3.Should().NotBe(ThatIsInOrNotInSet.None).And.NotBe(ThatIsInOrNotInSet.Two).And.NotBe(ThatIsInOrNotInSet.Four);
            result4.Should().NotBe(ThatIsInOrNotInSet.None).And.NotBe(ThatIsInOrNotInSet.Two).And.NotBe(ThatIsInOrNotInSet.Four);
        }

        [Fact]
        public static void ThatIsIn_without_IEqualityComparer___Should_return_new_dummy___When_referenceDummy_is_null_and_comparisonDummies_does_not_contain_null_and_it_is_possible_to_create_dummy_that_is_in_comparisonDummies_and_maxAttempts_is_greater_than_1()
        {
            // Arrange
            var referenceDummy = (ThatIsInOrNotInSet?)null;
            var comparisonDummies = new ThatIsInOrNotInSet?[] { ThatIsInOrNotInSet.One, ThatIsInOrNotInSet.Three, ThatIsInOrNotInSet.Five };

            // Act
            var result = referenceDummy.ThatIsIn(comparisonDummies, maxAttempts: 101);

            // Assert
            comparisonDummies.Should().Contain(result);
        }

        [Fact]
        public static void ThatIsIn_without_IEqualityComparer___Should_return_all_enum_values_in_comparisonDummies___When_called_many_times_on_referenceDummy_is_not_in_the_set_of_comparisonDummies()
        {
            // Arrange
            var comparisonDummies = new[] { ThatIsInOrNotInSet.One, ThatIsInOrNotInSet.Three, ThatIsInOrNotInSet.Five };

            // Act
            var actualDummies = new List<ThatIsInOrNotInSet>();
            for (int i = 0; i < 1000; i++)
            {
                actualDummies.Add(ThatIsInOrNotInSet.None.ThatIsIn(comparisonDummies));
                actualDummies.Add(ThatIsInOrNotInSet.Two.ThatIsIn(comparisonDummies));
                actualDummies.Add(ThatIsInOrNotInSet.Four.ThatIsIn(comparisonDummies));
            }

            // Assert
            actualDummies = actualDummies.Distinct().OrderBy(_ => _).ToList();
            actualDummies.Should().HaveCount(3);
            actualDummies.Should().ContainInOrder(ThatIsInOrNotInSet.One, ThatIsInOrNotInSet.Three, ThatIsInOrNotInSet.Five);
        }

        [Fact]
        public static void ThatIsIn_with_IEqualityComparer___Should_throw_ArgumentNullException___When_comparisonDummies_is_null_regardless_of_maxAttempts()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<NoInstancesAreEqual>();
            var referenceDummy2 = Some.Dummies<double>();
            var referenceDummy3 = Some.ReadOnlyDummies<double>();

            // Act
            var ex1a = Record.Exception(() => referenceDummy1.ThatIsIn(null, new NoInstancesAreEqualEqualityComparer()));
            var ex1b = Record.Exception(() => referenceDummy1.ThatIsIn(null, new NoInstancesAreEqualEqualityComparer(), maxAttempts: 101));
            var ex1c = Record.Exception(() => referenceDummy1.ThatIsIn(null, new NoInstancesAreEqualEqualityComparer(), maxAttempts: 0));
            var ex1d = Record.Exception(() => referenceDummy1.ThatIsIn(null, new NoInstancesAreEqualEqualityComparer(), maxAttempts: -1));

            var ex2a = Record.Exception(() => referenceDummy2.ThatIsIn(null, EqualityComparer<IList<double>>.Default));
            var ex2b = Record.Exception(() => referenceDummy2.ThatIsIn(null, EqualityComparer<IList<double>>.Default, maxAttempts: 101));
            var ex2c = Record.Exception(() => referenceDummy2.ThatIsIn(null, EqualityComparer<IList<double>>.Default, maxAttempts: 0));
            var ex2d = Record.Exception(() => referenceDummy2.ThatIsIn(null, EqualityComparer<IList<double>>.Default, maxAttempts: -1));

            var ex3a = Record.Exception(() => referenceDummy3.ThatIsIn(null, EqualityComparer<IReadOnlyList<double>>.Default));
            var ex3b = Record.Exception(() => referenceDummy3.ThatIsIn(null, EqualityComparer<IReadOnlyList<double>>.Default, maxAttempts: 101));
            var ex3c = Record.Exception(() => referenceDummy3.ThatIsIn(null, EqualityComparer<IReadOnlyList<double>>.Default, maxAttempts: 0));
            var ex3d = Record.Exception(() => referenceDummy3.ThatIsIn(null, EqualityComparer<IReadOnlyList<double>>.Default, maxAttempts: -1));

            // Assert
            ex1a.Should().BeOfType<ArgumentNullException>();
            ex1b.Should().BeOfType<ArgumentNullException>();
            ex1c.Should().BeOfType<ArgumentNullException>();
            ex1d.Should().BeOfType<ArgumentNullException>();

            ex2a.Should().BeOfType<ArgumentNullException>();
            ex2b.Should().BeOfType<ArgumentNullException>();
            ex2c.Should().BeOfType<ArgumentNullException>();
            ex2d.Should().BeOfType<ArgumentNullException>();

            ex3a.Should().BeOfType<ArgumentNullException>();
            ex3b.Should().BeOfType<ArgumentNullException>();
            ex3c.Should().BeOfType<ArgumentNullException>();
            ex3d.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void ThatIsIn_with_IEqualityComparer___Should_throw_InvalidOperationException___When_maxAttempts_is_1_and_referenceDummy_is_not_in_set_of_comparisonDummies()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<string>();
            var referenceDummy2 = Some.Dummies<string>();
            var referenceDummy3 = Some.ReadOnlyDummies<string>();

            // Act
            var ex1 = Record.Exception(() => referenceDummy1.ThatIsIn(new[] { A.Dummy<string>() }, EqualityComparer<string>.Default, maxAttempts: 1));
            var ex2 = Record.Exception(() => referenceDummy2.ThatIsIn(new[] { Some.Dummies<string>() }, EqualityComparer<IList<string>>.Default, maxAttempts: 1));
            var ex3 = Record.Exception(() => referenceDummy3.ThatIsIn(new[] { Some.ReadOnlyDummies<string>() }, EqualityComparer<IReadOnlyList<string>>.Default, maxAttempts: 1));

            // Assert
            ex1.Should().BeOfType<InvalidOperationException>();
            ex2.Should().BeOfType<InvalidOperationException>();
            ex3.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void ThatIsIn_with_IEqualityComparer___Should_throw_InvalidOperationException___When_referenceDummy_is_not_null_and_comparisonDummies_is_empty_and_max_attempts_is_greater_than_1()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<AllInstancesEqual>();
            var comparisonDummies1 = new AllInstancesEqual[] { };

            var referenceDummy2 = Some.Dummies<double>();
            var comparisonDummies2 = new IList<double>[] { };

            var referenceDummy3 = Some.ReadOnlyDummies<double>();
            var comparisonDummies3 = new IReadOnlyList<double>[] { };

            // Act
            var ex1 = Record.Exception(() => referenceDummy1.ThatIsIn(comparisonDummies1, new AllInstancesEqualEqualityComparer(), maxAttempts: 101));
            var ex2 = Record.Exception(() => referenceDummy2.ThatIsIn(comparisonDummies2, EqualityComparer<IList<double>>.Default, maxAttempts: 101));
            var ex3 = Record.Exception(() => referenceDummy3.ThatIsIn(comparisonDummies3, EqualityComparer<IReadOnlyList<double>>.Default, maxAttempts: 101));

            // Assert
            ex1.Should().BeOfType<InvalidOperationException>();
            ex2.Should().BeOfType<InvalidOperationException>();
            ex3.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void ThatIsIn_with_IEqualityComparer___Should_throw_InvalidOperationException___When_referenceDummy_is_null_and_comparisonDummies_is_empty_and_max_attempts_is_greater_than_1()
        {
            // Arrange
            NoInstancesAreEqual referenceDummy = null;
            var comparisonDummies = new NoInstancesAreEqual[] { };

            // Act
            var ex = Record.Exception(() => referenceDummy.ThatIsIn(comparisonDummies, new NoInstancesAreEqualEqualityComparer(), maxAttempts: 101));

            // Assert
            ex.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void ThatIsIn_with_IEqualityComparer___Should_throw_InvalidOperationException___When_all_possible_dummies_are_not_in_the_set_of_comparisonDummies_and_maxAttempts_is_greater_than_1()
        {
            // Arrange
            var comparisonDummy = A.Dummy<NoInstancesAreEqual>();

            // Act
            var ex = Record.Exception(() => A.Dummy<NoInstancesAreEqual>().ThatIsIn(new[] { comparisonDummy }, new NoInstancesAreEqualEqualityComparer(), maxAttempts: 101));

            // Assert
            ex.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void ThatIsIn_with_IEqualityComparer___Should_throw_InvalidOperationException___When_referenceDummy_is_not_null_and_comparisonDummies_contains_only_null_and_maxAttempts_is_greater_than_1()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<NoInstancesAreEqual>();
            var referenceDummy2 = Some.Dummies<double>();
            var referenceDummy3 = Some.ReadOnlyDummies<double>();

            // Act
            var ex1 = Record.Exception(() => referenceDummy1.ThatIsIn(new NoInstancesAreEqual[] { null }, new NoInstancesAreEqualEqualityComparer(), maxAttempts: 101));
            var ex2 = Record.Exception(() => referenceDummy2.ThatIsIn(new IList<double>[] { null }, EqualityComparer<IList<double>>.Default, maxAttempts: 101));
            var ex3 = Record.Exception(() => referenceDummy3.ThatIsIn(new IReadOnlyList<double>[] { null }, EqualityComparer<IReadOnlyList<double>>.Default, maxAttempts: 101));

            // Assert
            ex1.Should().BeOfType<InvalidOperationException>();
            ex2.Should().BeOfType<InvalidOperationException>();
            ex3.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void ThatIsIn_with_IEqualityComparer___Should_throw_InvalidOperationException___When_referenceDummy_is_null_and_comparisonDummies_does_not_contain_null_and_not_possible_create_a_dummy_that_is_in_comparisonDummies_and_maxAttempts_is_greater_than_1()
        {
            // Arrange
            NoInstancesAreEqual referenceDummy = null;
            var comparisonDummies = new[] { A.Dummy<NoInstancesAreEqual>() };

            // Act
            var ex1 = Record.Exception(() => referenceDummy.ThatIsIn(comparisonDummies, new NoInstancesAreEqualEqualityComparer(), maxAttempts: 101));

            // Assert
            ex1.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void ThatIsIn_with_IEqualityComparer___Should_return_null___When_referenceDummy_is_null_and_comparisonDummies_contains_null_regardless_of_maxAttempts()
        {
            // Arrange
            NoInstancesAreEqual referenceDummy = null;
            var comparisonDummy = new NoInstancesAreEqual[] { null };

            // Act
            var result1 = referenceDummy.ThatIsIn(comparisonDummy, new NoInstancesAreEqualEqualityComparer());
            var result2 = referenceDummy.ThatIsIn(comparisonDummy, new NoInstancesAreEqualEqualityComparer(), maxAttempts: 101);
            var result3 = referenceDummy.ThatIsIn(comparisonDummy, new NoInstancesAreEqualEqualityComparer(), maxAttempts: 0);
            var result4 = referenceDummy.ThatIsIn(comparisonDummy, new NoInstancesAreEqualEqualityComparer(), maxAttempts: -1);

            // Assert
            result1.Should().BeNull();
            result2.Should().BeNull();
            result3.Should().BeNull();
            result4.Should().BeNull();
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "purposely using lower-case to test equality comparer")]
        public static void ThatIsIn_with_IEqualityComparer___Should_return_referenceDummy___When_referenceDummy_is_in_set_of_comparisonDummies_regardless_of_maxAttempts()
        {
            // Arrange
            var referenceDummy1 = A.Dummy<string>().ToUpperInvariant();
            var referenceDummy2 = Some.Dummies<double>();
            var referenceDummy3 = Some.ReadOnlyDummies<double>();

            // Act
            var result1a = referenceDummy1.ThatIsIn(new[] { referenceDummy1.ToLowerInvariant() }, StringComparer.OrdinalIgnoreCase);
            var result1b = referenceDummy1.ThatIsIn(new[] { referenceDummy1.ToLowerInvariant() }, StringComparer.OrdinalIgnoreCase, maxAttempts: 101);
            var result1c = referenceDummy1.ThatIsIn(new[] { referenceDummy1.ToLowerInvariant() }, StringComparer.OrdinalIgnoreCase, maxAttempts: 0);
            var result1d = referenceDummy1.ThatIsIn(new[] { referenceDummy1.ToLowerInvariant() }, StringComparer.OrdinalIgnoreCase, maxAttempts: -1);

            var result2a = referenceDummy2.ThatIsIn(new[] { referenceDummy2 }, EqualityComparer<IList<double>>.Default);
            var result2b = referenceDummy2.ThatIsIn(new[] { referenceDummy2 }, EqualityComparer<IList<double>>.Default, maxAttempts: 101);
            var result2c = referenceDummy2.ThatIsIn(new[] { referenceDummy2 }, EqualityComparer<IList<double>>.Default, maxAttempts: 0);
            var result2d = referenceDummy2.ThatIsIn(new[] { referenceDummy2 }, EqualityComparer<IList<double>>.Default, maxAttempts: -1);

            var result3a = referenceDummy3.ThatIsIn(new[] { referenceDummy3 }, EqualityComparer<IReadOnlyList<double>>.Default);
            var result3b = referenceDummy3.ThatIsIn(new[] { referenceDummy3 }, EqualityComparer<IReadOnlyList<double>>.Default, maxAttempts: 101);
            var result3c = referenceDummy3.ThatIsIn(new[] { referenceDummy3 }, EqualityComparer<IReadOnlyList<double>>.Default, maxAttempts: 0);
            var result3d = referenceDummy3.ThatIsIn(new[] { referenceDummy3 }, EqualityComparer<IReadOnlyList<double>>.Default, maxAttempts: -1);

            // Assert
            result1a.Should().BeSameAs(referenceDummy1);
            result1b.Should().BeSameAs(referenceDummy1);
            result1c.Should().BeSameAs(referenceDummy1);
            result1d.Should().BeSameAs(referenceDummy1);

            result2a.Should().BeSameAs(referenceDummy2);
            result2b.Should().BeSameAs(referenceDummy2);
            result2c.Should().BeSameAs(referenceDummy2);
            result2d.Should().BeSameAs(referenceDummy2);

            result3a.Should().BeSameAs(referenceDummy3);
            result3b.Should().BeSameAs(referenceDummy3);
            result3c.Should().BeSameAs(referenceDummy3);
            result3d.Should().BeSameAs(referenceDummy3);
        }

        [Fact]
        public static void ThatIsIn_with_IEqualityComparer___Should_return_new_dummy___When_referenceDummy_is_in_not_in_the_set_of_comparisonDummies_regardless_of_maxAttempts()
        {
            // Arrange
            var comparisonDummies = new[] { ThatIsInOrNotInSet.One, ThatIsInOrNotInSet.Three, ThatIsInOrNotInSet.Five };

            // Act
            var result1 = ThatIsInOrNotInSet.None.ThatIsIn(comparisonDummies, EqualityComparer<ThatIsInOrNotInSet>.Default);
            var result2 = ThatIsInOrNotInSet.Two.ThatIsIn(comparisonDummies, EqualityComparer<ThatIsInOrNotInSet>.Default, maxAttempts: 101);
            var result3 = ThatIsInOrNotInSet.Four.ThatIsIn(comparisonDummies, EqualityComparer<ThatIsInOrNotInSet>.Default, maxAttempts: 0);
            var result4 = ThatIsInOrNotInSet.None.ThatIsIn(comparisonDummies, EqualityComparer<ThatIsInOrNotInSet>.Default, maxAttempts: -1);

            // Assert
            result1.Should().NotBe(ThatIsInOrNotInSet.None).And.NotBe(ThatIsInOrNotInSet.Two).And.NotBe(ThatIsInOrNotInSet.Four);
            result2.Should().NotBe(ThatIsInOrNotInSet.None).And.NotBe(ThatIsInOrNotInSet.Two).And.NotBe(ThatIsInOrNotInSet.Four);
            result3.Should().NotBe(ThatIsInOrNotInSet.None).And.NotBe(ThatIsInOrNotInSet.Two).And.NotBe(ThatIsInOrNotInSet.Four);
            result4.Should().NotBe(ThatIsInOrNotInSet.None).And.NotBe(ThatIsInOrNotInSet.Two).And.NotBe(ThatIsInOrNotInSet.Four);
        }

        [Fact]
        public static void ThatIsIn_with_IEqualityComparer___Should_return_new_dummy___When_referenceDummy_is_null_and_comparisonDummies_does_not_contain_null_and_it_is_possible_to_create_dummy_that_is_in_comparisonDummies_and_maxAttempts_is_greater_than_1()
        {
            // Arrange
            var referenceDummy = (ThatIsInOrNotInSet?)null;
            var comparisonDummies = new ThatIsInOrNotInSet?[] { ThatIsInOrNotInSet.One, ThatIsInOrNotInSet.Three, ThatIsInOrNotInSet.Five };

            // Act
            var result = referenceDummy.ThatIsIn(comparisonDummies, EqualityComparer<ThatIsInOrNotInSet?>.Default, maxAttempts: 101);

            // Assert
            comparisonDummies.Should().Contain(result);
        }

        [Fact]
        public static void ThatIsIn_with_IEqualityComparer___Should_return_all_enum_values_in_comparisonDummies___When_called_many_times_on_referenceDummy_is_not_in_the_set_of_comparisonDummies()
        {
            // Arrange
            var comparisonDummies = new[] { ThatIsInOrNotInSet.One, ThatIsInOrNotInSet.Three, ThatIsInOrNotInSet.Five };

            // Act
            var actualDummies = new List<ThatIsInOrNotInSet>();
            for (int i = 0; i < 1000; i++)
            {
                actualDummies.Add(ThatIsInOrNotInSet.None.ThatIsIn(comparisonDummies, EqualityComparer<ThatIsInOrNotInSet>.Default));
                actualDummies.Add(ThatIsInOrNotInSet.Two.ThatIsIn(comparisonDummies, EqualityComparer<ThatIsInOrNotInSet>.Default));
                actualDummies.Add(ThatIsInOrNotInSet.Four.ThatIsIn(comparisonDummies, EqualityComparer<ThatIsInOrNotInSet>.Default));
            }

            // Assert
            actualDummies = actualDummies.Distinct().OrderBy(_ => _).ToList();
            actualDummies.Should().HaveCount(3);
            actualDummies.Should().ContainInOrder(ThatIsInOrNotInSet.One, ThatIsInOrNotInSet.Three, ThatIsInOrNotInSet.Five);
        }

        [Fact]
        public static void ThatIsInRange_without_IComparer___Should_throw_ArgumentException___When_parameter_rangeStartInclusive_is_less_than_parameter_rangeEndInclusive()
        {
            // Arrange
            var referenceDummy = A.Dummy<int>();

            // Act
            var ex1 = Record.Exception(() => referenceDummy.ThatIsInRange(11, 10));
            var ex2 = Record.Exception(() => referenceDummy.ThatIsInRange(11, 10, maxAttempts: 1));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex1.Message.Should().Contain("rangeStartInclusive is > rangeEndInclusive");

            ex2.Should().BeOfType<ArgumentException>();
            ex2.Message.Should().Contain("rangeStartInclusive is > rangeEndInclusive");
        }

        [Fact]
        public static void ThatIsInRange_without_IComparer___Should_throw_ArgumentException___When_parameter_rangeStartInclusive_is_not_null_and_rangeEndInclusive_is_null()
        {
            // Arrange
            var referenceDummy = new ComparableIntAsObject(A.Dummy<int>());
            var startRangeInclusive = new ComparableIntAsObject(A.Dummy<int>());

            // Act
            var ex1 = Record.Exception(() => referenceDummy.ThatIsInRange(startRangeInclusive, null));
            var ex2 = Record.Exception(() => referenceDummy.ThatIsInRange(startRangeInclusive, null, maxAttempts: 1));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex1.Message.Should().Contain("rangeStartInclusive is > rangeEndInclusive");

            ex2.Should().BeOfType<ArgumentException>();
            ex2.Message.Should().Contain("rangeStartInclusive is > rangeEndInclusive");
        }

        [Fact]
        public static void ThatIsInRange_without_IComparer___Should_not_throw___When_parameter_rangeStartInclusive_is_null_and_rangeEndInclusive_is_null()
        {
            // Arrange
            ComparableIntAsObject referenceDummy = null;

            // Act
            var ex1 = Record.Exception(() => referenceDummy.ThatIsInRange(null, null));
            var ex2 = Record.Exception(() => referenceDummy.ThatIsInRange(null, null, maxAttempts: 1));

            // Assert
            ex1.Should().BeNull();
            ex2.Should().BeNull();
        }

        [Fact]
        public static void ThatIsInRange_without_IComparer___Should_not_throw___When_parameter_rangeStartInclusive_is_null_and_rangeEndInclusive_is_not_null()
        {
            // Arrange
            var referenceDummy = new ComparableIntAsObject(50);
            var endRangeInclusive = new ComparableIntAsObject(100);

            // Act
            var ex1 = Record.Exception(() => referenceDummy.ThatIsInRange(null, endRangeInclusive));
            var ex2 = Record.Exception(() => referenceDummy.ThatIsInRange(null, endRangeInclusive, maxAttempts: 1));

            // Assert
            ex1.Should().BeNull();
            ex2.Should().BeNull();
        }

        [Fact]
        public static void ThatIsInRange_without_IComparer___Should_return_referenceDummy___When_referenceDummy_is_within_the_specified_range()
        {
            // Arrange
            var referenceDummy1 = 11;
            ComparableIntAsObject referenceDummy2 = null;
            var referenceDummy3 = new ComparableIntAsObject(50);

            // Act
            var actual1 = referenceDummy1.ThatIsInRange(11, 11);
            var actual2 = referenceDummy1.ThatIsInRange(11, 12);
            var actual3 = referenceDummy1.ThatIsInRange(10, 11);
            var actual4 = referenceDummy1.ThatIsInRange(10, 12);

            var actual5 = referenceDummy2.ThatIsInRange(null, null);
            var actual6 = referenceDummy2.ThatIsInRange(null, new ComparableIntAsObject(A.Dummy<int>()));

            var actual7 = referenceDummy3.ThatIsInRange(null, new ComparableIntAsObject(51));
            var actual8 = referenceDummy3.ThatIsInRange(new ComparableIntAsObject(49), new ComparableIntAsObject(51));

            // Assert
            actual1.Should().Be(referenceDummy1);
            actual2.Should().Be(referenceDummy1);
            actual3.Should().Be(referenceDummy1);
            actual4.Should().Be(referenceDummy1);

            actual5.Should().BeNull();
            actual6.Should().BeNull();

            actual7.Should().BeSameAs(referenceDummy3);
            actual8.Should().BeSameAs(referenceDummy3);
        }

        [Fact]
        public static void ThatIsInRange_without_IComparer___Should_return_new_dummy_that_is_within_specified_range___When_referenceDummy_is_not_within_the_specified_range()
        {
            // Arrange
            var startRangeInclusive = -192322;
            var endRangeInclusive = 500000;

            var referenceDummy1 = -192323;
            ComparableIntAsObject referenceDummy2 = null;
            var referenceDummy3 = new ComparableIntAsObject(referenceDummy1);

            var actual1 = new List<int>();
            var actual2 = new List<ComparableIntAsObject>();
            var actual3 = new List<ComparableIntAsObject>();

            // Act
            for (int x = 0; x < 1000; x++)
            {
                actual1.Add(referenceDummy1.ThatIsInRange(startRangeInclusive, endRangeInclusive));
                actual2.Add(referenceDummy2.ThatIsInRange(new ComparableIntAsObject(startRangeInclusive), new ComparableIntAsObject(endRangeInclusive)));
                actual3.Add(referenceDummy3.ThatIsInRange(new ComparableIntAsObject(startRangeInclusive), new ComparableIntAsObject(endRangeInclusive)));
            }

            // Assert
            actual1.Select(_ => (_ >= startRangeInclusive) && (_ <= endRangeInclusive)).ToList().ForEach(_ => _.Should().BeTrue());
            actual2.Select(_ => (_.Value >= startRangeInclusive) && (_.Value <= endRangeInclusive)).ToList().ForEach(_ => _.Should().BeTrue());
            actual3.Select(_ => (_.Value >= startRangeInclusive) && (_.Value <= endRangeInclusive)).ToList().ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void ThatIsInRange_with_IComparer___Should_throw_ArgumentNullException___When_parameter_comparer_is_null()
        {
            // Arrange
            var referenceDummy = A.Dummy<int>();

            // Act
            var actual = Record.Exception(() => referenceDummy.ThatIsInRange(10, 11, null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("comparer");
        }

        [Fact]
        public static void ThatIsInRange_with_IComparer___Should_throw_ArgumentException___When_parameter_rangeStartInclusive_is_less_than_parameter_rangeEndInclusive()
        {
            // Arrange
            var referenceDummy = A.Dummy<int>();
            var comparer = Comparer<int>.Default;

            // Act
            var ex1 = Record.Exception(() => referenceDummy.ThatIsInRange(11, 10, comparer));
            var ex2 = Record.Exception(() => referenceDummy.ThatIsInRange(11, 10, comparer, maxAttempts: 1));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex1.Message.Should().Contain("rangeStartInclusive is > rangeEndInclusive");

            ex2.Should().BeOfType<ArgumentException>();
            ex2.Message.Should().Contain("rangeStartInclusive is > rangeEndInclusive");
        }

        [Fact]
        public static void ThatIsInRange_with_IComparer___Should_throw_ArgumentException___When_parameter_rangeStartInclusive_is_not_null_and_rangeEndInclusive_is_null()
        {
            // Arrange
            var referenceDummy = new ComparableIntAsObject(A.Dummy<int>());
            var startRangeInclusive = new ComparableIntAsObject(A.Dummy<int>());
            var comparer = new ComparableIntAsObjectComparer();

            // Act
            var ex1 = Record.Exception(() => referenceDummy.ThatIsInRange(startRangeInclusive, null, comparer));
            var ex2 = Record.Exception(() => referenceDummy.ThatIsInRange(startRangeInclusive, null, comparer, maxAttempts: 1));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex1.Message.Should().Contain("rangeStartInclusive is > rangeEndInclusive");

            ex2.Should().BeOfType<ArgumentException>();
            ex2.Message.Should().Contain("rangeStartInclusive is > rangeEndInclusive");
        }

        [Fact]
        public static void ThatIsInRange_with_IComparer___Should_not_throw___When_parameter_rangeStartInclusive_is_null_and_rangeEndInclusive_is_null()
        {
            // Arrange
            ComparableIntAsObject referenceDummy = null;
            var comparer = new ComparableIntAsObjectComparer();

            // Act
            var ex1 = Record.Exception(() => referenceDummy.ThatIsInRange(null, null, comparer));
            var ex2 = Record.Exception(() => referenceDummy.ThatIsInRange(null, null, comparer, maxAttempts: 1));

            // Assert
            ex1.Should().BeNull();
            ex2.Should().BeNull();
        }

        [Fact]
        public static void ThatIsInRange_with_IComparer___Should_not_throw___When_parameter_rangeStartInclusive_is_null_and_rangeEndInclusive_is_not_null()
        {
            // Arrange
            var referenceDummy = new ComparableIntAsObject(50);
            var endRangeInclusive = new ComparableIntAsObject(100);
            var comparer = new ComparableIntAsObjectComparer();

            // Act
            var ex1 = Record.Exception(() => referenceDummy.ThatIsInRange(null, endRangeInclusive, comparer));
            var ex2 = Record.Exception(() => referenceDummy.ThatIsInRange(null, endRangeInclusive, comparer, maxAttempts: 1));

            // Assert
            ex1.Should().BeNull();
            ex2.Should().BeNull();
        }

        [Fact]
        public static void ThatIsInRange_with_IComparer___Should_return_referenceDummy___When_referenceDummy_is_within_the_specified_range()
        {
            // Arrange
            var referenceDummy1 = 11;
            ComparableIntAsObject referenceDummy2 = null;
            var referenceDummy3 = new ComparableIntAsObject(50);

            var comparer1 = Comparer<int>.Default;
            var comparer2 = new ComparableIntAsObjectComparer();
            var comparer3 = new ComparableIntAsObjectComparer();

            // Act
            var actual1 = referenceDummy1.ThatIsInRange(11, 11, comparer1);
            var actual2 = referenceDummy1.ThatIsInRange(11, 12, comparer1);
            var actual3 = referenceDummy1.ThatIsInRange(10, 11, comparer1);
            var actual4 = referenceDummy1.ThatIsInRange(10, 12, comparer1);

            var actual5 = referenceDummy2.ThatIsInRange(null, null, comparer2);
            var actual6 = referenceDummy2.ThatIsInRange(null, new ComparableIntAsObject(A.Dummy<int>()), comparer2);

            var actual7 = referenceDummy3.ThatIsInRange(null, new ComparableIntAsObject(51), comparer3);
            var actual8 = referenceDummy3.ThatIsInRange(new ComparableIntAsObject(49), new ComparableIntAsObject(51), comparer3);

            // Assert
            actual1.Should().Be(referenceDummy1);
            actual2.Should().Be(referenceDummy1);
            actual3.Should().Be(referenceDummy1);
            actual4.Should().Be(referenceDummy1);

            actual5.Should().BeNull();
            actual6.Should().BeNull();

            actual7.Should().BeSameAs(referenceDummy3);
            actual8.Should().BeSameAs(referenceDummy3);
        }

        [Fact]
        public static void ThatIsInRange_with_IComparer___Should_return_new_dummy_that_is_within_specified_range___When_referenceDummy_is_not_within_the_specified_range()
        {
            // Arrange
            var startRangeInclusive = -192322;
            var endRangeInclusive = 500000;

            var referenceDummy1 = -192323;
            ComparableIntAsObject referenceDummy2 = null;
            var referenceDummy3 = new ComparableIntAsObject(referenceDummy1);

            var comparer1 = Comparer<int>.Default;
            var comparer2 = new ComparableIntAsObjectComparer();
            var comparer3 = new ComparableIntAsObjectComparer();

            var actual1 = new List<int>();
            var actual2 = new List<ComparableIntAsObject>();
            var actual3 = new List<ComparableIntAsObject>();

            // Act
            for (int x = 0; x < 1000; x++)
            {
                actual1.Add(referenceDummy1.ThatIsInRange(startRangeInclusive, endRangeInclusive, comparer1));
                actual2.Add(referenceDummy2.ThatIsInRange(new ComparableIntAsObject(startRangeInclusive), new ComparableIntAsObject(endRangeInclusive), comparer2));
                actual3.Add(referenceDummy3.ThatIsInRange(new ComparableIntAsObject(startRangeInclusive), new ComparableIntAsObject(endRangeInclusive), comparer3));
            }

            // Assert
            actual1.Select(_ => (_ >= startRangeInclusive) && (_ <= endRangeInclusive)).ToList().ForEach(_ => _.Should().BeTrue());
            actual2.Select(_ => (_.Value >= startRangeInclusive) && (_.Value <= endRangeInclusive)).ToList().ForEach(_ => _.Should().BeTrue());
            actual3.Select(_ => (_.Value >= startRangeInclusive) && (_.Value <= endRangeInclusive)).ToList().ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void ThatIsNotInRange_without_IComparer___Should_throw_ArgumentException___When_parameter_rangeStartInclusive_is_less_than_parameter_rangeEndInclusive()
        {
            // Arrange
            var referenceDummy = A.Dummy<int>();

            // Act
            var ex1 = Record.Exception(() => referenceDummy.ThatIsNotInRange(11, 10));
            var ex2 = Record.Exception(() => referenceDummy.ThatIsNotInRange(11, 10, maxAttempts: 1));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex1.Message.Should().Contain("rangeStartInclusive is > rangeEndInclusive");

            ex2.Should().BeOfType<ArgumentException>();
            ex2.Message.Should().Contain("rangeStartInclusive is > rangeEndInclusive");
        }

        [Fact]
        public static void ThatIsNotInRange_without_IComparer___Should_throw_ArgumentException___When_parameter_rangeStartInclusive_is_not_null_and_rangeEndInclusive_is_null()
        {
            // Arrange
            var referenceDummy = new ComparableIntAsObject(A.Dummy<int>());
            var startRangeInclusive = new ComparableIntAsObject(A.Dummy<int>());

            // Act
            var ex1 = Record.Exception(() => referenceDummy.ThatIsNotInRange(startRangeInclusive, null));
            var ex2 = Record.Exception(() => referenceDummy.ThatIsNotInRange(startRangeInclusive, null, maxAttempts: 1));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex1.Message.Should().Contain("rangeStartInclusive is > rangeEndInclusive");

            ex2.Should().BeOfType<ArgumentException>();
            ex2.Message.Should().Contain("rangeStartInclusive is > rangeEndInclusive");
        }

        [Fact]
        public static void ThatIsNotInRange_without_IComparer___Should_not_throw___When_parameter_rangeStartInclusive_is_null_and_rangeEndInclusive_is_null()
        {
            // Arrange
            ComparableIntAsObject referenceDummy = null;

            // Act
            var ex = Record.Exception(() => referenceDummy.ThatIsNotInRange(null, null));

            // Assert
            ex.Should().BeNull();
        }

        [Fact]
        public static void ThatIsNotInRange_without_IComparer___Should_not_throw___When_parameter_rangeStartInclusive_is_null_and_rangeEndInclusive_is_not_null()
        {
            // Arrange
            var referenceDummy = new ComparableIntAsObject(50);
            var endRangeInclusive = new ComparableIntAsObject(100);

            // Act
            var ex = Record.Exception(() => referenceDummy.ThatIsNotInRange(null, endRangeInclusive));

            // Assert
            ex.Should().BeNull();
        }

        [Fact]
        public static void ThatIsNotInRange_without_IComparer___Should_return_referenceDummy___When_referenceDummy_is_not_within_the_specified_range()
        {
            // Arrange
            var referenceDummy1 = 11;
            ComparableIntAsObject referenceDummy2 = null;
            var referenceDummy3 = new ComparableIntAsObject(50);

            // Act
            var actual1 = referenceDummy1.ThatIsNotInRange(12, 12);
            var actual2 = referenceDummy1.ThatIsNotInRange(10, 10);
            var actual3 = referenceDummy1.ThatIsNotInRange(1, 10);
            var actual4 = referenceDummy1.ThatIsNotInRange(12, 20);

            var actual5 = referenceDummy2.ThatIsNotInRange(new ComparableIntAsObject(50), new ComparableIntAsObject(100));

            var actual6 = referenceDummy3.ThatIsNotInRange(null, null);
            var actual7 = referenceDummy3.ThatIsNotInRange(null, new ComparableIntAsObject(49));
            var actual8 = referenceDummy3.ThatIsNotInRange(new ComparableIntAsObject(0), new ComparableIntAsObject(49));
            var actual9 = referenceDummy3.ThatIsNotInRange(new ComparableIntAsObject(51), new ComparableIntAsObject(100));

            // Assert
            actual1.Should().Be(referenceDummy1);
            actual2.Should().Be(referenceDummy1);
            actual3.Should().Be(referenceDummy1);
            actual4.Should().Be(referenceDummy1);

            actual5.Should().BeNull();

            actual6.Should().BeSameAs(referenceDummy3);
            actual7.Should().BeSameAs(referenceDummy3);
            actual8.Should().BeSameAs(referenceDummy3);
            actual9.Should().BeSameAs(referenceDummy3);
        }

        [Fact]
        public static void ThatIsNotInRange_without_IComparer___Should_return_new_dummy_that_is_not_within_specified_range___When_referenceDummy_is_within_the_specified_range()
        {
            // Arrange
            var startRangeInclusive = -1000;
            var endRangeInclusive = 1000;

            var referenceDummy1 = startRangeInclusive;
            ComparableIntAsObject referenceDummy2 = null;
            ComparableIntAsObject referenceDummy3 = null;
            var referenceDummy4 = new ComparableIntAsObject(int.MinValue);
            var referenceDummy5 = new ComparableIntAsObject(endRangeInclusive - 1);

            var actual1 = new List<int>();
            var actual2 = new List<ComparableIntAsObject>();
            var actual3 = new List<ComparableIntAsObject>();
            var actual4 = new List<ComparableIntAsObject>();
            var actual5 = new List<ComparableIntAsObject>();

            // Act
            for (int x = 0; x < 1000; x++)
            {
                actual1.Add(referenceDummy1.ThatIsNotInRange(startRangeInclusive, endRangeInclusive));
                actual2.Add(referenceDummy2.ThatIsNotInRange(null, null));
                actual3.Add(referenceDummy3.ThatIsNotInRange(null, new ComparableIntAsObject(endRangeInclusive)));
                actual4.Add(referenceDummy4.ThatIsNotInRange(null, new ComparableIntAsObject(endRangeInclusive)));
                actual5.Add(referenceDummy5.ThatIsNotInRange(new ComparableIntAsObject(startRangeInclusive), new ComparableIntAsObject(endRangeInclusive)));
            }

            // Assert
            actual1.Select(_ => (_ < startRangeInclusive) || (_ > endRangeInclusive)).ToList().ForEach(_ => _.Should().BeTrue());
            actual2.ForEach(_ => _.Should().NotBeNull());
            actual3.Select(_ => _.Value > endRangeInclusive).ToList().ForEach(_ => _.Should().BeTrue());
            actual4.Select(_ => _.Value > endRangeInclusive).ToList().ForEach(_ => _.Should().BeTrue());
            actual5.Select(_ => (_.Value < startRangeInclusive) || (_.Value > endRangeInclusive)).ToList().ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void ThatIsNotInRange_with_IComparer___Should_throw_ArgumentNullException___When_parameter_comparer_is_null()
        {
            // Arrange
            var referenceDummy = A.Dummy<int>();

            // Act
            var actual = Record.Exception(() => referenceDummy.ThatIsNotInRange(10, 11, null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("comparer");
        }

        [Fact]
        public static void ThatIsNotInRange_with_IComparer___Should_throw_ArgumentException___When_parameter_rangeStartInclusive_is_less_than_parameter_rangeEndInclusive()
        {
            // Arrange
            var referenceDummy = A.Dummy<int>();
            var comparer = Comparer<int>.Default;

            // Act
            var ex1 = Record.Exception(() => referenceDummy.ThatIsNotInRange(11, 10, comparer));
            var ex2 = Record.Exception(() => referenceDummy.ThatIsNotInRange(11, 10, comparer, maxAttempts: 1));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex1.Message.Should().Contain("rangeStartInclusive is > rangeEndInclusive");

            ex2.Should().BeOfType<ArgumentException>();
            ex2.Message.Should().Contain("rangeStartInclusive is > rangeEndInclusive");
        }

        [Fact]
        public static void ThatIsNotInRange_with_IComparer___Should_throw_ArgumentException___When_parameter_rangeStartInclusive_is_not_null_and_rangeEndInclusive_is_null()
        {
            // Arrange
            var referenceDummy = new ComparableIntAsObject(A.Dummy<int>());
            var startRangeInclusive = new ComparableIntAsObject(A.Dummy<int>());
            var comparer = new ComparableIntAsObjectComparer();

            // Act
            var ex1 = Record.Exception(() => referenceDummy.ThatIsNotInRange(startRangeInclusive, null, comparer));
            var ex2 = Record.Exception(() => referenceDummy.ThatIsNotInRange(startRangeInclusive, null, comparer, maxAttempts: 1));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex1.Message.Should().Contain("rangeStartInclusive is > rangeEndInclusive");

            ex2.Should().BeOfType<ArgumentException>();
            ex2.Message.Should().Contain("rangeStartInclusive is > rangeEndInclusive");
        }

        [Fact]
        public static void ThatIsNotInRange_with_IComparer___Should_not_throw___When_parameter_rangeStartInclusive_is_null_and_rangeEndInclusive_is_null()
        {
            // Arrange
            ComparableIntAsObject referenceDummy = null;
            var comparer = new ComparableIntAsObjectComparer();

            // Act
            var ex = Record.Exception(() => referenceDummy.ThatIsNotInRange(null, null, comparer));

            // Assert
            ex.Should().BeNull();
        }

        [Fact]
        public static void ThatIsNotInRange_with_IComparer___Should_not_throw___When_parameter_rangeStartInclusive_is_null_and_rangeEndInclusive_is_not_null()
        {
            // Arrange
            var referenceDummy = new ComparableIntAsObject(50);
            var endRangeInclusive = new ComparableIntAsObject(100);
            var comparer = new ComparableIntAsObjectComparer();

            // Act
            var ex = Record.Exception(() => referenceDummy.ThatIsNotInRange(null, endRangeInclusive, comparer));

            // Assert
            ex.Should().BeNull();
        }

        [Fact]
        public static void ThatIsNotInRange_with_IComparer___Should_return_referenceDummy___When_referenceDummy_is_not_within_the_specified_range()
        {
            // Arrange
            var referenceDummy1 = 11;
            ComparableIntAsObject referenceDummy2 = null;
            var referenceDummy3 = new ComparableIntAsObject(50);

            var comparer1 = Comparer<int>.Default;
            var comparer2 = new ComparableIntAsObjectComparer();
            var comparer3 = new ComparableIntAsObjectComparer();

            // Act
            var actual1 = referenceDummy1.ThatIsNotInRange(12, 12, comparer1);
            var actual2 = referenceDummy1.ThatIsNotInRange(10, 10, comparer1);
            var actual3 = referenceDummy1.ThatIsNotInRange(1, 10, comparer1);
            var actual4 = referenceDummy1.ThatIsNotInRange(12, 20, comparer1);

            var actual5 = referenceDummy2.ThatIsNotInRange(new ComparableIntAsObject(50), new ComparableIntAsObject(100), comparer2);

            var actual6 = referenceDummy3.ThatIsNotInRange(null, null, comparer3);
            var actual7 = referenceDummy3.ThatIsNotInRange(null, new ComparableIntAsObject(49), comparer3);
            var actual8 = referenceDummy3.ThatIsNotInRange(new ComparableIntAsObject(0), new ComparableIntAsObject(49), comparer3);
            var actual9 = referenceDummy3.ThatIsNotInRange(new ComparableIntAsObject(51), new ComparableIntAsObject(100), comparer3);

            // Assert
            actual1.Should().Be(referenceDummy1);
            actual2.Should().Be(referenceDummy1);
            actual3.Should().Be(referenceDummy1);
            actual4.Should().Be(referenceDummy1);

            actual5.Should().BeNull();

            actual6.Should().BeSameAs(referenceDummy3);
            actual7.Should().BeSameAs(referenceDummy3);
            actual8.Should().BeSameAs(referenceDummy3);
            actual9.Should().BeSameAs(referenceDummy3);
        }

        [Fact]
        public static void ThatIsNotInRange_with_IComparer___Should_return_new_dummy_that_is_not_within_specified_range___When_referenceDummy_is_within_the_specified_range()
        {
            // Arrange
            var startRangeInclusive = -1000;
            var endRangeInclusive = 1000;

            var referenceDummy1 = startRangeInclusive;
            ComparableIntAsObject referenceDummy2 = null;
            ComparableIntAsObject referenceDummy3 = null;
            var referenceDummy4 = new ComparableIntAsObject(int.MinValue);
            var referenceDummy5 = new ComparableIntAsObject(endRangeInclusive - 1);

            var comparer1 = Comparer<int>.Default;
            var comparer2 = new ComparableIntAsObjectComparer();
            var comparer3 = new ComparableIntAsObjectComparer();
            var comparer4 = new ComparableIntAsObjectComparer();
            var comparer5 = new ComparableIntAsObjectComparer();

            var actual1 = new List<int>();
            var actual2 = new List<ComparableIntAsObject>();
            var actual3 = new List<ComparableIntAsObject>();
            var actual4 = new List<ComparableIntAsObject>();
            var actual5 = new List<ComparableIntAsObject>();

            // Act
            for (int x = 0; x < 1000; x++)
            {
                actual1.Add(referenceDummy1.ThatIsNotInRange(startRangeInclusive, endRangeInclusive, comparer1));
                actual2.Add(referenceDummy2.ThatIsNotInRange(null, null, comparer2));
                actual3.Add(referenceDummy3.ThatIsNotInRange(null, new ComparableIntAsObject(endRangeInclusive), comparer3));
                actual4.Add(referenceDummy4.ThatIsNotInRange(null, new ComparableIntAsObject(endRangeInclusive), comparer4));
                actual5.Add(referenceDummy5.ThatIsNotInRange(new ComparableIntAsObject(startRangeInclusive), new ComparableIntAsObject(endRangeInclusive), comparer5));
            }

            // Assert
            actual1.Select(_ => (_ < startRangeInclusive) || (_ > endRangeInclusive)).ToList().ForEach(_ => _.Should().BeTrue());
            actual2.ForEach(_ => _.Should().NotBeNull());
            actual3.Select(_ => _.Value > endRangeInclusive).ToList().ForEach(_ => _.Should().BeTrue());
            actual4.Select(_ => _.Value > endRangeInclusive).ToList().ForEach(_ => _.Should().BeTrue());
            actual5.Select(_ => (_.Value < startRangeInclusive) || (_.Value > endRangeInclusive)).ToList().ForEach(_ => _.Should().BeTrue());
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
    }
}
