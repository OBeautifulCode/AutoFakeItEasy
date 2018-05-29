// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RandomEnumSequenceGeneratorTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy.Test
{
    using System;
    using System.Linq;

    using FluentAssertions;

    using Ploeh.AutoFixture.Kernel;

    using Xunit;

    public static class RandomEnumSequenceGeneratorTest
    {
        [Fact]
        public static void Constructor___Should_return_an_object_that_is_assignable_to_ISpecimenBuilder___When_object_is_constructed()
        {
            // Arrange, Act
            var systemUnderTest = new RandomEnumSequenceGenerator();

            // Assert
            systemUnderTest.Should().BeAssignableTo<ISpecimenBuilder>();
        }

        [Fact]
        public static void Create___Should_returns_an_object_equal_to_NoSpecimen___When_called_with_null_request()
        {
            // Arrange
            var dummyContainer = new DummySpecimenContext();
            var systemUnderTest = new RandomEnumSequenceGenerator();
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
            var systemUnderTest = new RandomEnumSequenceGenerator();
            var dummyRequest = new object();

            // Act
            var ex = Record.Exception(() => systemUnderTest.Create(dummyRequest, null));

            // Assert
            ex.Should().BeNull();
        }

        [Fact]
        public static void Create___Should_return_an_object_equal_to_NoSpecimen___When_request_is_not_an_enum()
        {
            // Arrange
            var dummyContainer = new DummySpecimenContext();
            var systemUnderTest = new RandomEnumSequenceGenerator();
            var expectedResult = new NoSpecimen();
            var request = new object();

            // Act
            var actualResult = systemUnderTest.Create(request, dummyContainer);

            // Assert
            actualResult.Should().Be(expectedResult);
        }

        [Fact]
        public static void Create___Should_returns_an_enum_of_the_same_type_as_request___When_request_is_for_an_enum()
        {
            // Arrange
            var dummyContainer = new DummySpecimenContext();
            var systemUnderTest = new RandomEnumSequenceGenerator();
            var request = typeof(Number);

            // Act
            var actualResult = systemUnderTest.Create(request, dummyContainer);

            // Assert
            actualResult.Should().BeOfType<Number>();
        }

        [Fact]
        public static void Create___Should_return_random_enum_values___When_multiple_requests_are_made_for_enum()
        {
            // Arrange
            var dummyContainer = new DummySpecimenContext();
            var systemUnderTest = new RandomEnumSequenceGenerator();
            var request = typeof(Number);
            var enumValuesCount = Enum.GetValues(typeof(Number)).Length;
            var enumValuesInOrder = Enum.GetValues(typeof(Number)).Cast<Number>();

            // Act
            var actualResult = Enumerable.Range(1, enumValuesCount).Select(_ => systemUnderTest.Create(request, dummyContainer)).Cast<Number>().ToList();

            // Assert
            actualResult.Should().NotEqual(enumValuesInOrder);
        }

        [Fact]
        public static void Create___Should_return_all_enum_values_at_least_once___When_many_requests_are_made_for_an_enum()
        {
            // Arrange
            var dummyContainer = new DummySpecimenContext();
            var systemUnderTest = new RandomEnumSequenceGenerator();
            var request = typeof(Number);
            var allEnumValues = Enum.GetValues(request).Cast<Number>();

            // Act
            var actualResult = Enumerable.Range(1, 1000).Select(_ => systemUnderTest.Create(request, dummyContainer)).Cast<Number>().ToList();

            // Assert
            foreach (var number in allEnumValues)
            {
                actualResult.Should().Contain(number);
            }
        }
    }
}
