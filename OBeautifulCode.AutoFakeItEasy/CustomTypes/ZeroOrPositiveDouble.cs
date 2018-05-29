// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZeroOrPositiveDouble.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represents a zero or positive double.
    /// </summary>
    [DebuggerDisplay("{" + nameof(Value) + "}")]
    public sealed class ZeroOrPositiveDouble : ConstrainedValue<double>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroOrPositiveDouble"/> class.
        /// </summary>
        /// <param name="value">The value held by the <see cref="ZeroOrPositiveDouble"/> instance.</param>
        public ZeroOrPositiveDouble(
            double value)
            : base(value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "value is less than 0");
            }
        }
    }
}