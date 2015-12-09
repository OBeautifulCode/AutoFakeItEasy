﻿// --------------------------------------------------------------------------------------------------------------------
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
    public sealed class PositiveInteger : ConstrainedInteger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PositiveInteger"/> class.
        /// </summary>
        /// <param name="value">The value held by the <see cref="PositiveInteger"/> instance.</param>
        public PositiveInteger(int value)
            : base(value)
        {
            Condition.Requires(value, nameof(value)).IsGreaterThan(0);
        }
    }
}

// ReSharper restore CheckNamespace