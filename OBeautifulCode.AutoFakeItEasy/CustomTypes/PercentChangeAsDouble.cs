// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PercentChangeAsDouble.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AutoFakeItEasy
{
    using System;
    using System.Diagnostics;

    using OBeautifulCode.Math;

    /// <summary>
    /// Represents a positive integer.
    /// </summary>
    [DebuggerDisplay("{Value}")]
    public sealed class PercentChangeAsDouble : ConstrainedValue<double>
    {
        /// <summary>
        /// The minimum possible percent change.
        /// </summary>
        internal const double MinPercentChange = -1d;

        /// <summary>
        /// The maximum possible percent change.
        /// </summary>
        internal const double MaxPercentChange = 3d;

        /// <summary>
        /// Initializes a new instance of the <see cref="PercentChangeAsDouble"/> class.
        /// </summary>
        /// <param name="value">The value held by the <see cref="PercentChangeAsDouble"/> instance.</param>
        public PercentChangeAsDouble(
            double value)
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
        internal static PercentChangeAsDouble CreateConstrainedValue()
        {
            var minPercentChangeInThousands = Convert.ToInt32(MinPercentChange * 1000d);
            var maxPercentChangeInThousands = Convert.ToInt32(MaxPercentChange * 1000d);
            var randomPercentChangeInThousands = ThreadSafeRandom.Next(minPercentChangeInThousands, maxPercentChangeInThousands + 1);
            var percentChangeAsDouble = Convert.ToDouble(randomPercentChangeInThousands) / 1000d;
            var result = new PercentChangeAsDouble(percentChangeAsDouble);
            return result;
        }
    }
}

// ReSharper restore CheckNamespace