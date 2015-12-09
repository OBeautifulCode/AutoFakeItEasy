AutoFakeItEasy
==============
[![Build status](https://ci.appveyor.com/api/projects/status/c92mt9ckaewlyl6m?svg=true)](https://ci.appveyor.com/project/SurajGupta/obeautifulcode-autofakeiteasy)

Overview
--------
- This package makes [FakeItEasy] Dummies actually useful by wiring-them-up to real-looking test data.
- By default, this will explode if the result is ever used: `A.Dummy<int>()`
- Using AutoFakeItEasy, that same call will return a **usable, random integer**.
- This makes unit tests more expressive with less lines of code.  Like this:

```
public void BuildShips___Should_return_empty_collection_of_ships___When_number_of_ships_is_zero_or_negative()
{
    // Arrange
    var systemUnderTest = new ShipMaker();
    int numberOfShips = (int)A.Dummy<ZeroOrNegativeInteger>();  // more later on this type...

    // Act
    var result = systemUnderTest.BuildShips(numberOfShips);

    // Assert
    ex.Should().BeEmpty();
}
```

- It means that, in a wide set of scenarios, you can rely solely on `A.Fake<T>()` and `A.Dummy<T>()` for creating and configuring objects that you need to unit test a system.  This uniform grammar leads to cleaner, more readable unit tests.
- This likely violates the strict definition of a dummy - something that is required to make a call but is not used.  I don't think that that very narrow definition/behavior is all that useful.  FakeItEasy has tried to simplify the world by unifying stubs and mocks under Fakes.  I would like to further simplifying it, by unifying dummies, test data, and anonymous variables under Dummies.
-  I define a dummy as a real object that contains random but reasonable/real-like data, where there is no need to configure the object's behavior.  The *system under test* can reliably use that object to perform some operation.  *I* can use the object's value to formulate my expectations after the operation has been performed.
-  I use [Autofixture] to create the dummies - its a powerful library for creating test data.
-  So why not just use [Autofixture] independently?  In some cases you should - Autofixture is ridiculously flexible.   However...
    -  You have to new-up a fixture object in each test.  This is an extra line of code, but what's more problematic is that it doesn't allow you to exercise a wide range of test-data space.  For example, `Enums` will always be created in sequential order.  So unless you are new-ing up lots of Enums in that test (which is probably a smell), you will always test the same `Enum` values.
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
AutoFakeItEasy supplies these useful types.  They can all be casted to an `int`:

- `PositiveInteger` - a positive integer
- `ZeroOrPositiveIntger` - a positive integer or zero
- `NegativeInteger` - a negative integer
- `ZeroOrNegativeInteger` - a negative integer or zero

This makes it easy to create random but constrained integers.

How it Works
-----
- Under the covers, AutoFakeItEasy simply implements `IDummyFactory` and then bootstraps it into your app domain.
- For any call To `A.Dummy<T>` where T is NOT an interface type, AutoFakeItEasy uses [AutoFixture] to create the object.
- [AutoFixture] has been customized so that:
  - Signed numeric types return positive, zero and negative numbers, instead of the default of positive numbers
  - `Enums` return random values instead of the default of sequential values
  - `Bools` are randomly selected instead of the default of sequential values (false, true, false, true, ...)


[FakeItEasy]: https://fakeiteasy.github.io/
[AutoFixture]: https://github.com/AutoFixture/AutoFixture