// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZeroOrNegativeInteger.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AutoFakeItEasy
{
    using System.Diagnostics;

    using Conditions;

    /// <summary>
    /// Represents a zero or negative integer.
    /// </summary>
    [DebuggerDisplay("{Value}")]
    public class ZeroOrNegativeInteger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroOrNegativeInteger"/> class.
        /// </summary>
        /// <param name="value">The value held by the <see cref="ZeroOrNegativeInteger"/> instance.</param>
        public ZeroOrNegativeInteger(int value)
        {
            Condition.Requires(value, nameof(value)).IsLessOrEqual(0);
            this.Value = value;
        }

        /// <summary>
        /// Gets the underlying <see cref="int"/> value of the instance.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Performs an explicit conversion from <see cref="ZeroOrNegativeInteger"/> to <see cref="int"/>.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static explicit operator int(ZeroOrNegativeInteger from)
        {
            return from.Value;
        }
    }
}

// ReSharper restore CheckNamespace