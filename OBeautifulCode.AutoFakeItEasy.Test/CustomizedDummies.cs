// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomizedDummies.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy.Test
{
#pragma warning disable SA1649 // File name must match first type name
#pragma warning disable SA1402 // File may only contain a single class
    public class CustomDummyDoesNotThrowWhenCreated
    {
        public CustomDummyDoesNotThrowWhenCreated(int value)
        {
            this.Value = value;
        }

        public int Value { get; set; }
    }

    public class CustomDummyDoesNotThrowWhenRegisteredTwice
    {
        public CustomDummyDoesNotThrowWhenRegisteredTwice(int value)
        {
            this.Value = value;
        }

        public int Value { get; set; }
    }

    public class CustomDummyUsesCustomDummyCreatorFunc
    {
        public CustomDummyUsesCustomDummyCreatorFunc(int value)
        {
            this.Value = value;
        }

        public int Value { get; set; }
    }

    public class CustomDummyUsesMostRecentlyAddedCustomDummyCreatorFunc
    {
        public CustomDummyUsesMostRecentlyAddedCustomDummyCreatorFunc(int value)
        {
            this.Value = value;
        }

        public int Value { get; set; }
    }
#pragma warning restore SA1402 // File may only contain a single class
#pragma warning restore SA1649 // File name must match first type name
}
