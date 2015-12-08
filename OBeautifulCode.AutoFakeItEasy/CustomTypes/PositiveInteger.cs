// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PositiveInteger.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AutoFakeItEasy
{
    using System.Diagnostics;

    using Conditions;

    /// <summary>
    /// Represents a positive integer.
    /// </summary>
    [DebuggerDisplay("{Value}")]
    public class PositiveInteger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PositiveInteger"/> class.
        /// </summary>
        /// <param name="value">The value held by the <see cref="PositiveInteger"/> instance.</param>
        public PositiveInteger(int value)
        {
            Condition.Requires(value, nameof(value)).IsGreaterThan(0);
            this.Value = value;
        }

        /// <summary>
        /// Gets the underlying <see cref="int"/> value of the instance.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Performs an explicit conversion from <see cref="PositiveInteger"/> to <see cref="int"/>.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static explicit operator int(PositiveInteger from)
        {
            return from.Value;
        }
    }
}

// ReSharper restore CheckNamespace