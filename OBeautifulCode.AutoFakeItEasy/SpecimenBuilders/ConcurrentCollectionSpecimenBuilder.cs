// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConcurrentCollectionSpecimenBuilder.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    using AutoFixture.Kernel;

    /// <summary>
    /// Creates types in the <see cref="System.Collections.Concurrent"/> namespace.
    /// </summary>
    public class ConcurrentCollectionSpecimenBuilder : ISpecimenBuilder
    {
        private static readonly Type BagUnboundedGenericType = typeof(ConcurrentBag<>);

        private static readonly Type DictionaryUnboundedGenericType = typeof(ConcurrentDictionary<,>);

        private static readonly Type QueueUnboundedGenericType = typeof(ConcurrentQueue<>);

        private static readonly Type StackUnboundedGenericType = typeof(ConcurrentStack<>);

        private static readonly Type[] SupportedUnboundedGenericTypes = new[] { BagUnboundedGenericType, DictionaryUnboundedGenericType, QueueUnboundedGenericType, StackUnboundedGenericType };

        /// <summary>
        /// Builds a concurrent collection of the specified type and populates it with dummies.
        /// </summary>
        /// <param name="request">The request that describes what to create.</param>
        /// <param name="context">Not used.</param>
        /// <returns>
        /// A concurrent collection of the specified type, populated with dummies.
        /// </returns>
        public object Create(
            object request,
            ISpecimenContext context)
        {
            var requestedType = request as Type;
            if (requestedType == null)
            {
                return new NoSpecimen();
            }

            if (!requestedType.IsGenericType)
            {
                return new NoSpecimen();
            }

            var unboundedGenericRequestedType = requestedType.GetGenericTypeDefinition();
            if (!SupportedUnboundedGenericTypes.Contains(unboundedGenericRequestedType))
            {
                return new NoSpecimen();
            }

            var genericArguments = requestedType.GenericTypeArguments;

            Type constructorParameterType;
            if (unboundedGenericRequestedType == BagUnboundedGenericType)
            {
                constructorParameterType = typeof(List<>).MakeGenericType(genericArguments);
            }
            else if (unboundedGenericRequestedType == DictionaryUnboundedGenericType)
            {
                constructorParameterType = typeof(Dictionary<,>).MakeGenericType(genericArguments);
            }
            else if (unboundedGenericRequestedType == QueueUnboundedGenericType)
            {
                constructorParameterType = typeof(List<>).MakeGenericType(genericArguments);
            }
            else if (unboundedGenericRequestedType == StackUnboundedGenericType)
            {
                constructorParameterType = typeof(List<>).MakeGenericType(genericArguments);
            }
            else
            {
                throw new InvalidOperationException("should never get here with this type: " + requestedType);
            }

            var constructorParameter = AD.ummy(constructorParameterType);
            var result = Activator.CreateInstance(requestedType, constructorParameter);

            return result;
        }
    }
}