// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateWith.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy
{
    using System.Collections.Generic;

    /// <summary>
    /// Determines if and how to populate an <see cref="IList{T}"/>
    /// generated via <see cref="Some.Dummies{T}(int, CreateWith)"/> with nulls.
    /// </summary>
    public enum CreateWith
    {
        /// <summary>
        /// The resulting list should not contain any null elements.
        /// </summary>
        NoNulls,

        /// <summary>
        /// The resulting list should contain one or more null elements.
        /// </summary>
        OneOrMoreNulls,

        /// <summary>
        /// The resulting list should contain zero or more null elements.
        /// </summary>
        ZeroOrMoreNulls
    }
}
