# Fluent Testing

A simple and expressive tool for writing unit tests in C# using a fluent API. It provides a `Given-When-Then` syntax, making tests easy to read and write with a clear, structured flow.

This library is intentionally minimal and lightweight, with only a few core methods and no external dependencies. This simplicity makes it easy to extend or adapt to your needs, and you’re free to copy or modify the code as you wish. The goal is to support flexibility without locking you into any specific framework or toolset.

# Usage

## Basic Example

There are two approaches available, you could have a dedicated class that defines steps of your test like below. 

```csharp
public class CalculatorTests
{
    private readonly CalculatorTestSteps _testSteps = new();
    
    [Fact]
    public async Task ShouldMultiply()
    {
        await _testSteps
            .Given(x => x.ANewCalculator())
            .When(x => x.TwoNumbersMultiplied(10, 10))
            .Then(x => x.TheNumberShouldEqual(100))
            .RunAsync();
    }
}
```

The `CalculatorTestSteps` class is used to perform setup, execution and verification of the subject under test. 

```csharp
public class CalculatorTestSteps
{
    private Calculator _calculator = null!;
    private double _calculatorResult
    
    public void ANewCalculator()
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

The second approach is you can create methods within the test class itself like the example below.

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

Both options are valid depending on your scenario.