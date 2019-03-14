// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConcurrentCollectionSpecimenBuilderTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy.Test
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    using AutoFixture.Kernel;

    using FluentAssertions;

    using Xunit;

    public static class ConcurrentCollectionSpecimenBuilderTest
    {
        [Fact]
        public static void Create___Should_return_NoSpecimen___When_parameter_request_is_null()
        {
            // Arrange
            var container = new DummySpecimenContext();
            var systemUnderTest = new ConcurrentCollectionSpecimenBuilder();

            var expected = new NoSpecimen();

            // Act
            var actual = systemUnderTest.Create(null, container);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public static void Create___Should_return_NoSpecimen___When_parameter_request_is_not_a_type()
        {
            // Arrange
            var container = new DummySpecimenContext();
            var systemUnderTest = new ConcurrentCollectionSpecimenBuilder();
            var request = new object();

            var expected = new NoSpecimen();

            // Act
            var actual = systemUnderTest.Create(request, container);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public static void Create___Should_return_NoSpecimen___When_parameter_request_is_not_a_generic_type()
        {
            // Arrange
            var container = new DummySpecimenContext();
            var systemUnderTest = new ConcurrentCollectionSpecimenBuilder();
            var request = typeof(object);

            var expected = new NoSpecimen();

            // Act
            var actual = systemUnderTest.Create(request, container);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public static void Create___Should_return_NoSpecimen___When_parameter_request_is_not_a_generic_concurrent_collection_type()
        {
            // Arrange
            var dummyContainer = new DummySpecimenContext();
            var systemUnderTest = new ConcurrentCollectionSpecimenBuilder();
            var request = typeof(Dictionary<string, string>);

            var expected = new NoSpecimen();

            // Act
            var actual = systemUnderTest.Create(request, dummyContainer);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public static void Create___Should_not_throw___When_parameter_container_is_null()
        {
            // Arrange
            var systemUnderTest = new ConcurrentCollectionSpecimenBuilder();
            var dummyRequest = typeof(ConcurrentBag<string>);

            // Act
            var ex = Record.Exception(() => systemUnderTest.Create(dummyRequest, null));

            // Assert
            ex.Should().BeNull();
        }

        [Fact]
        public static void Create___Should_return_an_object_of_type_ConcurrentBag___When_parameter_request_is_type_of_ConcurrentBag()
        {
            // Arrange
            var container = new DummySpecimenContext();
            var systemUnderTest = new ConcurrentCollectionSpecimenBuilder();
            var request = typeof(ConcurrentBag<int>);

            // Act
            var actualResult = systemUnderTest.Create(request, container);

            // Assert
            actualResult.Should().BeOfType<ConcurrentBag<int>>();
        }

        [Fact]
        public static void Create___Should_return_an_object_of_type_ConcurrentDictionary___When_parameter_request_is_type_of_ConcurrentDictionary()
        {
            // Arrange
            var container = new DummySpecimenContext();
            var systemUnderTest = new ConcurrentCollectionSpecimenBuilder();
            var request = typeof(ConcurrentDictionary<int, string>);

            // Act
            var actualResult = systemUnderTest.Create(request, container);

            // Assert
            actualResult.Should().BeOfType<ConcurrentDictionary<int, string>>();
        }

        [Fact]
        public static void Create___Should_return_an_object_of_type_ConcurrentStack___When_parameter_request_is_type_of_ConcurrentStack()
        {
            // Arrange
            var container = new DummySpecimenContext();
            var systemUnderTest = new ConcurrentCollectionSpecimenBuilder();
            var request = typeof(ConcurrentStack<Guid>);

            // Act
            var actualResult = systemUnderTest.Create(request, container);

            // Assert
            actualResult.Should().BeOfType<ConcurrentStack<Guid>>();
        }

        [Fact]
        public static void Create___Should_return_an_object_of_type_ConcurrentQueue___When_parameter_request_is_type_of_ConcurrentQueue()
        {
            // Arrange
            var container = new DummySpecimenContext();
            var systemUnderTest = new ConcurrentCollectionSpecimenBuilder();
            var request = typeof(ConcurrentQueue<Guid>);

            // Act
            var actualResult = systemUnderTest.Create(request, container);

            // Assert
            actualResult.Should().BeOfType<ConcurrentQueue<Guid>>();
        }
    }
}