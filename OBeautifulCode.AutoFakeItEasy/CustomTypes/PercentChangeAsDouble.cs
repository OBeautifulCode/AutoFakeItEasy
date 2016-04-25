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

    using Conditions;

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
        public PercentChangeAsDouble(double value)
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