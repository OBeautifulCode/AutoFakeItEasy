// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Some.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using FakeItEasy;

    using OBeautifulCode.Math.Recipes;

    /// <summary>
    /// Provides methods for generating a lists of dummies.
    /// </summary>
    public static class Some
    {
        /// <summary>
        /// The minimum number of random elements to create when generating a list of dummies.
        /// </summary>
        internal const int MinRandomNumberOfElements = 1;

        /// <summary>
        /// The maximum number of random elements to create when generating a list of dummies.
        /// </summary>
        internal const int MaxRandomNumberOfElements = 10;

        private const double ProbabilityOfNull = .25;

        /// <summary>
        /// Gets a list of dummies of the specified type.
        /// </summary>
        /// <param name="numberOfElements">The number of elements in the list to generate.  If negative then a random number of elements between 1 and 10 will be generated.</param>
        /// <param name="createWith">Determines if and how to populate the list with nulls.  The default is to create a list with no nulls.</param>
        /// <typeparam name="T">The type of dummies to return.</typeparam>
        /// <returns>A list of dummy objects of the specified type.</returns>
        public static IList<T> Dummies<T>(
            int numberOfElements = -1,
            CreateWith createWith = CreateWith.NoNulls)
        {
            var result = new SomeDummiesList<T>(numberOfElements, createWith);
            if (numberOfElements < 0)
            {
                numberOfElements = ThreadSafeRandom.Next(MinRandomNumberOfElements, MaxRandomNumberOfElements + 1);
            }

            var type = typeof(T);
            bool isNullable = !type.IsValueType || (Nullable.GetUnderlyingType(type) != null);

            if (createWith == CreateWith.OneOrMoreNulls)
            {
                if (!isNullable)
                {
                    throw new ArgumentException("Cannot create a list of value type with one or more nulls.  Value types cannot be null.");
                }

                if (numberOfElements == 0)
                {
                    throw new ArgumentException("Cannot create a list with one or more nulls when number of elements is zero.");
                }
            }

            for (int x = 0; x < numberOfElements; x++)
            {
                if (createWith == CreateWith.NoNulls)
                {
                    result.Add(A.Dummy<T>());
                }
                else if ((createWith == CreateWith.OneOrMoreNulls) || (createWith == CreateWith.ZeroOrMoreNulls))
                {
                    var useNull = isNullable && (ThreadSafeRandom.NextDouble() <= ProbabilityOfNull);
                    result.Add(useNull ? default(T) : A.Dummy<T>());
                }
                else
                {
                    throw new NotSupportedException("This create with option is not supported: " + createWith);
                }
            }

            if (createWith == CreateWith.OneOrMoreNulls)
            {
                if (result.All(_ => _ != null))
                {
                    int randomIndex = ThreadSafeRandom.Next(0, numberOfElements);
                    result[randomIndex] = default(T);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets a list of read-only dummies of the specified type.
        /// </summary>
        /// <param name="numberOfElements">The number of elements in the read-only list to generate.  If negative then a random number of elements between 1 and 10 will be generated.</param>
        /// <param name="createWith">Determines if and how to populate the read-only list with nulls.  The default is to create a list with no nulls.</param>
        /// <typeparam name="T">The type of dummies to return.</typeparam>
        /// <returns>A list of read-only dummy objects of the specified type.</returns>
        public static IReadOnlyList<T> ReadOnlyDummies<T>(
            int numberOfElements = -1,
            CreateWith createWith = CreateWith.NoNulls)
        {
            var dummies = Dummies<T>(numberOfElements, createWith);
            var result = new SomeReadOnlyDummiesList<T>(dummies, numberOfElements, createWith);
            return result;
        }
    }
}