// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PositiveDouble.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AutoFakeItEasy
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represents a positive double.
    /// </summary>
    [DebuggerDisplay("{Value}")]
    public sealed class PositiveDouble : ConstrainedValue<double>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PositiveDouble"/> class.
        /// </summary>
        /// <param name="value">The value held by the <see cref="PositiveDouble"/> instance.</param>
        public PositiveDouble(
            double value)
            : base(value)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "value is less than or equal to 0");
            }
        }
    }
}

// ReSharper restore CheckNamespace