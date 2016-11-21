// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NegativeInteger.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AutoFakeItEasy
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represents a negative integer.
    /// </summary>
    [DebuggerDisplay("{Value}")]
    public sealed class NegativeInteger : ConstrainedValue<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NegativeInteger"/> class.
        /// </summary>
        /// <param name="value">The value held by the <see cref="NegativeInteger"/> instance.</param>
        public NegativeInteger(int value)
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