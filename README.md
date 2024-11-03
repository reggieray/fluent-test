# Fluent Testing

A simple and expressive tool for writing unit tests in C# using a fluent API. It provides a `Given-When-Then` syntax, making tests easy to read and write with a clear, structured flow.

This library is intentionally minimal and lightweight, with only a few core methods and no external dependencies. This simplicity makes it easy to extend or adapt to your needs, and you’re free to copy or modify the code as you wish. The goal is to support flexibility without locking you into any specific framework or toolset.

# Usage

## Basic Example

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

The recommend approach is to use a class to wrap your test steps like the example given below:

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