// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoFixtureBackedDummyFactory.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using FakeItEasy;

    using OBeautifulCode.Math;

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

        private static readonly MethodInfo FakeItEasyDummyMethod = typeof(A).GetMethods().Single(_ => _.Name == "Dummy");

        private static readonly HashSet<Type> AllowedAbstractTypes = new HashSet<Type>();

        private readonly MethodInfo autoFixtureCreateMethod;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoFixtureBackedDummyFactory"/> class.
        /// </summary>
        public AutoFixtureBackedDummyFactory()
        {
            this.autoFixtureCreateMethod = typeof(SpecimenFactory)
                .GetMethods()
                .Single(_ => (_.Name == "Create") && (_.GetParameters().Length == 1) && (_.GetParameters().Single().ParameterType == typeof(ISpecimenBuilder)));

            ConfigureRecursionBehavior();

            AddCustomizations();

            RegisterCustomTypes();
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
        /// <remarks>
        /// If this method is called multiple times for the same type,
        /// the most recently added creator will be used.
        /// </remarks>
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

        /// <summary>
        /// Instructs the dummy factory to use a random, concrete subclass
        /// of the specified type when making dummies of that type.
        /// </summary>
        /// <typeparam name="T">The reference type.</typeparam>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "In this case we just need the type, not a parameter of that type.")]
        public static void UseRandomConcreteSubclassForDummy<T>()
        {
            Type type = typeof(T);
            var concreteSubclasses = type.Assembly
                .GetTypes()
                .Where(_ => _.IsSubclassOf(type))
                .Where(_ => !_.IsAbstract)
                .Where(CanCreateType)
                .ToList();

            if (concreteSubclasses.Count == 0)
            {
                throw new ArgumentException("There are no concrete subclasses of " + type.Name);
            }

            if (type.IsAbstract)
            {
                AllowedAbstractTypes.Add(type);
            }

            Func<T> randomSubclassDummyCreator = () =>
                {
                    // get random subclass
                    var randomIndex = ThreadSafeRandom.Next(0, concreteSubclasses.Count);
                    var randomSubclass = concreteSubclasses[randomIndex];

                    // call the FakeItEasy A.Dummy method to create that subclass
                    MethodInfo fakeItEasyGenericDummyMethod = FakeItEasyDummyMethod.MakeGenericMethod(randomSubclass);
                    object result = fakeItEasyGenericDummyMethod.Invoke(null, null);
                    return (T)result;
                };

            AddDummyCreator(randomSubclassDummyCreator);
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

        private static void ConfigureRecursionBehavior()
        {
            // It's not AutoFixture's job to detect recursion.  Infinite recursion will cause the process to blow-up;
            // it's typically a behavior that's easy to observe/detect.  By allowing recursion we enable a swath
            // of legitimate scenarios (e.g. a tree).
            var throwingRecursionBehaviors = Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList();
            foreach (var throwingRecursionBehavior in throwingRecursionBehaviors)
            {
                Fixture.Behaviors.Remove(throwingRecursionBehavior);
            }
        }

        private static void AddCustomizations()
        {
            // fix some of AutoFixture's poor defaults - see README.md
            // ReSharper disable RedundantNameQualifier

            // this will generate numbers in the range [-32768,32768]
            Fixture.Customizations.Insert(0, new AutoFakeItEasy.RandomNumericSequenceGenerator(short.MinValue, short.MaxValue + 2));
            Fixture.Customizations.Insert(0, new AutoFakeItEasy.RandomBoolSequenceGenerator());
            Fixture.Customizations.Insert(0, new AutoFakeItEasy.RandomEnumSequenceGenerator());

            // ReSharper restore RedundantNameQualifier
        }

        private static void RegisterCustomTypes()
        {
            Fixture.Register(() => new PositiveInteger(Math.Abs(Try.Running(Fixture.Create<int>).Until(result => result != 0))));
            Fixture.Register(() => new NegativeInteger(-1 * Math.Abs(Try.Running(Fixture.Create<int>).Until(result => result != 0))));
            Fixture.Register(() => new ZeroOrPositiveInteger(Math.Abs(Fixture.Create<int>())));
            Fixture.Register(() => new ZeroOrNegativeInteger(-1 * Math.Abs(Fixture.Create<int>())));
        }

        private static bool CanCreateType(Type type)
        {
            if (type.IsInterface)
            {
                return false;
            }

            if (type.IsAbstract)
            {
                if (!AllowedAbstractTypes.Contains(type))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
