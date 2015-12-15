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
        private static readonly Fixture Fixture = new Fixture();

        private static readonly object FixtureLock = new object();

        private readonly MethodInfo autoFixtureCreateMethod;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoFixtureBackedDummyFactory"/> class.
        /// </summary>
        public AutoFixtureBackedDummyFactory()
        {
            this.autoFixtureCreateMethod = typeof(SpecimenFactory)
                .GetMethods()
                .Single(_ => (_.Name == "Create") && (_.GetParameters().Length == 1) && (_.GetParameters().Single().ParameterType == typeof(ISpecimenBuilder)));

            // add customizations
            Fixture.Customizations.Insert(0, new AutoFakeItEasy.RandomNumericSequenceGenerator(short.MinValue, short.MaxValue + 2));
            Fixture.Customizations.Insert(0, new AutoFakeItEasy.RandomBoolSequenceGenerator());
            Fixture.Customizations.Insert(0, new AutoFakeItEasy.RandomEnumSequenceGenerator());

            // register custom types
            Fixture.Register(() => new PositiveInteger(Math.Abs(Try.Running(Fixture.Create<int>).Until(result => result != 0))));
            Fixture.Register(() => new NegativeInteger(-1 * Math.Abs(Try.Running(Fixture.Create<int>).Until(result => result != 0))));
            Fixture.Register(() => new ZeroOrPositiveInteger(Math.Abs(Fixture.Create<int>())));
            Fixture.Register(() => new ZeroOrNegativeInteger(-1 * Math.Abs(Fixture.Create<int>())));
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

        /// <summary>
        /// Registers a function for creating a dummy of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the dummy to create.</typeparam>
        /// <param name="dummyCreatorFunc">The function to call to create the dummy.</param>
        public static void AddDummyCreator<T>(Func<T> dummyCreatorFunc)
        {
            Type type = typeof(T);
            if (!CanCreateType(type))
            {
                throw new ArgumentException("AutoFakeItEasy cannot create dummies of type " + type);
            }

            lock (FixtureLock)
            {
                Fixture.Register(dummyCreatorFunc);
            }
        }

        /// <inheritdoc />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "factory caller will ensure this is not null")]
        public bool CanCreate(Type type)
        {
            return CanCreateType(type);
        }

        /// <inheritdoc />
        public object Create(Type type)
        {
            // call the AutoFixture Create() method, lock because AutoFixture is not thread safe.
            MethodInfo autoFixtureGenericCreateMethod = this.autoFixtureCreateMethod.MakeGenericMethod(type);

            lock (FixtureLock)
            {
                object result = autoFixtureGenericCreateMethod.Invoke(null, new object[] { Fixture });
                return result;
            }
        }

        private static bool CanCreateType(Type type)
        {
            if (type.IsInterface)
            {
                return false;
            }

            if (type.IsAbstract)
            {
                return false;
            }

            return true;
        }
    }
}
