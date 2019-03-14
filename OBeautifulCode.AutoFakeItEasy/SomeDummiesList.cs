// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SomeDummiesList.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents a <see cref="List{T}"/> generated via a call to <see cref="Some.Dummies{T}(int,AutoFakeItEasy.CreateWith)"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "'List' is a better suffix than 'Collection' in this case.")]
    public class SomeDummiesList<T> : List<T>, ISomeDummies
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SomeDummiesList{T}"/> class.
        /// </summary>
        /// <param name="numberOfElementsSpecifiedInCallToSomeDummies">The number of elements in the list to generate as specified in the call to <see cref="Some.Dummies{T}(int,AutoFakeItEasy.CreateWith)"/>.</param>
        /// <param name="createWithSpecifiedInCallToSomeDummies">Determines if and how to populate the list with nulls as specified in the call to <see cref="Some.Dummies{T}(int,AutoFakeItEasy.CreateWith)"/>.</param>
        public SomeDummiesList(
            int numberOfElementsSpecifiedInCallToSomeDummies,
            CreateWith createWithSpecifiedInCallToSomeDummies)
        {
            this.NumberOfElementsSpecifiedInCallToSomeDummies = numberOfElementsSpecifiedInCallToSomeDummies;
            this.CreateWithSpecifiedInCallToSomeDummies = createWithSpecifiedInCallToSomeDummies;
        }

        /// <inheritdoc />
        public int NumberOfElementsSpecifiedInCallToSomeDummies { get; }

        /// <inheritdoc />
        public CreateWith CreateWithSpecifiedInCallToSomeDummies { get; }
    }
}
