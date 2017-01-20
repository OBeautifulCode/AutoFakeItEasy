// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConstrainDummyToExclude.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AutoFakeItEasy.Test
{
#pragma warning disable SA1649 // File name must match first type name
#pragma warning disable SA1402 // File may only contain a single class
    public enum MostlyGoodStuffWithoutComparer
    {
        WorkingFromHome,

        RainyDays,

        Chocolate,

        Vacation,

        Meditation,

        FoodPoisoning,

        Bulldogs
    }

    public enum MostlyGoodStuffWithComparer
    {
        WorkingFromHome,

        RainyDays,

        Chocolate,

        Vacation,

        Meditation,

        FoodPoisoning,

        Bulldogs
    }

    public enum MostlyGoodStuffWithoutComparerReestablished
    {
        WorkingFromHome,

        RainyDays,

        Chocolate,

        Vacation,

        Meditation,

        FoodPoisoning,

        Bulldogs
    }

    public enum MostlyGoodStuffWithComparerReestablished
    {
        WorkingFromHome,

        RainyDays,

        Chocolate,

        Vacation,

        Meditation,

        FoodPoisoning,

        Bulldogs
    }

    public enum MostlyGoodStuffWithoutComparerIndirect
    {
        WorkingFromHome,

        RainyDays,

        Chocolate,

        Vacation,

        Meditation,

        FoodPoisoning,

        Bulldogs
    }

    public enum MostlyGoodStuffWithComparerIndirect
    {
        WorkingFromHome,

        RainyDays,

        Chocolate,

        Vacation,

        Meditation,

        FoodPoisoning,

        Bulldogs
    }

    public class ConstrainDummiesToExcludeIndirect
    {
        public string SomeProperty { get; set; }

        public MostlyGoodStuffWithoutComparerIndirect MostlyGoodStuffWithoutComparerIndirect { get; set; }

        public MostlyGoodStuffWithComparerIndirect MostlyGoodStuffWithComparerIndirect { get; set; }
    }

#pragma warning restore SA1402 // File may only contain a single class
#pragma warning restore SA1649 // File name must match first type name
}

// ReSharper restore CheckNamespace