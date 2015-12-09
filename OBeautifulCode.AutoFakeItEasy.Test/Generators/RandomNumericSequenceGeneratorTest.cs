// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RandomNumericSequenceGeneratorTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
namespace OBeautifulCode.AutoFakeItEasy.Test
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using OBeautifulCode.Math;

    using Ploeh.AutoFixture.Kernel;

    using Xunit;

    /// <summary>
    /// Tests the <see cref="RandomNumericSequenceGenerator"/> class.
    /// </summary>
    public static class RandomNumericSequenceGeneratorTest
    {
        [Fact]
        public static void Constructor___Should_throw_ArgumentOutOfRangeException___When_parameter_inclusiveLowerLimit_is_greater_than_or_equal_to_parameter_exclusiveUpperLimit()
        {
            // Arrange
            var randomInt = ThreadSafeRandom.Next(int.MinValue + 1, int.MaxValue);

            // Act
            var ex1 = Record.Exception(() => new RandomNumericSequenceGenerator(randomInt, randomInt));
            var ex2 = Record.Exception(() => new RandomNumericSequenceGenerator(randomInt, randomInt - 1));

            // Assert
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void Constructor___Should_not_throw___When_parameter_inclusiveLowerLimit_is_less_than_parameter_exclusiveUpperLimit()
        {
            // Arrange
            var randomInt = ThreadSafeRandom.Next(int.MinValue, int.MaxValue);

            // Act
            var ex1 = Record.Exception(() => new RandomNumericSequenceGenerator(randomInt, randomInt + 1));
            var ex2 = Record.Exception(() => new RandomNumericSequenceGenerator(int.MinValue, int.MaxValue));

            // Assert
            ex1.Should().BeNull();
            ex2.Should().BeNull();
        }

        [Fact]
        public static void Constructor___Should_return_an_object_that_is_assignable_to_ISpecimenBuilder___When_object_is_constructed()
        {
            // Arrange, Act
            var systemUnderTest = new RandomNumericSequenceGenerator(int.MinValue, int.MaxValue);

            // Assert
            systemUnderTest.Should().BeAssignableTo<ISpecimenBuilder>();
        }

        [Fact]
        public static void Create___Should_returns_an_object_equal_to_NoSpecimen___When_called_with_null_request()
        {
            // Arrange
            var dummyContainer = new DummySpecimenContext();
            var systemUnderTest = new RandomNumericSequenceGenerator(int.MinValue, int.MaxValue);
            var expectedResult = new NoSpecimen();

            // Act
            var actualResult = systemUnderTest.Create(null, dummyContainer);

            // Assert
            actualResult.Should().Be(expectedResult);
        }

        [Fact]
        public static void Create___Should_not_throw___When_called_with_null_container()
        {
            // Arrange
            var systemUnderTest = new RandomNumericSequenceGenerator(int.MinValue, int.MaxValue);
            var dummyRequest = new object();

            // Act
            var ex = Record.Exception(() => systemUnderTest.Create(dummyRequest, null));

            // Assert
            ex.Should().BeNull();
        }

        [Fact]
        public static void Create___Should_return_an_object_equal_to_NoSpecimen___When_request_is_not_a_numeric_type()
        {
            // Arrange
            var dummyContainer = new DummySpecimenContext();
            var systemUnderTest = new RandomNumericSequenceGenerator(int.MinValue, int.MaxValue);
            var expectedResult = new NoSpecimen();
            var request = new object();

            // Act
            var actualResult = systemUnderTest.Create(request, dummyContainer);

            // Assert
            actualResult.Should().Be(expectedResult);
        }

        [Fact]
        public static void Create___Should_returns_a_numeric_of_the_same_type_as_request___When_request_is_a_numeric_type()
        {
            // Arrange
            var dummyContainer = new DummySpecimenContext();
            var systemUnderTest = new RandomNumericSequenceGenerator(int.MinValue, int.MaxValue);

            // Act
            var actualByteResult = systemUnderTest.Create(typeof(byte), dummyContainer);
            var actualDecimalResult = systemUnderTest.Create(typeof(decimal), dummyContainer);
            var actualDoubleResult = systemUnderTest.Create(typeof(double), dummyContainer);
            var actualShortResult = systemUnderTest.Create(typeof(short), dummyContainer);
            var actualIntResult = systemUnderTest.Create(typeof(int), dummyContainer);
            var actualLongResult = systemUnderTest.Create(typeof(long), dummyContainer);
            var actualSbyteResult = systemUnderTest.Create(typeof(sbyte), dummyContainer);
            var actualFloatResult = systemUnderTest.Create(typeof(float), dummyContainer);
            var actualUshortesult = systemUnderTest.Create(typeof(ushort), dummyContainer);
            var actualUintResult = systemUnderTest.Create(typeof(uint), dummyContainer);
            var actualUlongResult = systemUnderTest.Create(typeof(ulong), dummyContainer);

            // Assert
            actualByteResult.Should().BeOfType<byte>();
            actualDecimalResult.Should().BeOfType<decimal>();
            actualDoubleResult.Should().BeOfType<double>();
            actualShortResult.Should().BeOfType<short>();
            actualIntResult.Should().BeOfType<int>();
            actualLongResult.Should().BeOfType<long>();
            actualSbyteResult.Should().BeOfType<sbyte>();
            actualFloatResult.Should().BeOfType<float>();
            actualUshortesult.Should().BeOfType<ushort>();
            actualUintResult.Should().BeOfType<uint>();
            actualUlongResult.Should().BeOfType<ulong>();
        }

        [Fact]
        public static void Create___Should_return_unique_numeric_values___When_the_number_of_calls_to_Create_equals_the_size_of_the_range_of_the_generator()
        {
            // Arrange
            var dummyContainer = new DummySpecimenContext();
            var systemUnderTest = new RandomNumericSequenceGenerator(sbyte.MinValue, sbyte.MaxValue);

            // Act
            var actualResult = new List<short>();
            for (int i = sbyte.MinValue; i < sbyte.MaxValue; i++)
            {
                actualResult.Add((sbyte)systemUnderTest.Create(typeof(sbyte), dummyContainer));
            }

            // Assert
            actualResult.Should().OnlyHaveUniqueItems();
        }

        [Fact]
        public static void Create___Should_reset_uniqueness_check___When_the_number_of_calls_to_Create_exceeds_the_size_of_the_range_of_the_generator()
        {
            // Arrange
            var dummyContainer = new DummySpecimenContext();
            var systemUnderTest = new RandomNumericSequenceGenerator(sbyte.MinValue, sbyte.MaxValue);
            var pastResult = new List<short>();
            for (int i = sbyte.MinValue; i < sbyte.MaxValue; i++)
            {
                pastResult.Add((sbyte)systemUnderTest.Create(typeof(sbyte), dummyContainer));
            }

            // Act
            var actualResult = (sbyte)systemUnderTest.Create(typeof(sbyte), dummyContainer);

            // Assert
            pastResult.Should().Contain(actualResult);
        }

        [Fact]
        public static void Create___Should_always_return_values_within_specified_generator_range___When_Create_is_called_many_times()
        {
            // Arrange
            var dummyContainer = new DummySpecimenContext();
            var systemUnderTest = new RandomNumericSequenceGenerator(sbyte.MinValue, sbyte.MaxValue);

            // Act
            var actualResult = new List<short>();
            for (int i = 0; i < 10000; i++)
            {
                actualResult.Add((sbyte)systemUnderTest.Create(typeof(sbyte), dummyContainer));
            }

            // Assert
            actualResult.ForEach(_ => _.Should().BeGreaterOrEqualTo(sbyte.MinValue));
            actualResult.ForEach(_ => _.Should().BeLessThan(sbyte.MaxValue));
        }
    }
}

// ReSharper restore InconsistentNaming
// ReSharper restore CheckNamespace