// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InterfaceAndImplementationDummies.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AutoFakeItEasy.Test
{
#pragma warning disable SA1649 // File name must match first type name
#pragma warning disable SA1402 // File may only contain a single class

    public interface IAmAnInterfaceWithNoImplementations
    {
    }

    public interface IAmAnInterfaceThatDoesNothing
    {
    }

    public interface IAmAnInterfaceWithOnlyInterfaceImplementations : IAmAnInterfaceThatDoesNothing
    {
    }

    public interface IAmAnInterfaceWithSomeClassImplementations
    {
    }

    public interface IAmAnInterfaceWithSomeInterfaceImplementations
    {
    }

    public interface IAmAnInterfaceInterfaceImplementation1 : IAmAnInterfaceWithSomeInterfaceImplementations
    {
    }

    public interface IAmAnInterfaceWithSomeInterfaceImplementations2
    {
    }

    public interface IAmAnInterfaceWithSomeClassImplementations2 : IAmAnInterfaceWithSomeInterfaceImplementations2
    {
    }

    public class ClassInterfaceImplementation1 : IAmAnInterfaceWithSomeClassImplementations
    {
    }

    public class ClassInterfaceImplementation2 : IAmAnInterfaceWithSomeClassImplementations
    {
    }

    public class ClassInterfaceImplementation3 : IAmAnInterfaceWithSomeClassImplementations
    {
    }

    public class ClassInterfaceImplementation4 : IAmAnInterfaceWithSomeClassImplementations2
    {
    }

    public class ClassInterfaceImplementation5 : IAmAnInterfaceWithSomeClassImplementations2
    {
    }

    public class ClassInterfaceImplementation6 : IAmAnInterfaceWithSomeClassImplementations2
    {
    }
#pragma warning restore SA1402 // File may only contain a single class
#pragma warning restore SA1649 // File name must match first type name
}

// ReSharper restore CheckNamespace