// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EqualityDummies.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy.Test
{
    using System;
    using System.Collections.Generic;

    using OBeautifulCode.Math.Recipes;

#pragma warning disable SA1649 // File name must match first type name
#pragma warning disable SA1402 // File may only contain a single class
    public class AllInstancesEqual : IEquatable<AllInstancesEqual>
    {
        public static bool operator ==(AllInstancesEqual left, AllInstancesEqual right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            return true;
        }

        public static bool operator !=(AllInstancesEqual left, AllInstancesEqual right) => !(left == right);

        public bool Equals(AllInstancesEqual other) => this == other;

        public override bool Equals(object obj) => this == (obj as AllInstancesEqual);

        public override int GetHashCode() => 0;
    }

    public class AllInstancesEqualEqualityComparer : IEqualityComparer<AllInstancesEqual>
    {
        public bool Equals(AllInstancesEqual x, AllInstancesEqual y) => x == y;

        public int GetHashCode(AllInstancesEqual obj) => obj.GetHashCode();
    }

    public class NoInstancesAreEqual : IEquatable<NoInstancesAreEqual>
    {
        public static bool operator ==(NoInstancesAreEqual left, NoInstancesAreEqual right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            return false;
        }

        public static bool operator !=(NoInstancesAreEqual left, NoInstancesAreEqual right) => !(left == right);

        public bool Equals(NoInstancesAreEqual other) => this == other;

        public override bool Equals(object obj) => this == (obj as NoInstancesAreEqual);

        public override int GetHashCode() => ThreadSafeRandom.Next();
    }

    public class NoInstancesAreEqualEqualityComparer : IEqualityComparer<NoInstancesAreEqual>
    {
        public bool Equals(NoInstancesAreEqual x, NoInstancesAreEqual y) => x == y;

        public int GetHashCode(NoInstancesAreEqual obj) => obj.GetHashCode();
    }

#pragma warning restore SA1402 // File may only contain a single class
#pragma warning restore SA1649 // File name must match first type name
}