// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZeroOrNegativeDouble.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represents a zero or negative double.
    /// </summary>
    [DebuggerDisplay("{" + nameof(Value) + "}")]
    public sealed class ZeroOrNegativeDouble : ConstrainedValue<double>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroOrNegativeDouble"/> class.
        /// </summary>
        /// <param name="value">The value held by the <see cref="ZeroOrNegativeDouble"/> instance.</param>
        public ZeroOrNegativeDouble(
            double value)
            : base(value)
        {
            if (value > 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "value is greater than 0");
            }
        }
    }
}