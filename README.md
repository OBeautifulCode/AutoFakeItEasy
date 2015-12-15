AutoFakeItEasy
==============
[![Build status](https://ci.appveyor.com/api/projects/status/c92mt9ckaewlyl6m?svg=true)](https://ci.appveyor.com/project/SurajGupta/obeautifulcode-autofakeiteasy)
[![Nuget status](https://img.shields.io/nuget/vpre/OBeautifulCode.AutoFakeItEasy.svg)](https://www.nuget.org/packages/OBeautifulCode.AutoFakeItEasy/1.0.11-beta010)


Overview
--------
- This package makes [FakeItEasy] Dummies actually useful by wiring-them-up to real-looking test data.
- By default, the results of the following call will throw if it is ever used: `A.Dummy<int>()`
- With AutoFakeItEasy, that same call will return a **usable, random integer**.
- This makes unit tests more expressive, with less lines of code.  Like this:

```
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
    -  numeric types such as ints won't include zero or negative numbers unless you cutomize AutoFixture.
    -  It's less readable:

```
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

This makes it easy to create random but constrained integers.

Custom Dummy Creation
---------------------
You can customize how `A.Dummy<T>()` creates specific types as such:

```
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

```
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
        public int Priority => 0;

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


How it Works
-----
- Under the covers, AutoFakeItEasy simply implements `IDummyFactory` and then bootstraps it into your app domain.
- For any call To `A.Dummy<T>` where T is NOT an interface type and NOT an abstract type, AutoFakeItEasy uses [AutoFixture] to create the object.
- [AutoFixture] has been customized so that:
  - Signed numeric types return positive, zero and negative numbers, instead of the default of positive numbers
  - `Enums` return random values instead of the default of sequential values
  - `Bools` are randomly selected instead of the default of sequential values (false, true, false, true, ...)


[FakeItEasy]: https://fakeiteasy.github.io/
[AutoFixture]: https://github.com/AutoFixture/AutoFixture
[Dummy Factory]: https://github.com/FakeItEasy/FakeItEasy/wiki/Custom-Dummy-Creation