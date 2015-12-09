// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RandomEnumSequenceGenerator.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AutoFakeItEasy
{
    using System;
    using System.Linq;

    using OBeautifulCode.Math;

    using Ploeh.AutoFixture.Kernel;

    /// <summary>
    /// Generates random enum values in a round-robin fashion.
    /// </summary>
    /// <remarks>
    /// Adapted from <a href="https://github.com/AutoFixture/AutoFixture/blob/master/Src/AutoFixture/EnumGenerator.cs"/>
    /// </remarks>
    public class RandomEnumSequenceGenerator : ISpecimenBuilder
    {
        /// <summary>
        /// Creates a new, random enum value based on a request.
        /// </summary>
        /// <param name="request">The request that describes what to create.</param>
        /// <param name="context">A context that can be used to create other specimens. Not used.</param>
        /// <returns>
        /// An enum value if appropriate; otherwise a <see cref="NoSpecimen"/> instance.
        /// </returns>
        public object Create(object request, ISpecimenContext context)
        {
            // can I handle this request?
            var t = request as Type;
            if ((t == null) || !t.IsEnum)
            {
                return new NoSpecimen();
            }

            // get random enum value
            var enumValues = Enum.GetValues(t).Cast<object>().ToList();
            var randomIndex = ThreadSafeRandom.Next(0, enumValues.Count);
            var result = enumValues[randomIndex];
            return result;
        }
    }
}

// ReSharper restore CheckNamespace