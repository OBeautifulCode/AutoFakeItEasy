// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UtcDateTime.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy
{
    using System;
    using System.Diagnostics;

    using static System.FormattableString;

    /// <summary>
    /// Represents a <see cref="DateTime"/> in UTC.
    /// </summary>
    [DebuggerDisplay("{" + nameof(Value) + "}")]
    public sealed class UtcDateTime : ConstrainedValue<DateTime>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UtcDateTime"/> class.
        /// </summary>
        /// <param name="value">The value held by the <see cref="UtcDateTime"/> instance.</param>
        public UtcDateTime(
            DateTime value)
            : base(value)
        {
            if (value.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException(nameof(value), Invariant($"{nameof(value)}.{nameof(DateTime.Kind)} != {nameof(DateTimeKind)}.{nameof(DateTimeKind.Utc)}"));
            }
        }
    }
}