// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZeroOrNegativeInteger.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represents a zero or negative integer.
    /// </summary>
    [DebuggerDisplay("{" + nameof(Value) + "}")]
    public sealed class ZeroOrNegativeInteger : ConstrainedValue<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroOrNegativeInteger"/> class.
        /// </summary>
        /// <param name="value">The value held by the <see cref="ZeroOrNegativeInteger"/> instance.</param>
        public ZeroOrNegativeInteger(
            int value)
            : base(value)
        {
            if (value > 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "value is greater than 0");
            }
        }
    }
}