// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComparableDummies.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy.Test
{
    using System;
    using System.Collections.Generic;

#pragma warning disable SA1649 // File name must match first type name
#pragma warning disable SA1402 // File may only contain a single class
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1036:OverrideMethodsOnComparableTypes", Justification = "this is for testing only.")]
    public class ComparableIntAsObject : IComparable<ComparableIntAsObject>
    {
        public ComparableIntAsObject(
            int value)
        {
            this.Value = value;
        }

        public int Value { get; }

        public int CompareTo(
            ComparableIntAsObject other)
        {
            if (other == null)
            {
                return 1;
            }

            return this.Value.CompareTo(other.Value);
        }
    }

    public class ComparableIntAsObjectComparer : IComparer<ComparableIntAsObject>
    {
        public int Compare(
            ComparableIntAsObject x,
            ComparableIntAsObject y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }

                return -1;
            }

            return x.CompareTo(y);
        }
    }
#pragma warning restore SA1402 // File may only contain a single class
#pragma warning restore SA1649 // File name must match first type name
}