// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZeroOrNegativeInteger.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AutoFakeItEasy
{
    using System.Diagnostics;

    using Conditions;

    /// <summary>
    /// Represents a zero or negative integer.
    /// </summary>
    [DebuggerDisplay("{Value}")]
    public sealed class ZeroOrNegativeInteger : ConstrainedValue<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroOrNegativeInteger"/> class.
        /// </summary>
        /// <param name="value">The value held by the <see cref="ZeroOrNegativeInteger"/> instance.</param>
        public ZeroOrNegativeInteger(int value)
            : base(value)
        {
            value.Requires(nameof(value)).IsLessOrEqual(0);
        }
    }
}

// ReSharper restore CheckNamespace