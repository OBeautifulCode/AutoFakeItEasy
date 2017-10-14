// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RandomNumericSequenceGenerator.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AutoFakeItEasy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using OBeautifulCode.Math.Recipes;

    using Ploeh.AutoFixture.Kernel;

    /// <summary>
    /// Creates a sequence of random, unique numbers.
    /// </summary>
    public class RandomNumericSequenceGenerator : ISpecimenBuilder
    {
        private readonly long inclusiveLowerLimit;
        private readonly long exclusiveUpperLimit;

        private HashSet<long> numbersUsed = new HashSet<long>();

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomNumericSequenceGenerator" /> class.
        /// </summary>
        /// <param name="inclusiveLowerLimit">The lower limit.</param>
        /// <param name="exclusiveUpperLimit">The upper limit.</param>
        public RandomNumericSequenceGenerator(
            long inclusiveLowerLimit,
            long exclusiveUpperLimit)
        {
            if (inclusiveLowerLimit >= exclusiveUpperLimit)
            {
                throw new ArgumentOutOfRangeException(nameof(inclusiveLowerLimit), "inclusive lower limit is greater than or equal to exclusive upper limit");
            }

            this.inclusiveLowerLimit = inclusiveLowerLimit;
            this.exclusiveUpperLimit = exclusiveUpperLimit;
        }

        /// <summary>
        /// Creates a random number.
        /// </summary>
        /// <param name="request">The request that describes what to create.</param>
        /// <param name="context">A context that can be used to create other specimens.</param>
        /// <returns>
        /// The next random number in a sequence, if <paramref name="request"/> is a request
        /// for a numeric value; otherwise, a <see cref="NoSpecimen"/> instance.
        /// </returns>
        public object Create(
            object request,
            ISpecimenContext context)
        {
            var type = request as Type;
            if (type == null)
            {
                return new NoSpecimen();
            }

            return this.CreateRandom(type);
        }

        private static long RandomLong(
            long inclusiveMin,
            long exclusiveMax)
        {
            byte[] buffer = new byte[8];
            ThreadSafeRandom.NextBytes(buffer);
            long longRand = BitConverter.ToInt64(buffer, 0);
            var result = Math.Abs(longRand % (exclusiveMax - inclusiveMin)) + inclusiveMin;
            return result;
        }

        private object CreateRandom(
            Type request)
        {
            switch (Type.GetTypeCode(request))
            {
                case TypeCode.Byte:
                    return (byte)this.GetNextRandom();
                case TypeCode.Decimal:
                    return (decimal)this.GetNextRandom();
                case TypeCode.Double:
                    return (double)this.GetNextRandom();
                case TypeCode.Int16:
                    return (short)this.GetNextRandom();
                case TypeCode.Int32:
                    return (int)this.GetNextRandom();
                case TypeCode.Int64:
                    return this.GetNextRandom();
                case TypeCode.SByte:
                    return (sbyte)this.GetNextRandom();
                case TypeCode.Single:
                    return (float)this.GetNextRandom();
                case TypeCode.UInt16:
                    return (ushort)this.GetNextRandom();
                case TypeCode.UInt32:
                    return (uint)this.GetNextRandom();
                case TypeCode.UInt64:
                    return (ulong)this.GetNextRandom();
                default:
                    return new NoSpecimen();
            }
        }

        private long GetNextRandom()
        {
            long randomNumber;

            do
            {
                randomNumber = RandomLong(this.inclusiveLowerLimit, this.exclusiveUpperLimit);
            }
            while (this.numbersUsed.Contains(randomNumber));

            this.numbersUsed.Add(randomNumber);

            if (this.numbersUsed.LongCount() == (this.exclusiveUpperLimit - this.inclusiveLowerLimit))
            {
                this.numbersUsed = new HashSet<long>();
            }

            return randomNumber;
        }
    }
}

// ReSharper restore CheckNamespace