// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SomeDummiesListTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy.Test
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using OBeautifulCode.Math.Recipes;

    using Xunit;

    public static class SomeDummiesListTest
    {
        [Fact]
        public static void Constructor___Should_return_type_derived_from_List_that_is_empty___When_called()
        {
            // Arrange
            var numberOfElements = ThreadSafeRandom.Next(int.MinValue, int.MaxValue);
            var createWith = (CreateWith)ThreadSafeRandom.Next(0, Enum.GetNames(typeof(CreateWith)).Length);

            // Act
            var systemUnderTest = new SomeDummiesList<double>(numberOfElements, createWith);

            // Assert
            systemUnderTest.Should().BeAssignableTo<List<double>>();
            systemUnderTest.Should().BeEmpty();
        }

        [Fact]
        public static void NumberOfElementsSpecifiedInCallToSomeDummies___Should_return_parameter_numberOfElements_passed_to_constructor___When_getting()
        {
            // Arrange
            var expectedNumberOfElements = ThreadSafeRandom.Next(int.MinValue, int.MaxValue);
            var createWith = (CreateWith)ThreadSafeRandom.Next(0, Enum.GetNames(typeof(CreateWith)).Length);
            var systemUnderTest = new SomeDummiesList<double>(expectedNumberOfElements, createWith);

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
            var systemUnderTest = new SomeDummiesList<double>(numberOfElements, expectedCreateWith);

            // Act
            var actualCreateWith = systemUnderTest.CreateWithSpecifiedInCallToSomeDummies;

            // Assert
            actualCreateWith.Should().Be(expectedCreateWith);
        }
    }
}
