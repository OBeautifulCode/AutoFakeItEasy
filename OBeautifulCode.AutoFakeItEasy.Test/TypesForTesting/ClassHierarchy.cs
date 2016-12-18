// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClassHierarchy.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AutoFakeItEasy.Test
{
    using System;

#pragma warning disable SA1649 // File name must match first type name
#pragma warning disable SA1402 // File may only contain a single class
    public abstract class Animal
    {
        protected Animal(string name)
        {
            this.Name = name;
        }

        public string Name { get; }
    }

    public class Dog : Animal
    {
        public Dog(string name)
            : base(name)
        {
        }
    }

    public class Lion : Animal
    {
        public Lion(string name, bool isInPack)
            : base(name)
        {
            this.IsInPack = isInPack;
        }

        public bool IsInPack { get; }
    }

    public class Zebra : Animal
    {
        public Zebra(string name, DateTime born)
            : base(name)
        {
            this.Born = born;
        }

        public DateTime Born { get; }
    }

    public abstract class TwoWheelers
    {
    }

    public class Motorcycle : TwoWheelers
    {
    }

    public class Bicycle : TwoWheelers
    {
    }

    public class Scooter : TwoWheelers
    {
    }

    public class Moped : TwoWheelers
    {
    }

    public abstract class School
    {
    }

    public class Elementary : School
    {
    }

    public class University : School
    {
    }

    public class Postgraduate : School
    {
    }

    public class HighSchool : School
    {
    }

    public abstract class Wine
    {
    }

    public class Malbec : Wine
    {
    }

    public class Pinonoir : Wine
    {
    }

    public class Shiraz : Wine
    {
    }

    public abstract class Beer
    {
    }

    public class Lager : Beer
    {
    }

    public class Stout : Beer
    {
    }

    public class Ipa : Beer
    {
    }

    public abstract class Cake
    {
    }

    public class PoundCake : Cake
    {
    }

    public class StrawberryShortcake : Cake
    {
    }

    public class ChocolateCake : Cake
    {
    }

#pragma warning restore SA1402 // File may only contain a single class
#pragma warning restore SA1649 // File name must match first type name
}

// ReSharper restore CheckNamespace