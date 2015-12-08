// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NegativeInteger.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AutoFakeItEasy
{
    using System.Diagnostics;

    using Conditions;

    /// <summary>
    /// Represents a negative integer.
    /// </summary>
    [DebuggerDisplay("{Value}")]
    public class NegativeInteger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NegativeInteger"/> class.
        /// </summary>
        /// <param name="value">The value held by the <see cref="NegativeInteger"/> instance.</param>
        public NegativeInteger(int value)
        {
            Condition.Requires(value, nameof(value)).IsLessThan(0);
            this.Value = value;
        }

        /// <summary>
        /// Gets the underlying <see cref="int"/> value of the instance.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Performs an explicit conversion from <see cref="NegativeInteger"/> to <see cref="int"/>.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static explicit operator int(NegativeInteger from)
        {
            return from.Value;
        }
    }
}

// ReSharper restore CheckNamespace