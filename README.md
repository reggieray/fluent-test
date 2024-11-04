# Fluent Testing

A simple and expressive tool for writing unit tests in C# using a fluent API. It provides a `Given-When-Then` syntax, making tests easy to read and write with a clear, structured flow.

This library is intentionally minimal and lightweight, with only a few core methods and no external dependencies. This simplicity makes it easy to extend or adapt to your needs, and you’re free to copy or modify the code as you wish. The goal is to support flexibility without locking you into any specific framework or toolset.

This project was inspired by [BDDfy](https://github.com/TestStack/TestStack.BDDfy) and [BDTest](https://github.com/thomhurst/BDTest), two libraries I'm a fan of and regularly use. While these libraries are fantastic for fluent Given-When-Then API syntax, I found myself searching for a simpler option as I didn't want any extra customizations or the need to have HTML reports. I purely wanted something just to organize my test code so it was easier to read and understand.

# Installation

To add this library to your project, you can install it via NuGet with the following command:

```bash
dotnet add package FluentTesting
```

Alternatively, as this package only consists of one class plus a few extension methods, you can copy the `FluentTest` class code directly into your project and start using it immediately.

# Features

- Fluent Syntax: Provides a clear Given-When-Then structure that makes tests highly readable.
- Sync and Async Support: Supports both synchronous and asynchronous test steps, making it flexible for different scenarios.
- Minimal and Lightweight: With no external dependencies, it’s easy to integrate and adapt without extra configuration.
- Customizable: Allows flexibility to add your own methods and steps as needed.

# Usage

## Basic Example

There are two approaches available:

1. Using a Dedicated Class for Test Steps
2. Defining Steps Inline in the Test Class

#### Option 1: Dedicated Test Steps Class

This approach allows you to create a dedicated class, such as `CalculatorTestSteps`, that defines the setup, execution, and verification steps for your test.

```csharp
public class CalculatorTests
{
    private readonly CalculatorTestSteps _testSteps = new();
    
    [Fact]
    public async Task ShouldMultiply()
    {
        await _testSteps
            .Given(x => x.ACalculator())
            .When(x => x.TwoNumbersMultiplied(10, 10))
            .Then(x => x.TheNumberShouldEqual(100))
            .RunAsync();
    }
}
```

Here, the `CalculatorTestSteps` class manages the test steps in a modular way:

```csharp
public class CalculatorTestSteps
{
    private Calculator _calculator = null!;
    private double _calculatorResult
    
    public void ACalculator()
    {
        _calculator = new();
    }
    
    public void TwoNumbersMultiplied(int a, int b)
    {
        _calculatorResult = _calculator.Multiply(a, b);
    }
    
    public void TheNumberShouldEqual(int expected)
    {
        _calculatorResult.Should().Be(expected);
    }
}
```

#### Option 2: Inline Test Steps

Alternatively, you can define the `Given`, `When`, and `Then` steps directly within the test class itself:

```csharp
public class PrimeServiceTests
{
    private Sample.PrimeService.PrimeService? _primeService;
    private bool _primeResult;
    
    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(7)]
    public async Task ShouldReturnTrueIfPrimeNumber(int value)
    {
        await this
            .Given(x => x.APrimeService())
            .When(x => x.IsPrimeIsCalledFor(value))
            .Then(x => x.TrueIsReturned())
            .RunAsync();
    }
    
    private void TrueIsReturned()
    {
        _primeResult.Should().BeTrue();
    }

    private void IsPrimeIsCalledFor(int value)
    {
        _primeResult = Sample.PrimeService.PrimeService.IsPrime(value);
    }

    private void APrimeService()
    {
        _primeService = new Sample.PrimeService.PrimeService();
    }
}
```

Both approaches are valid and can be chosen depending on the complexity of your tests and your preferences.