// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DummySpecimenContext.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AutoFakeItEasy.Test
{
    using System;

    using Ploeh.AutoFixture.Kernel;

    internal class DummySpecimenContext : ISpecimenContext
    {
        public DummySpecimenContext()
        {
            this.OnResolve = r => null;
        }

        internal Func<object, object> OnResolve { get; set; }

        public object Resolve(object request)
        {
            return this.OnResolve(request);
        }
    }
}

// ReSharper restore CheckNamespace