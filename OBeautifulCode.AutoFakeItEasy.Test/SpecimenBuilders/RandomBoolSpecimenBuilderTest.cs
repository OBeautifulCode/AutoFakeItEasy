// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RandomBoolSpecimenBuilderTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy.Test
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoFixture.Kernel;

    using FluentAssertions;

    using Xunit;

    public static class RandomBoolSpecimenBuilderTest
    {
        [Fact]
        public static void Constructor___Should_return_an_object_that_is_assignable_to_ISpecimenBuilder___When_object_is_constructed()
        {
            // Arrange, Act
            var systemUnderTest = new AutoFakeItEasy.RandomBoolSpecimenBuilder();

            // Assert
            systemUnderTest.Should().BeAssignableTo<ISpecimenBuilder>();
        }

        [Fact]
        public static void Create___Should_return_an_object_equal_to_NoSpecimen___When_called_with_null_request()
        {
            // Arrange
            var dummyContainer = new DummySpecimenContext();
            var systemUnderTest = new AutoFakeItEasy.RandomBoolSpecimenBuilder();
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
            var systemUnderTest = new AutoFakeItEasy.RandomBoolSpecimenBuilder();
            var dummyRequest = new object();

            // Act
            var ex = Record.Exception(() => systemUnderTest.Create(dummyRequest, null));

            // Assert
            ex.Should().BeNull();
        }

        [Fact]
        public static void Create___Should_return_an_object_equal_to_NoSpecimen___When_request_is_not_a_boolean()
        {
            // Arrange
            var dummyContainer = new DummySpecimenContext();
            var systemUnderTest = new AutoFakeItEasy.RandomBoolSpecimenBuilder();
            var expectedResult = new NoSpecimen();
            var request = new object();

            // Act
            var actualResult = systemUnderTest.Create(request, dummyContainer);

            // Assert
            actualResult.Should().Be(expectedResult);
        }

        [Fact]
        public static void Create___Should_return_an_object_of_type_bool___When_request_is_for_a_boolean()
        {
            // Arrange
            var dummyContainer = new DummySpecimenContext();
            var systemUnderTest = new AutoFakeItEasy.RandomBoolSpecimenBuilder();
            var request = typeof(bool);

            // Act
            var actualResult = systemUnderTest.Create(request, dummyContainer);

            // Assert
            actualResult.Should().BeOfType<bool>();
        }

        [Fact]
        public static void Create___Should_return_random_booleans___When_multiple_requests_are_made_for_booleans()
        {
            // Arrange
            var dummyContainer = new DummySpecimenContext();
            var systemUnderTest = new AutoFakeItEasy.RandomBoolSpecimenBuilder();
            var request = typeof(bool);
            var sequentialBools1 = new List<bool> { true, false, true, false, true, false, true, false };
            var sequentialBools2 = new List<bool> { false, true, false, true, false, true, false, true };

            // Act
            var actualResult = Enumerable.Range(1, sequentialBools1.Count).Select(_ => systemUnderTest.Create(request, dummyContainer)).Cast<bool>().ToList();

            // Assert
            actualResult.Should().NotEqual(sequentialBools1);
            actualResult.Should().NotEqual(sequentialBools2);
        }

        [Fact]
        public static void Create___Should_return_both_true_and_false_at_least_once___When_many_requests_are_made_for_booleans()
        {
            // Arrange
            var dummyContainer = new DummySpecimenContext();
            var systemUnderTest = new AutoFakeItEasy.RandomBoolSpecimenBuilder();
            var request = typeof(bool);

            // Act
            var actualResult = Enumerable.Range(1, 1000).Select(_ => systemUnderTest.Create(request, dummyContainer)).Cast<bool>().ToList();

            // Assert
            actualResult.Should().Contain(true);
            actualResult.Should().Contain(false);
        }
    }
}