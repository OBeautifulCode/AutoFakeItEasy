// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZeroOrPositiveInteger.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AutoFakeItEasy
{
    using System.Diagnostics;

    using Conditions;

    /// <summary>
    /// Represents a zero or positive integer.
    /// </summary>
    [DebuggerDisplay("{Value}")]
    public sealed class ZeroOrPositiveInteger : ConstrainedInteger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroOrPositiveInteger"/> class.
        /// </summary>
        /// <param name="value">The value held by the <see cref="ZeroOrPositiveInteger"/> instance.</param>
        public ZeroOrPositiveInteger(int value)
            : base(value)
        {
            Condition.Requires(value, nameof(value)).IsGreaterOrEqual(0);
        }
    }
}

// ReSharper restore CheckNamespace