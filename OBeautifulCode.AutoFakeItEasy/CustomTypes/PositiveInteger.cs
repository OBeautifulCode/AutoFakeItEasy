// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PositiveInteger.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AutoFakeItEasy
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represents a positive integer.
    /// </summary>
    [DebuggerDisplay("{Value}")]
    public sealed class PositiveInteger : ConstrainedValue<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PositiveInteger"/> class.
        /// </summary>
        /// <param name="value">The value held by the <see cref="PositiveInteger"/> instance.</param>
        public PositiveInteger(int value)
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