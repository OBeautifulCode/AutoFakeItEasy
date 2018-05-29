// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReadOnlyCollectionRelay.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Ploeh.AutoFixture.Kernel;

    /// <summary>
    /// Relays a request for an <see cref="IReadOnlyCollection{T}" /> to a request for a
    /// <see cref="ReadOnlyCollection{T}"/> and returns the result.
    /// </summary>
    internal class ReadOnlyCollectionRelay : ISpecimenBuilder
    {
        /// <inheritdoc />
        public object Create(
            object request,
            ISpecimenContext context)
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
            if ((typeArguments.Length != 1) || typeof(IReadOnlyCollection<>) != requestedType.GetGenericTypeDefinition())
            {
                return new NoSpecimen();
            }

            var result = context.Resolve(typeof(ReadOnlyCollection<>).MakeGenericType(typeArguments));
            return result;
        }
    }
}