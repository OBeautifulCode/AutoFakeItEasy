// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZeroOrPositiveInteger.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AutoFakeItEasy
{
    using System.Diagnostics;

    using Conditions;

    /// <summary>
    /// Represents a zero or positive integer.
    /// </summary>
    [DebuggerDisplay("{Value}")]
    public class ZeroOrPositiveInteger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroOrPositiveInteger"/> class.
        /// </summary>
        /// <param name="value">The value held by the <see cref="ZeroOrPositiveInteger"/> instance.</param>
        public ZeroOrPositiveInteger(int value)
        {
            Condition.Requires(value, nameof(value)).IsGreaterOrEqual(0);
            this.Value = value;
        }

        /// <summary>
        /// Gets the underlying <see cref="int"/> value of the instance.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Performs an explicit conversion from <see cref="ZeroOrPositiveInteger"/> to <see cref="int"/>.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static explicit operator int(ZeroOrPositiveInteger from)
        {
            return from.Value;
        }
    }
}

// ReSharper restore CheckNamespace