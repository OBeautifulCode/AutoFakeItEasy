// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PercentChangeAsDecimal.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy
{
    using System;
    using System.Diagnostics;

    using OBeautifulCode.Math.Recipes;

    /// <summary>
    /// Represents a positive integer.
    /// </summary>
    [DebuggerDisplay("{" + nameof(Value) + "}")]
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
        public PercentChangeAsDecimal(
            decimal value)
            : base(value)
        {
            if (value < MinPercentChange)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "value is less than the minimum possible percent change");
            }

            if (value > MaxPercentChange)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "value is greater than the maximum possible percent change");
            }
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