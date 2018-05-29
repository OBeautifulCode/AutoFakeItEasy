// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConstrainDummyToBeOneOf.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy.Test
{
#pragma warning disable SA1649 // File name must match first type name
#pragma warning disable SA1402 // File may only contain a single class
    public enum MostlyBadStuffWithoutComparer
    {
        JunkFood,

        WalkInThePark,

        Hurricane,

        Tulips,

        Treasure,

        MeanPeople,

        Chores,
    }

    public enum MostlyBadStuffWithComparer
    {
        JunkFood,

        WalkInThePark,

        Hurricane,

        Tulips,

        Treasure,

        MeanPeople,

        Chores,
    }

    public enum MostlyBadStuffWithoutComparerReestablished
    {
        JunkFood,

        WalkInThePark,

        Hurricane,

        Tulips,

        Treasure,

        MeanPeople,

        Chores,
    }

    public enum MostlyBadStuffWithComparerReestablished
    {
        JunkFood,

        WalkInThePark,

        Hurricane,

        Tulips,

        Treasure,

        MeanPeople,

        Chores,
    }

    public enum MostlyBadStuffWithoutComparerIndirect
    {
        JunkFood,

        WalkInThePark,

        Hurricane,

        Tulips,

        Treasure,

        MeanPeople,

        Chores,
    }

    public enum MostlyBadStuffWithComparerIndirect
    {
        JunkFood,

        WalkInThePark,

        Hurricane,

        Tulips,

        Treasure,

        MeanPeople,

        Chores,
    }

    public class ConstrainDummiesToBeOneOfIndirect
    {
        public string SomeProperty { get; set; }

        public MostlyBadStuffWithoutComparerIndirect MostlyBadStuffWithoutComparerIndirect { get; set; }

        public MostlyBadStuffWithComparerIndirect MostlyBadStuffWithComparerIndirect { get; set; }
    }

#pragma warning restore SA1402 // File may only contain a single class
#pragma warning restore SA1649 // File name must match first type name
}