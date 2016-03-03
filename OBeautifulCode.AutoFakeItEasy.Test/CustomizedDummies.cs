// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomizedDummies.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy.Test
{
    using System.Diagnostics.CodeAnalysis;

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

    public class UseCustomDummyCreatorFuncForConcreteType
    {
        public UseCustomDummyCreatorFuncForConcreteType(int value)
        {
            this.Value = value;
        }

        public int Value { get; set; }
    }

    public abstract class UseCustomDummyCreatorFuncForAbstractType
    {
        protected UseCustomDummyCreatorFuncForAbstractType(int abstractValue)
        {
            this.AbstractValue = abstractValue;
        }

        public int AbstractValue { get; set; }
    }

    public class UseCustomDummyCreatorFuncForAbstractTypeReturnedType : UseCustomDummyCreatorFuncForAbstractType
    {
        public UseCustomDummyCreatorFuncForAbstractTypeReturnedType(int abstractValue, int concreteValue)
            : base(abstractValue)
        {
            this.ConcreteValue = concreteValue;
        }

        public int ConcreteValue { get; set; }
    }

    [SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:ElementsMustAppearInTheCorrectOrder", Justification = "For testing.")]
    public interface IUseCustomDummyCreatorFuncForInterfaceType
    {
        int Value { get; set; }
    }

    public class UseCustomDummyCreatorFuncForInterfaceType : IUseCustomDummyCreatorFuncForInterfaceType
    {
        public UseCustomDummyCreatorFuncForInterfaceType(int value)
        {
            this.Value = value;
        }

        public int Value { get; set; }
    }

    public class UseMostRecentlyAddedCustomDummyCreatorFunc
    {
        public UseMostRecentlyAddedCustomDummyCreatorFunc(int value)
        {
            this.Value = value;
        }

        public int Value { get; set; }
    }
#pragma warning restore SA1402 // File may only contain a single class
#pragma warning restore SA1649 // File name must match first type name
}
