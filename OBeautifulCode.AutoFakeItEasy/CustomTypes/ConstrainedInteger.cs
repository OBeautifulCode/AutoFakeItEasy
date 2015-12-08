// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConstrainedInteger.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AutoFakeItEasy
{
    using System.Diagnostics;

    /// <summary>
    /// Represents a positive integer.
    /// </summary>
    [DebuggerDisplay("{Value}")]
    public abstract class ConstrainedInteger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstrainedInteger"/> class.
        /// </summary>
        /// <param name="value">The value held by the <see cref="ConstrainedInteger"/> instance.</param>
        protected ConstrainedInteger(int value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the underlying <see cref="int"/> value of the instance.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Performs an explicit conversion from <see cref="ConstrainedInteger"/> to <see cref="int"/>.
        /// </summary>
        /// <param name="from">The <see cref="ConstrainedInteger"/> to convert from.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static explicit operator int(ConstrainedInteger from)
        {
            return from.Value;
        }

        /// <summary>
        /// Converts to an <see cref="int"/> from a <see cref="ConstrainedInteger"/>.
        /// </summary>
        /// <param name="from">The <see cref="ConstrainedInteger"/> to convert from.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "int", Justification = "We have to use this exact method name to be CLS-compliant.")]
        public static int ToInt(ConstrainedInteger from)
        {
            return from.Value;
        }
    }
}

// ReSharper restore CheckNamespace