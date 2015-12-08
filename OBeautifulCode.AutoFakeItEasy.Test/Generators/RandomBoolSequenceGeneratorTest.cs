// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RandomBoolSequenceGeneratorTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
namespace OBeautifulCode.AutoFakeItEasy.Test
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Ploeh.AutoFixture.Kernel;

    using Xunit;

    /// <summary>
    /// Tests the <see cref="RandomBoolSequenceGenerator"/> class.
    /// </summary>
    public static class RandomBoolSequenceGeneratorTest
    {
        [Fact]
        public static void Constructor___When_object_is_constructed__Returns_an_object_that_is_assignable_to_ISpecimenBuilder()
        {
            // Arrange, Act
            var systemUnderTest = new RandomBoolSequenceGenerator();

            // Assert
            systemUnderTest.Should().BeAssignableTo<ISpecimenBuilder>();
        }

        [Fact]
        public static void Create___When_called_with_null_request___Returns_an_object_equal_to_NoSpecimen()
        {
            // Arrange
            var dummyContainer = new DummySpecimenContext();
            var systemUnderTest = new RandomBoolSequenceGenerator();
            var expectedResult = new NoSpecimen();

            // Act
            var actualResult = systemUnderTest.Create(null, dummyContainer);

            // Assert
            actualResult.Should().Be(expectedResult);
        }

        [Fact]
        public static void Create___When_called_with_null_container___Does_not_throw()
        {
            // Arrange
            var systemUnderTest = new RandomBoolSequenceGenerator();
            var dummyRequest = new object();

            // Act
            var ex = Record.Exception(() => systemUnderTest.Create(dummyRequest, null));

            // Assert
            ex.Should().BeNull();
        }

        [Fact]
        public static void Create___When_request_is_not_a_boolean___Returns_an_object_equal_to_NoSpecimen()
        {
            // Arrange
            var dummyContainer = new DummySpecimenContext();
            var systemUnderTest = new RandomBoolSequenceGenerator();
            var expectedResult = new NoSpecimen();
            var request = new object();

            // Act
            var actualResult = systemUnderTest.Create(request, dummyContainer);

            // Assert
            actualResult.Should().Be(expectedResult);
        }

        [Fact]
        public static void Create___When_request_is_for_a_boolean___Returns_an_object_of_type_bool()
        {
            // Arrange
            var dummyContainer = new DummySpecimenContext();
            var systemUnderTest = new RandomBoolSequenceGenerator();
            var request = typeof(bool);

            // Act
            var actualResult = systemUnderTest.Create(request, dummyContainer);

            // Assert
            actualResult.Should().BeOfType<bool>();
        }

        [Fact]
        public static void Create___When_multiple_requests_are_made_for_booleans___Returns_random_booleans()
        {
            // Arrange
            var dummyContainer = new DummySpecimenContext();
            var systemUnderTest = new RandomBoolSequenceGenerator();
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
        public static void Create___When_many_requests_are_made_for_booleans___Returns_both_true_and_false_at_least_once()
        {
            // Arrange
            var dummyContainer = new DummySpecimenContext();
            var systemUnderTest = new RandomBoolSequenceGenerator();
            var request = typeof(bool);

            // Act
            var actualResult = Enumerable.Range(1, 1000).Select(_ => systemUnderTest.Create(request, dummyContainer)).Cast<bool>().ToList();

            // Assert
            actualResult.Should().Contain(true);
            actualResult.Should().Contain(false);
        }
    }
}

// ReSharper restore InconsistentNaming
// ReSharper restore CheckNamespace