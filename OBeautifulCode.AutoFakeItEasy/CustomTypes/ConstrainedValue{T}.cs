// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConstrainedValue{T}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AutoFakeItEasy
{
    using System.Diagnostics;

    /// <summary>
    /// Represents a constrained value.
    /// </summary>
    /// <typeparam name="T">The type of the constrained value.</typeparam>
    [DebuggerDisplay("{Value}")]
    public abstract class ConstrainedValue<T>
        where T : struct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstrainedValue{T}"/> class.
        /// </summary>
        /// <param name="value">The value of the <see cref="ConstrainedValue{T}"/> instance.</param>
        protected ConstrainedValue(
            T value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the underlying value of the instance.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Performs an implicit conversion from a <see cref="ConstrainedValue{T}"/> to it's underlying type.
        /// </summary>
        /// <param name="from">The <see cref="ConstrainedValue{T}"/> to convert from.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator T(
            ConstrainedValue<T> from)
        {
            return from.Value;
        }
    }
}

// ReSharper restore CheckNamespace