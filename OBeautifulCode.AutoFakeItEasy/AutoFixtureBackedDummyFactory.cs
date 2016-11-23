﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoFixtureBackedDummyFactory.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;

    using FakeItEasy;

    using OBeautifulCode.Math;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    /// <summary>
    /// A dummy factory backed by AutoFixture.
    /// </summary>
    public class AutoFixtureBackedDummyFactory : IDummyFactory
    {
        private static readonly Fixture Fixture = new Fixture();

        private static readonly object FixtureLock = new object();

        private static readonly MethodInfo FakeItEasyDummyMethod = typeof(A).GetMethods().Single(_ => _.Name == "Dummy");

        private static readonly ConcurrentDictionary<Type, object> RegisteredTypes = new ConcurrentDictionary<Type, object>();

        private static readonly ConcurrentDictionary<Type, Func<object>> ConstrainedDummyCreatorFuncsByType = new ConcurrentDictionary<Type, Func<object>>();

        private static readonly ThreadLocal<HashSet<Type>> ConstrainedDummyCreatorFuncsInUse = new ThreadLocal<HashSet<Type>>(() => new HashSet<Type>());

        private static readonly MethodInfo AutoFixtureCreateMethod =
            typeof(SpecimenFactory)
            .GetMethods()
            .Single(_ => (_.Name == "Create") && (_.GetParameters().Length == 1) && (_.GetParameters().Single().ParameterType == typeof(ISpecimenBuilder)));

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoFixtureBackedDummyFactory"/> class.
        /// </summary>
        public AutoFixtureBackedDummyFactory()
        {
            ConfigureRecursionBehavior();

            AddCustomizations();

            RegisterCustomTypes();
        }

        /// <inheritdoc />
        // ReSharper disable RedundantNameQualifier
        public Priority Priority => FakeItEasy.Priority.Default;
        // ReSharper restore RedundantNameQualifier

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
            lock (FixtureLock)
            {
                Fixture.Register(dummyCreatorFunc);
            }

            Type type = typeof(T);
            RegisteredTypes.TryAdd(type, new object());
        }

        /// <summary>
        /// Constrain dummies of the specified type to always be in a specified set of dummies.
        /// </summary>
        /// <typeparam name="T">The type of the dummy to create.</typeparam>
        /// <param name="possibleDummies">The dummies that can be returned.</param>
        public static void ConstrainDummyToBeOneOf<T>(params T[] possibleDummies)
        {
            ConstrainDummyToBeOneOf<T>(possibleDummies, null);
        }

        /// <summary>
        /// Constrain dummies of the specified type to always be in a specified set of dummies.
        /// </summary>
        /// <typeparam name="T">The type of the dummy to create.</typeparam>
        /// <param name="possibleDummies">The dummies that can be returned.</param>
        /// <param name="comparer">An equality comparer to use when comparing constructed dummies against the dummies that can be returned.</param>
        public static void ConstrainDummyToBeOneOf<T>(IEnumerable<T> possibleDummies, IEqualityComparer<T> comparer = null)
        {
            Type type = typeof(T);
            Func<object> creatorFunc = () =>
            {
                try
                {
                    T result = comparer == null
                        ? A.Dummy<T>().ThatIsIn(possibleDummies)
                        : A.Dummy<T>().ThatIsIn(possibleDummies, comparer);
                    return result;
                }
                finally
                {
                    ConstrainedDummyCreatorFuncsInUse.Value.Remove(type);
                }
            };

            ConstrainedDummyCreatorFuncsByType.AddOrUpdate(type, creatorFunc, (t, c) => creatorFunc);
        }

        /// <summary>
        /// Constrain dummies of the specified type to never be in a specified set of dummies.
        /// </summary>
        /// <typeparam name="T">The type of the dummy to create.</typeparam>
        /// <param name="possibleDummies">The dummies that cannot be returned.</param>
        public static void ConstrainDummyToExclude<T>(params T[] possibleDummies)
        {
            ConstrainDummyToExclude<T>(possibleDummies, null);
        }

        /// <summary>
        /// Constrain dummies of the specified type to never be in a specified set of dummies.
        /// </summary>
        /// <typeparam name="T">The type of the dummy to create.</typeparam>
        /// <param name="possibleDummies">The dummies that cannot be returned.</param>
        /// <param name="comparer">An equality comparer to use when comparing constructed dummies against the dummies to exclude.</param>
        public static void ConstrainDummyToExclude<T>(IEnumerable<T> possibleDummies, IEqualityComparer<T> comparer = null)
        {
            Type type = typeof(T);
            Func<object> creatorFunc = () =>
            {
                try
                {
                    T result = comparer == null
                        ? A.Dummy<T>().ThatIsNotIn(possibleDummies)
                        : A.Dummy<T>().ThatIsNotIn(possibleDummies, comparer);
                    return result;
                }
                finally
                {
                    ConstrainedDummyCreatorFuncsInUse.Value.Remove(type);
                }
            };

            ConstrainedDummyCreatorFuncsByType.AddOrUpdate(type, creatorFunc, (t, c) => creatorFunc);
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
            var result = CreateType(type);
            return result;
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
            AddDummyCreator(() => new PositiveInteger(Math.Abs(A.Dummy<int>().ThatIsNot(0))));
            AddDummyCreator(() => new NegativeInteger(-1 * Math.Abs(A.Dummy<int>().ThatIsNot(0))));
            AddDummyCreator(() => new ZeroOrPositiveInteger(Math.Abs(Fixture.Create<int>())));
            AddDummyCreator(() => new ZeroOrNegativeInteger(-1 * Math.Abs(Fixture.Create<int>())));

            AddDummyCreator(() => new PositiveDouble(Math.Abs(A.Dummy<double>().ThatIsNot(0))));
            AddDummyCreator(() => new NegativeDouble(-1d * Math.Abs(A.Dummy<double>().ThatIsNot(0))));
            AddDummyCreator(() => new ZeroOrPositiveDouble(Math.Abs(Fixture.Create<double>())));
            AddDummyCreator(() => new ZeroOrNegativeDouble(-1 * Math.Abs(Fixture.Create<double>())));

            AddDummyCreator(PercentChangeAsDouble.CreateConstrainedValue);
            AddDummyCreator(PercentChangeAsDecimal.CreateConstrainedValue);
        }

        private static bool CanCreateType(Type type)
        {
            if (RegisteredTypes.ContainsKey(type))
            {
                return true;
            }

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

        private static object CreateType(Type type)
        {
            // trying to create a constrained dummy?  these dummies are NOT registered with AutoFixture
            // otherwise it would result in infinite recursion and an OverflowException
            if (ConstrainedDummyCreatorFuncsByType.ContainsKey(type))
            {
                // has the creator func already called on this thread?
                // if so we don't want to call it again, rather we want to just drop down
                // to AutoFixture and build an unconstrained dummy.  we can let the currently
                // running creator func handle the retries needed to satisfy the constraint
                if (!ConstrainedDummyCreatorFuncsInUse.Value.Contains(type))
                {
                    ConstrainedDummyCreatorFuncsInUse.Value.Add(type);
                    var creatorFunc = ConstrainedDummyCreatorFuncsByType[type];
                    var result = creatorFunc();
                    return result;
                }
            }

            // call the AutoFixture Create() method, lock because AutoFixture is not thread safe.
            MethodInfo autoFixtureGenericCreateMethod = AutoFixtureCreateMethod.MakeGenericMethod(type);

            lock (FixtureLock)
            {
                object result = autoFixtureGenericCreateMethod.Invoke(null, new object[] { Fixture });
                return result;
            }
        }
    }
}
