// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NegativeDouble.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AutoFakeItEasy
{
    using System.Diagnostics;

    using Conditions;

    /// <summary>
    /// Represents a negative double.
    /// </summary>
    [DebuggerDisplay("{Value}")]
    public sealed class NegativeDouble : ConstrainedValue<double>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NegativeDouble"/> class.
        /// </summary>
        /// <param name="value">The value held by the <see cref="NegativeDouble"/> instance.</param>
        public NegativeDouble(double value)
            : base(value)
        {
            Condition.Requires(value, nameof(value)).IsLessThan(0);
        }
    }
}

// ReSharper restore CheckNamespace