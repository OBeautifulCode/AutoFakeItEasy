// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PercentChangeAsDecimal.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AutoFakeItEasy
{
    using System;
    using System.Diagnostics;

    using Conditions;

    using OBeautifulCode.Math;

    /// <summary>
    /// Represents a positive integer.
    /// </summary>
    [DebuggerDisplay("{Value}")]
    public sealed class PercentChangeAsDecimal : ConstrainedValue<decimal>
    {
        /// <summary>
        /// The minimum possible percent change.
        /// </summary>
        internal const decimal MinPercentChange = -1m;

        /// <summary>
        /// The maximum possible percent change.
        /// </summary>
        internal const decimal MaxPercentChange = 3m;

        /// <summary>
        /// Initializes a new instance of the <see cref="PercentChangeAsDecimal"/> class.
        /// </summary>
        /// <param name="value">The value held by the <see cref="PercentChangeAsDecimal"/> instance.</param>
        public PercentChangeAsDecimal(decimal value)
            : base(value)
        {
            Condition.Requires(value, nameof(value)).IsGreaterOrEqual(MinPercentChange).IsLessOrEqual(MaxPercentChange);
        }

        /// <summary>
        /// Generates a valid <see cref="PercentChangeAsDecimal"/>.
        /// </summary>
        /// <returns>
        /// Returns a valid <see cref="PercentChangeAsDecimal"/>.
        /// </returns>
        internal static PercentChangeAsDecimal CreateConstrainedValue()
        {
            var minPercentChangeInThousands = Convert.ToInt32(MinPercentChange * 1000);
            var maxPercentChangeInThousands = Convert.ToInt32(MaxPercentChange * 1000);
            var randomPercentChangeInThousands = ThreadSafeRandom.Next(minPercentChangeInThousands, maxPercentChangeInThousands + 1);
            var percentChangeAsDecimal = Convert.ToDecimal(randomPercentChangeInThousands) / 1000m;
            var result = new PercentChangeAsDecimal(percentChangeAsDecimal);
            return result;
        }
    }
}

// ReSharper restore CheckNamespace