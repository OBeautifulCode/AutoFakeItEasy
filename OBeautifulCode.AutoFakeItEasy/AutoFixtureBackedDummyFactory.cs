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

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    using Spritely.Redo;

    /// <summary>
    /// A dummy factory backed by AutoFixture.
    /// </summary>
    public class AutoFixtureBackedDummyFactory : IDummyFactory
    {
        private readonly Fixture fixture = new Fixture();

        private readonly object fixtureLock = new object();

        private readonly MethodInfo autoFixtureCreateMethod;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoFixtureBackedDummyFactory"/> class.
        /// </summary>
        public AutoFixtureBackedDummyFactory()
        {
            this.autoFixtureCreateMethod = typeof(SpecimenFactory)
                .GetMethods()
                .Single(_ => (_.Name == "Create") && (_.GetParameters().Length == 1) && (_.GetParameters().Single().ParameterType == typeof(ISpecimenBuilder)));

            // defined in this project:
            this.fixture.Customizations.Insert(0, new AutoFakeItEasy.RandomNumericSequenceGenerator(short.MinValue, short.MaxValue + 2));
            this.fixture.Customizations.Insert(0, new RandomBoolSequenceGenerator());
            this.fixture.Customizations.Insert(0, new RandomEnumSequenceGenerator());

            // custom types
            this.fixture.Register(() => new PositiveInteger(Math.Abs(Try.Running(this.fixture.Create<int>).Until(result => result != 0))));
            this.fixture.Register(() => new NegativeInteger(-1 * Math.Abs(Try.Running(this.fixture.Create<int>).Until(result => result != 0))));
            this.fixture.Register(() => new ZeroOrPositiveInteger(Math.Abs(this.fixture.Create<int>())));
            this.fixture.Register(() => new ZeroOrNegativeInteger(-1 * Math.Abs(this.fixture.Create<int>())));
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
            // call the AutoFixture Create() method, lock because AutoFixture is not thread safe.
            MethodInfo autoFixtureGenericCreateMethod = this.autoFixtureCreateMethod.MakeGenericMethod(type);

            lock (this.fixtureLock)
            {
                object result = autoFixtureGenericCreateMethod.Invoke(null, new object[] { this.fixture });
                return result;
            }
        }
    }
}
