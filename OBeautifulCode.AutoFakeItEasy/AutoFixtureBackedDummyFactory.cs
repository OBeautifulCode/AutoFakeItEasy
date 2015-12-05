// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoFixtureBackedDummyFactory.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy
{
    using System;
    using System.Linq;
    using System.Reflection;

    using FakeItEasy;

    using OBeautifulCode.Math;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    /// <summary>
    /// A dummy factory backed by AutoFixture.
    /// </summary>
    public class AutoFixtureBackedDummyFactory : IDummyFactory
    {
        private readonly Fixture fixture = new Fixture();

        private readonly MethodInfo autoFixtureCreateMethod;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoFixtureBackedDummyFactory"/> class.
        /// </summary>
        public AutoFixtureBackedDummyFactory()
        {
            this.autoFixtureCreateMethod = typeof(SpecimenFactory)
                .GetMethods()
                .Single(_ => (_.Name == "Create") && (_.GetParameters().Length == 1) && (_.GetParameters().Single().ParameterType == typeof(ISpecimenBuilder)));
        }

        /// <inheritdoc />
        public int Priority => int.MinValue;

        /// <summary>
        /// Loads this factory in the app domain, which makes it
        /// visible to FakeItEasy's extension point scanner.
        /// </summary>
        public static void LoadInAppDomain()
        {
        }

        /// <inheritdoc />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "factory caller will ensure this is not null")]
        public bool CanCreate(Type type)
        {
            if (type.IsInterface)
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        public object Create(Type type)
        {
            int timesToCallAutoFixtureCreateMethod = 1;

            if (type.IsEnum)
            {
                // autofixture creates enum values sequentially, this makes it random
                var enumCount = Enum.GetNames(type).Length;
                timesToCallAutoFixtureCreateMethod = ThreadSafeRandom.Next(1, enumCount);
            }
            else if (type == typeof(bool))
            {
                // autofixture creates bools sequentially, this makes it random
                timesToCallAutoFixtureCreateMethod = ThreadSafeRandom.Next(1, 10);
            }

            MethodInfo autoFixtureGenericCreateMethod = this.autoFixtureCreateMethod.MakeGenericMethod(type);

            object result = null;
            for (int i = 0; i < timesToCallAutoFixtureCreateMethod; i++)
            {
                result = autoFixtureGenericCreateMethod.Invoke(null, new object[] { this.fixture });
            }

            return result;
        }
    }
}
