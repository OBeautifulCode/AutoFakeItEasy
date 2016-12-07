// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AD.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy
{
    using System;

    /// <summary>
    /// Provides methods for generating fake objects.
    /// </summary>
    // ReSharper disable InconsistentNaming
    public static class AD
    // ReSharper restore InconsistentNaming
    {
#pragma warning disable SA1300
        /// <summary>
        /// Gets a dummy object of the specified type.
        /// </summary>
        /// <param name="typeOfDummy">The type of dummy to return.</param>
        /// <returns>A dummy object of the specified type.</returns>
        /// <exception cref="ArgumentException">Dummies of the specified type can not be created.</exception>
        // ReSharper disable InconsistentNaming
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "ummy", Justification = "Attempting to get something as close to A.Dummy<T> as possible.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ummy", Justification = "Attempting to get something as close to A.Dummy<T> as possible.")]
        public static object ummy(Type typeOfDummy)
        // ReSharper restore InconsistentNaming
        #pragma warning restore SA1300
        {
            var result = FakeItEasy.Sdk.Create.Dummy(typeOfDummy);
            return result;
        }
    }
}
