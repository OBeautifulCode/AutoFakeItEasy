// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZeroOrPositiveInteger.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AutoFakeItEasy
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represents a zero or positive integer.
    /// </summary>
    [DebuggerDisplay("{Value}")]
    public sealed class ZeroOrPositiveInteger : ConstrainedValue<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroOrPositiveInteger"/> class.
        /// </summary>
        /// <param name="value">The value held by the <see cref="ZeroOrPositiveInteger"/> instance.</param>
        public ZeroOrPositiveInteger(int value)
            : base(value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "value is less than 0");
            }
        }
    }
}

// ReSharper restore CheckNamespace