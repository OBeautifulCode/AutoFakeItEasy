[![Build status](https://ci.appveyor.com/api/projects/status/c92mt9ckaewlyl6m?svg=true)](https://ci.appveyor.com/project/SurajGupta/obeautifulcode-autofakeiteasy)

[![Nuget status](https://img.shields.io/nuget/v/OBeautifulCode.AutoFakeItEasy.svg)](https://www.nuget.org/packages/OBeautifulCode.AutoFakeItEasy)  OBeautifulCode.AutoFakeItEasy

AutoFakeItEasy
==============

Overview
--------
- This package makes [FakeItEasy] Dummies actually useful by wiring-them-up to real-looking, random test data.
- By default, the following call will always return 0: `A.Dummy<int>()`
- With AutoFakeItEasy, that same call will return a **random integer**.
- This enables your unit tests to explore more space.  Like this:

```c#
public void BuildShips___Should_return_empty_collection_of_ships___When_number_of_ships_is_zero_or_negative()
{
    // Arrange
    var systemUnderTest = new ShipMaker();
    int numberOfShips = A.Dummy<ZeroOrNegativeInteger>();  // more on this type later...

    // Act
    var result = systemUnderTest.BuildShips(numberOfShips);

    // Assert
    result.Should().BeEmpty();
}
```

- In a broad set of cases, you can rely solely on `A.Fake<T>()` and `A.Dummy<T>()` for creating and configuring the objects you need to test a system.  This uniformity in the grammar leads to cleaner, more readable unit tests.
- This repurposing likely violates the strict definition of a dummy - something that is required to make a call but is not used.  I believe this narrow definition/behavior has limited utility.  [FakeItEasy] has tried to simplify the world by unifying stubs and mocks under Fakes.  I am further simplifying it, by unifying dummies, test data, and anonymous variables under Dummies.
-  I define a dummy as a real object that contains random but reasonable/real-like data, where there is no need to configure the object's behavior.  The *system under test* can reliably use a dummy to perform some operation.  *I* can use that same dummy to formulate my expectations (e.g. "Should_return_empty_collection_of_ships") after the operation has been performed.
-  I use [Autofixture] to create the dummies - it's a powerful library for creating test data.
-  So why not use [Autofixture] directly?  In some cases you should - Autofixture is ridiculously flexible.   However...
    -  You have to new-up a fixture object with each test.  This is an extra line of code.  What's more problematic is that it limits you to a small range of test-data space.  For example, `Enums` will always be created in sequential order.  So unless you are new-ing up lots of Enums in a test (which is probably a smell), you will always test the same `Enum` values.
    -  OR you have to maintain an instance-level or static/app-domain-wide Fixture object.  Autofixture is not thread-safe, so you'll have to deal with that.
    -  numeric types such as ints won't include zero or negative numbers unless you customize AutoFixture.
    -  It's less readable:

```c#
// using FakeItEasy + AutoFixture
var fixture = new Fixture();
var creditCardGateway = A.Fake<ICanChargeCreditCards>();
var creditCard = fixture.Create<CreditCard>();
order.Complete(creditCardGateway, creditCard);

// using FakeItEasy + AutoFakeItEasy
var creditCardGateway = A.Fake<ICanChargeCreditCards>();
var creditCard = A.Dummy<CreditCard>();
order.Complete(creditCardGateway, creditCard);
```

Useful Types
------------
AutoFakeItEasy supplies these useful types.  They implicitly convert to an `int` (or explicitly convert via a cast to `int`):

- `PositiveInteger` - a positive integer
- `ZeroOrPositiveInteger` - a positive integer or zero
- `NegativeInteger` - a negative integer
- `ZeroOrNegativeInteger` - a negative integer or zero

This makes it easy to create random, but constrained integers.

These are similar to the types above, but for doubles:

- `PositiveDouble` - a positive double
- `ZeroOrPositiveDouble` - a positive double or zero
- `NegativeDouble` - a negative double
- `ZeroOrNegativeDouble` - a negative double or zero

These useful types are also supported, with implicit and explicit conversions available to the underlying type:

- `PercentChangeAsDecimal` - Percentage change between -100.0% and 300.0% expressed as a `decimal`.  Calls to `A.Dummy<PercentChangeAsDecimal>` result in values >= -1m and <= 3m, with a scale of 1 (1 digit to the right of the decimal point).  For example, a result of 1.234m represents 123.4%.
- `PercentChangeAsDouble` - Same as `PercentChangeAsDecimal` except that the underlying type is a `double`.


Custom Dummy Creation
---------------------
You can customize how `A.Dummy<T>()` creates specific types as such:

```c#
AutoFixtureBackedDummyFactory.AddDummyCreator(() => new MyPoco());

AutoFixtureBackedDummyFactory.AddDummyCreator(() => 
                              {
                                  var randomInt = A.Dummy<int>();
                                  var result = new MyObjectWithConstructor(randomInt);
                                  return result;
                              });
```

`AddDummyCreator<T>` takes one parameter of type `Func<T>`, so you can be as creative as needed for constructing and configuring instances of the type.

To ensure that your customization is used throughout your unit tests, add a [Dummy Factory] as such:

```c#
namespace Your.Namespace
{
    using System;

    using FakeItEasy;

    using OBeautifulCode.AutoFakeItEasy;

    public class DummyFactory : IDummyFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DummyFactory"/> class.
        /// </summary>
        public DummyFactory()
        {
            AutoFixtureBackedDummyFactory.AddDummyCreator(() => new MyPoco());
        }

        /// <inheritdoc />
        public int Priority => FakeItEasy.Priority.Default;

        /// <inheritdoc />
        public bool CanCreate(Type type)
        {
            return false;
        }

        /// <inheritdoc />
        public object Create(Type type)
        {
            return null;
        }
    }
}
```

Note that this is a bit of an overkill.  Once created, the dummy factory does nothing useful.  We only care about the code in the constructor.  Putting that code in the constructor of an implementation of `IDummyFactory` guarantees that it will be called before any unit tests are run.  Feel free to use another approach to achieve the same goal.


Custom Dummy Creation - Constraints
-----------------------------------
`AddDummyCreator<T>` is a flexible way customize dummies because it's it takes a `Func<T>`.  

However, you can't call `A.Dummy<T>` within `AddDummyCreator<T>`, for the same `T`.

When would you want to do that?

Let's say you always want to constrain an enum to a set of curated values.

```c#
 // more on ThatIsIn in the "Constrained Dummies" section later
AutoFixtureBackedDummyFactory.AddDummyCreator(() => 
                              {
                                  var month = A.Dummy<Month>().ThatIsIn(new[] { Month.December, Month.January, Month.February });
                                  return month;
                              });
```

This will unfortunately throw an `OverflowException` because of infinite recursion.  `A.Dummy<Month>` will call the lambda, which will call `A.Dummy<Month>`, and so on.

Try this...

```c#
AutoFixtureBackedDummyFactory.ConstrainDummyToBeOneOf(Month.December, Month.January, Month.February)
AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(Fruit.Strawberry, Fruit.Blueberry, Fruit.Raspberry)
```

- If you ALWAYS want a type to be contained within, or not contained within some set of values, use `ConstrainDummyToBeOneOf()` or `ConstrainDummyToExclude`
- If you want a single call to `A.Dummy<T>` to return a value that is contained within, or not contained within some set of values, see *Constrained Dummys* below.  


Some.Dummies\<T>
---------------
A common use-case is the need for an `ICollection` or `IEnumerable` or `IList` of dummies.  Use this:

```c#
var doubles = Some.Dummies<double>();
```

- We CAN call `A.Dummy<List<double>>()`, but we have no control over how that's created.  AutoFixture is going to create a `List<double>` with 3 elements, none of which will be null, always.  That's dangerous because its all-too-easy to write unit tests that *implicitly* depend on this default.  The *explicit* reading of that line of code is, "I want a dummy list of doubles."  That's it.  The proper thing for the system to do is return a list with a random number of elements to maintain consistentcy with our definition of a Dummy.
- We CANNOT call `A.Dummy<IList<double>>()` because AutoFakeItEasy doesn't support interface types (see "How It Works" below.).  That's unfortunate because the interface type (`IList<T>`) is more flexible than a concrete type (`List<T>`).

Enter `Some.Dummies<T>()`

By default, this **returns an** `IList<T>` **with a random number of elements between 1 and 10 inclusive**.

An [IList\<T>] is flexible return type because it's also an `ICollection<T>`, `IEnumerable<T>`, and `IEnumerable`.  It covers a broad set of use cases.

Here's the full method signature for `Some.Dummies<T>()`

    IList<T> Dummies<T>(int numberOfElements = -1, CreateWith createWith = CreateWith.NoNulls)

- We can specify the number of elements in the result using the `numberOfElements` parameter.  A negative number instructs the method to use a random number of elements.  Use 0 to return an empty list.
- Parameter `createWith` determines if and how to populated the generated `IList<T>` with nulls.  Here are the options:
    - `NoNulls` - The resulting list should not contain any null elements.
    - `OneOrMoreNulls` - The resulting list should contain one or more null elements.  This is a way to guarantee a null element.
    - `ZeroOrMoreNulls` - The resulting list should contain zero or more null elements.  This is a way to get a list that may or may not contain nulls.


Some.ReadOnlyDummies\<T>
------------------------
Returns an `IReadOnlyList<T>`; otherwise behaves exactly like `Some.Dummies<T>` documented above.


Constrained Dummies
-------------------
Sometimes it's useful to constrain dummies.  AutoFakeItEasy implements these extension methods for that purpose: `ThatIs`, `ThatIsNot`, `Whose`, `ThatIsIn`, `ThatIsNotIn`, `ThatIsInRange`, `ThatIsNotInRange`

```c#
var nonZero = A.Dummy<int>().ThatIs(i => i != 0);

var tallPerson = A.Dummy<Person>().Whose(p => p.HeightInCentimeters > 182);

var green = new Green();
var notGreen = A.Dummy<Color>().ThatIsNot(green);

var winterMonthsInNorthAmerica = A.Dummy<Month>().ThatIsIn(new[] { Month.December, Month.January, Month.February });

var springThruFallInNorthAmerica = A.Dummy<Month>().ThatIsNotIn(new[] { Month.December, Month.January, Month.February });

var ageOfPerson = A.Dummy<int>().ThatIsInRange(1, 99);
var positiveNumber = A.Dummy<int>().ThatIsNotInRange(int.MinValue, 0);
```

- `ThatIs`, `Whose`: These methods do the same thing; select one based on readability.  A lambda is used to test dummies against a constraint/condition.  If the condition fails, then another dummy is created and tested ... and so on until the condition is satisifed or a maximum number of attempts has been made.  `maxAttempts` is an optional parameter with default of -1, which is unlimited attempts.
- `ThatIsNot`: This method creates a dummy that is not equal (using `object.Equals(object)`) to some comparison dummy.  It also has an optional `maxAttempts` parameter that works the same way as described above.
- `ThatIsIn`: This method creates a dummy that is contained within a set (`IEnumerable`) of comparison dummies.  There are two overloads.  One uses `object.Equals(object)` for comparison.  The other takes an `IEqualityComparer<T>` to use when comparing.  It also has an optional `maxAttempts` parameter that works the same way as described above.
- `ThatIsNotIn`: This method creates a dummy that is NOT contained within a set (`IEnumerable`) of comparison dummies.  There are two overloads.  One uses `object.Equals(object)` for comparison.  The other takes an `IEqualityComparer<T>` to use when comparing.  It also has an optional `maxAttempts` parameter that works the same way as described above.
- `ThatIsInRange`: This method creates a dummy that falls within a specified range.  There are two overloads.  One requires the type of dummy being created to implement `IComparable` and calls `IComparable<T>.CompareTo()` for comparison.  The other takes an `IComparer<T>` as a parameter and calls `IComparer<T>.Compare()` for comparison.  It also has an optional `maxAttempts` parameter that works the same way as described above.
- `ThatIsNotInRange`: This method creates a dummy that falls outside of a specified range.  There are two overloads.  One requires the type of dummy being created to implement `IComparable` and calls `IComparable<T>.CompareTo()` for comparison.  The other takes an `IComparer<T>` as a parameter and calls `IComparer<T>.Compare()` for comparison.  It also has an optional `maxAttempts` parameter that works the same way as described above.

These constraining methods also work with `Some.Dummies<T>()`  For example, this will make 5 attempts to create an IList of doubles until there are an even number of elements in the list:

`Some.Dummies<double>().Whose(l => l.Count % 2 == 0, 5)`


Other Useful Features
---------------------
AutoFakeItEasy can't create abstract types (unless you use a custom dummy creator - see above).  However, you can instruct AutoFakeItEasy to build a **random, concrete subclass of an abstract type**.  Put this in your dummy factory (see "Custom Dummy Creation" above):

```c#
AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<Animal>()
```

When this is configured, calls to `A.Dummy<Animal>()` might return a `Zebra` or `Lion` or `Dog` - all concrete subclasses of `Animal`.


How it Works
-----
- Under the covers, AutoFakeItEasy simply implements `IDummyFactory` and then bootstraps it into your app domain.
- For any call To `A.Dummy<T>` where T is NOT an interface type and NOT an abstract type, AutoFakeItEasy uses [AutoFixture] to create the object.
  - Interface and abstract types CAN be created if you add a custom dummy creator (see "Custom Dummy Creation" above).
  - Otherwise, AutoFakeItEasy has no way of knowning what concrete type to instantiate!
- [AutoFixture] has been customized so that:
  - Signed numeric types return positive, zero and negative numbers, instead of the default of positive numbers
  - `Enums` return random values instead of the default of sequential values
  - `Flags` `Enums` return a random set of individual flags OR'ed (`|=`) together.
  - `Bools` are randomly selected instead of the default of sequential values (false, true, false, true, ...)


[FakeItEasy]: https://fakeiteasy.github.io/
[AutoFixture]: https://github.com/AutoFixture/AutoFixture
[Dummy Factory]: https://github.com/FakeItEasy/FakeItEasy/wiki/Custom-Dummy-Creation
[IList\<T>]: https://msdn.microsoft.com/en-us/library/5y536ey6(v=vs.110).aspx