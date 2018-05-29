// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RandomBoolSequenceGenerator.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy
{
    using System;

    using OBeautifulCode.Math.Recipes;

    using Ploeh.AutoFixture.Kernel;

    /// <summary>
    /// Creates random value <see langword="true"/> or <see langword="false"/>.
    /// </summary>
    /// <remarks>
    /// Adapted from: <a href="https://github.com/AutoFixture/AutoFixture/blob/master/Src/AutoFixture/RandomNumericSequenceGenerator.cs"/>.
    /// </remarks>
    public class RandomBoolSequenceGenerator : ISpecimenBuilder
    {
        /// <summary>
        /// Returns <see langword="true"/> or <see langword="false"/> randomly.
        /// </summary>
        /// <param name="request">The request that describes what to create.</param>
        /// <param name="context">Not used.</param>
        /// <returns>
        /// <see langword="true"/> or <see langword="false"/> generated randomly using <see cref="Random"/>,
        /// if <paramref name="request"/> is a request for a boolean; otherwise, a <see cref="NoSpecimen"/> instance.
        /// </returns>
        public object Create(
            object request,
            ISpecimenContext context)
        {
            if (!typeof(bool).Equals(request))
            {
                return new NoSpecimen();
            }

            return ThreadSafeRandom.Next(0, 2) == 0;
        }
    }
}