// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtensionMethods.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Conditions;

    using FakeItEasy;

    /// <summary>
    /// Some extension methods.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Returns a reference dummy if it meets a specified condition or
        /// creates new dummies of the same type until the condition is met.
        /// </summary>
        /// <param name="referenceDummy">The reference dummy.</param>
        /// <param name="condition">A function to test the reference dummy against a condition.</param>
        /// <param name="maxAttempts">
        /// The maximum number of times to attempt to create a dummy that meets the condition, before throwing.
        /// The reference dummy is itself considered the first attempt.  If this method creates a new dummy
        /// because the condition fails on the reference dummy, that is considered the second attempt.
        /// If max attempts is zero or negative then the method tries an infinite number of times to meet the condition (DANGER!!!).
        /// Default is 100.
        /// </param>
        /// <typeparam name="T">The type of dummy.</typeparam>
        /// <returns>Returns the reference dummy if it meets the specified condition.  Otherwise, returns a new dummy that meets the condition.</returns>
        public static T ThatIs<T>(this T referenceDummy, Func<T, bool> condition, int maxAttempts = 100)
        {
            Condition.Requires(condition, nameof(condition)).IsNotNull();

            var referenceDummyType = referenceDummy?.GetType();
            string someDummiesCallName = null;
            if ((referenceDummyType != null) && referenceDummyType.IsGenericType)
            {
                var genericTypeDefinition = referenceDummyType.GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(SomeDummiesList<>))
                {
                    someDummiesCallName = nameof(Some.Dummies);
                }
                else if (genericTypeDefinition == typeof(SomeReadOnlyDummiesList<>))
                {
                    someDummiesCallName = nameof(Some.ReadOnlyDummies);
                }
            }

            int attempts = 1;
            while (!condition(referenceDummy))
            {
                if (attempts == maxAttempts)
                {
                    throw new InvalidOperationException("Unable to create a dummy that satisfies the specified condition.");
                }

                if (someDummiesCallName != null)
                {
                    var someDummiesMethod = typeof(Some).GetMethods().Single(_ => _.Name == someDummiesCallName);
                    var typeOfElementsInList = referenceDummyType.GetGenericArguments().Single();
                    var someDummiesGenericMethod = someDummiesMethod.MakeGenericMethod(typeOfElementsInList);
                    var numberOfElements = referenceDummyType.GetProperty(nameof(ISomeDummies.NumberOfElementsSpecifiedInCallToSomeDummies)).GetValue(referenceDummy);
                    var createWith = referenceDummyType.GetProperty(nameof(ISomeDummies.CreateWithSpecifiedInCallToSomeDummies)).GetValue(referenceDummy);
                    referenceDummy = (T)someDummiesGenericMethod.Invoke(null, new[] { numberOfElements, createWith });
                }
                else
                {
                    referenceDummy = A.Dummy<T>();
                }

                attempts++;
            }

            return referenceDummy;
        }

        /// <summary>
        /// Returns a reference dummy if it meets a specified condition or
        /// creates new dummies of the same type until the condition is met.
        /// This method does the exact same thing as <see cref="ThatIs{T}(T, Func{T, bool}, int)"/>.
        /// </summary>
        /// <param name="referenceDummy">The reference dummy.</param>
        /// <param name="condition">A function to test the reference dummy against a condition.</param>
        /// <param name="maxAttempts">
        /// The maximum number of times to attempt to create a dummy that meets the condition, before throwing.
        /// The reference dummy is itself considered the first attempt.  If this method creates a new dummy
        /// because the condition fails on the reference dummy, that is considered the second attempt.
        /// If max attempts is zero or negative then the method tries an infinite number of times to meet the condition (DANGER!!!).
        /// Default is 100.
        /// </param>
        /// <typeparam name="T">The type of dummy.</typeparam>
        /// <returns>Returns the reference dummy if it meets the specified condition.  Otherwise, returns a new dummy that meets the condition.</returns>
        public static T Whose<T>(this T referenceDummy, Func<T, bool> condition, int maxAttempts = 100)
            => ThatIs(referenceDummy, condition, maxAttempts);

        /// <summary>
        /// Returns a reference dummy if it is not equal to a comparison dummy or
        /// creates new dummies of the same type until a dummy is created that does not equal the comparison dummy.
        /// Uses the comparison dummy's implementation of <see cref="object.Equals(object)"/>
        /// </summary>
        /// <param name="referenceDummy">The reference dummy.</param>
        /// <param name="comparisonDummy">A dummy to compare against.</param>
        /// <param name="maxAttempts">
        /// The maximum number of times to attempt to create a dummy that is not equal to the comparison dummy, before throwing.
        /// The reference dummy is itself considered the first attempt.  If this method creates a new dummy because
        /// the comparision dummy is equal to the reference dummy, that is considered the second attempt.
        /// If max attempts is zero or negative then the method tries an infinite number of times to create a dummy
        /// that is not equal to the comparison dummy (DANGER!!!).
        /// Default is 100.
        /// </param>
        /// <typeparam name="T">The type of dummy.</typeparam>
        /// <returns>
        /// Returns the reference dummy if is not equal to the comparison dummy.
        /// Otherwise, returns a new dummy that that is not equal to the comparison dummy.
        /// </returns>
        public static T ThatIsNot<T>(this T referenceDummy, T comparisonDummy, int maxAttempts = 100)
        {
            Func<T, bool> condition = t => !Equals(t, comparisonDummy);
            var result = ThatIs(referenceDummy, condition, maxAttempts);
            return result;
        }
    }
}
