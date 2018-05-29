// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SomeReadOnlyDummiesList.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represents a <see cref="ReadOnlyCollection{T}"/> generated via a call to <see cref="Some.ReadOnlyDummies{T}(int,AutoFakeItEasy.CreateWith)"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "'List' is a better suffix than 'Collection' in this case.")]
    public class SomeReadOnlyDummiesList<T> : ReadOnlyCollection<T>, ISomeDummies
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SomeReadOnlyDummiesList{T}"/> class.
        /// </summary>
        /// <param name="list">The list to wrap.</param>
        /// <param name="numberOfElementsSpecifiedInCallToSomeDummies">The number of elements in the list to generate as specified in the call to <see cref="Some.ReadOnlyDummies{T}(int,AutoFakeItEasy.CreateWith)"/>.</param>
        /// <param name="createWithSpecifiedInCallToSomeDummies">Determines if and how to populate the list with nulls as specified in the call to <see cref="Some.ReadOnlyDummies{T}(int,AutoFakeItEasy.CreateWith)"/>.</param>
        public SomeReadOnlyDummiesList(
            IList<T> list,
            int numberOfElementsSpecifiedInCallToSomeDummies,
            CreateWith createWithSpecifiedInCallToSomeDummies)
            : base(list)
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
