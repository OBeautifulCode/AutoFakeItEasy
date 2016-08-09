// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZeroOrNegativeDouble.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AutoFakeItEasy
{
    using System.Diagnostics;

    using Conditions;

    /// <summary>
    /// Represents a zero or negative double.
    /// </summary>
    [DebuggerDisplay("{Value}")]
    public sealed class ZeroOrNegativeDouble : ConstrainedValue<double>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroOrNegativeDouble"/> class.
        /// </summary>
        /// <param name="value">The value held by the <see cref="ZeroOrNegativeDouble"/> instance.</param>
        public ZeroOrNegativeDouble(double value)
            : base(value)
        {
            Condition.Requires(value, nameof(value)).IsLessOrEqual(0);
        }
    }
}

// ReSharper restore CheckNamespace