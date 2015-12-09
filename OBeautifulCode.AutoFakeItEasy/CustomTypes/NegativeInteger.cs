// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NegativeInteger.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AutoFakeItEasy
{
    using System.Diagnostics;

    using Conditions;

    /// <summary>
    /// Represents a negative integer.
    /// </summary>
    [DebuggerDisplay("{Value}")]
    public sealed class NegativeInteger : ConstrainedInteger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NegativeInteger"/> class.
        /// </summary>
        /// <param name="value">The value held by the <see cref="NegativeInteger"/> instance.</param>
        public NegativeInteger(int value)
            : base(value)
        {
            Condition.Requires(value, nameof(value)).IsLessThan(0);
        }
    }
}

// ReSharper restore CheckNamespace