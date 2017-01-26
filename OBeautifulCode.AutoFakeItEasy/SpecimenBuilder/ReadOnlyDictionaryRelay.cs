// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReadOnlyDictionaryRelay.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AutoFakeItEasy
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Ploeh.AutoFixture.Kernel;

    /// <summary>
    /// Relays a request for an <see cref="IReadOnlyDictionary{TKey, TValue}" /> to a request for a
    /// <see cref="ReadOnlyDictionary{TKey, TValue}"/> and returns the result.
    /// </summary>
    internal class ReadOnlyDictionaryRelay : ISpecimenBuilder
    {
        /// <inheritdoc />
        public object Create(object request, ISpecimenContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var requestedType = request as Type;
            if (requestedType == null)
            {
                return new NoSpecimen();
            }

            var typeArguments = requestedType.GetGenericArguments();
            if ((typeArguments.Length != 2) || typeof(IReadOnlyDictionary<,>) != requestedType.GetGenericTypeDefinition())
            {
                return new NoSpecimen();
            }

            var result = context.Resolve(typeof(ReadOnlyDictionary<,>).MakeGenericType(typeArguments));
            return result;
        }
    }
}

// ReSharper restore CheckNamespace