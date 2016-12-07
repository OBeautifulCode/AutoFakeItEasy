// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AD.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy
{
    using System;
    using System.Linq;
    using System.Reflection;

    using FakeItEasy;

    /// <summary>
    /// Provides methods for generating fake objects.
    /// </summary>
    // ReSharper disable InconsistentNaming
    public static class AD
    // ReSharper restore InconsistentNaming
    {
        private static readonly MethodInfo FakeItEasyDummyMethod = typeof(A).GetMethods().Single(_ => _.Name == nameof(A.Dummy));

#pragma warning disable SA1300
        /// <summary>
        /// Gets a dummy object of the specified type.
        /// </summary>
        /// <param name="type">The type of dummy to return.</param>
        /// <returns>A dummy object of the specified type.</returns>
        /// <exception cref="ArgumentException">Dummies of the specified type can not be created.</exception>
        // ReSharper disable InconsistentNaming
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "ummy", Justification = "Attempting to get something as close to A.Dummy<T> as possible.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ummy", Justification = "Attempting to get something as close to A.Dummy<T> as possible.")]
        public static object ummy(Type type)
        // ReSharper restore InconsistentNaming
        #pragma warning restore SA1300
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var fakeItEasyGenericDummyMethod = FakeItEasyDummyMethod.MakeGenericMethod(type);
            object result = fakeItEasyGenericDummyMethod.Invoke(null, null);
            return result;
        }
    }
}
