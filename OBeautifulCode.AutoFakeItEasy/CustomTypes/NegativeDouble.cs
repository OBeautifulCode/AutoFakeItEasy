// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NegativeDouble.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AutoFakeItEasy
{
    using System;
    using System.Diagnostics;

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
        public NegativeDouble(
            double value)
            : base(value)
        {
            if (value >= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "value is greater than or equal to 0");
            }
        }
    }
}

// ReSharper restore CheckNamespace