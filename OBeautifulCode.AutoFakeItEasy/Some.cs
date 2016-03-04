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

    using Conditions;

    using FakeItEasy;

    using OBeautifulCode.Math;

    /// <summary>
    /// Provides methods for generating a lists of dummies.
    /// </summary>
    public static class Some
    {
        private const double ProbabilityOfNull = .25;

        /// <summary>
        /// Gets a list of dummies of the specified type.
        /// </summary>
        /// <param name="numberOfElements">The number of elements in the list to generate.  Must be &gt;= 0.  Default is 3.</param>
        /// <param name="createWith">Determines if and how to populate the list with nulls.  The default is to create a list with no nulls.</param>
        /// <typeparam name="T">The type of dummies to return.</typeparam>
        /// <returns>A list of dummy objects of the specified type.</returns>
        public static IList<T> Dummies<T>(int numberOfElements = 3, CreateWith createWith = CreateWith.NoNulls)
        {
            Condition.Requires(numberOfElements, nameof(numberOfElements)).IsGreaterOrEqual(0);

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

            var result = new List<T>(numberOfElements);
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
    }
}
