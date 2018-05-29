// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SomeReadOnlyDummiesListTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy.Test
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    using FluentAssertions;

    using OBeautifulCode.Math.Recipes;

    using Xunit;

    public static class SomeReadOnlyDummiesListTest
    {
        [Fact]
        public static void Constructor___Should_return_type_derived_from_ReadOnlyCollection_that_is_initialized_with_the_specified_list___When_called()
        {
            // Arrange
            var numberOfElements = ThreadSafeRandom.Next(int.MinValue, int.MaxValue);
            var createWith = (CreateWith)ThreadSafeRandom.Next(0, Enum.GetNames(typeof(CreateWith)).Length);
            var underlyingList = Enumerable.Range(1, ThreadSafeRandom.Next(2, 15)).Select(_ => ThreadSafeRandom.NextDouble()).ToList();

            // Act
            var systemUnderTest = new SomeReadOnlyDummiesList<double>(underlyingList, numberOfElements, createWith);

            // Assert
            systemUnderTest.Should().BeAssignableTo<ReadOnlyCollection<double>>();
            systemUnderTest.SequenceEqual(underlyingList).Should().BeTrue();
        }

        [Fact]
        public static void NumberOfElementsSpecifiedInCallToSomeDummies___Should_return_parameter_numberOfElements_passed_to_constructor___When_getting()
        {
            // Arrange
            var expectedNumberOfElements = ThreadSafeRandom.Next(int.MinValue, int.MaxValue);
            var createWith = (CreateWith)ThreadSafeRandom.Next(0, Enum.GetNames(typeof(CreateWith)).Length);
            var underlyingList = Enumerable.Range(1, ThreadSafeRandom.Next(2, 15)).Select(_ => ThreadSafeRandom.NextDouble()).ToList();
            var systemUnderTest = new SomeReadOnlyDummiesList<double>(underlyingList, expectedNumberOfElements, createWith);

            // Act
            var actualNumberOfElements = systemUnderTest.NumberOfElementsSpecifiedInCallToSomeDummies;

            // Assert
            actualNumberOfElements.Should().Be(expectedNumberOfElements);
        }

        [Fact]
        public static void CreateWithSpecifiedInCallToSomeDummies___Should_return_parameter_createWith_passed_to_constructor___When_getting()
        {
            // Arrange
            var numberOfElements = ThreadSafeRandom.Next(int.MinValue, int.MaxValue);
            var expectedCreateWith = (CreateWith)ThreadSafeRandom.Next(0, Enum.GetNames(typeof(CreateWith)).Length);
            var underlyingList = Enumerable.Range(1, ThreadSafeRandom.Next(2, 15)).Select(_ => ThreadSafeRandom.NextDouble()).ToList();
            var systemUnderTest = new SomeReadOnlyDummiesList<double>(underlyingList, numberOfElements, expectedCreateWith);

            // Act
            var actualCreateWith = systemUnderTest.CreateWithSpecifiedInCallToSomeDummies;

            // Assert
            actualCreateWith.Should().Be(expectedCreateWith);
        }
    }
}
