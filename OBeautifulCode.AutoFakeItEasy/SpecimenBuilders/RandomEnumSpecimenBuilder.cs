// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RandomEnumSpecimenBuilder.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy
{
    using System;
    using System.Linq;

    using AutoFixture.Kernel;

    using OBeautifulCode.Collection.Recipes;
    using OBeautifulCode.Enum.Recipes;
    using OBeautifulCode.Math.Recipes;

    /// <summary>
    /// Generates random enum values.
    /// </summary>
    /// <remarks>
    /// Adapted from <a href="https://github.com/AutoFixture/AutoFixture/blob/master/Src/AutoFixture/EnumGenerator.cs"/>.
    /// </remarks>
    public class RandomEnumSpecimenBuilder : ISpecimenBuilder
    {
        /// <summary>
        /// Creates a new, random enum value based on a request.
        /// </summary>
        /// <param name="request">The request that describes what to create.</param>
        /// <param name="context">A context that can be used to create other specimens. Not used.</param>
        /// <returns>
        /// An enum value if appropriate; otherwise a <see cref="NoSpecimen"/> instance.
        /// </returns>
        public object Create(
            object request,
            ISpecimenContext context)
        {
            // can I handle this request?
            var t = request as Type;
            if ((t == null) || !t.IsEnum)
            {
                return new NoSpecimen();
            }

            Enum result;
            if (t.IsFlagsEnum())
            {
                // note: probably more efficient way to do this
                var individualFlags = t.GetIndividualFlags();
                var combinations = individualFlags.GetCombinations().ToList();
                var allPossibleValues = combinations.Select(_ => _.Aggregate((running, item) => running.BitwiseOr(item))).Distinct().ToList();
                var randomIndex = ThreadSafeRandom.Next(0, allPossibleValues.Count);
                result = allPossibleValues[randomIndex];
            }
            else
            {
                // get random enum value
                var enumValues = EnumExtensions.GetEnumValues(t).ToList();
                var randomIndex = ThreadSafeRandom.Next(0, enumValues.Count);
                result = enumValues[randomIndex];
            }

            return result;
        }
    }
}