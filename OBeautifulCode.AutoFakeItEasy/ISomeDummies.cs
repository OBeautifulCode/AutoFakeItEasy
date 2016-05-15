// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISomeDummies.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy
{
    /// <summary>
    /// Parameters specified for a call to generate some dummies.
    /// </summary>
    public interface ISomeDummies
    {
        /// <summary>
        /// Gets the number of elements in the list to generate as specified in the call to generate some dummies.
        /// </summary>
        int NumberOfElementsSpecifiedInCallToSomeDummies { get; }

        /// <summary>
        /// Gets a value that determines if and how to populate the list with nulls as specified in the call to generate some dummies."/>
        /// </summary>
        CreateWith CreateWithSpecifiedInCallToSomeDummies { get; }
    }
}
