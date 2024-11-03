using FluentAssertions;
using Xunit.Sdk;

namespace FluentTesting.Sample;

public class CalculatorTestSteps
{
    private Calculator.Calculator _calculator = null!;
    private double _calculatorResult;
    private Exception? _exception;

    public void ANewCalculator()
    {
        _calculator = new();
        _calculatorResult = 0;
    }
    
    public async Task ANewCalculatorAsync()
    {
        await Task.Delay(1);
        _calculator = new();
        _calculatorResult = 0;
    }

    public async Task TwoNumbersAreAddedAsync(int a, int b)
    {
        await Task.Delay(1);
        _calculatorResult = _calculator.Add(a, b);
    }
    
    public void TwoNumbersMultiplied(int a, int b)
    {
        _calculatorResult = _calculator.Multiply(a, b);
    }
    
    public void TwoNumbersDivided(int a, int b)
    {
        _calculatorResult = _calculator.Divide(a, b);
    }
    
    public void DivideNumberByZero(int a)
    {
        _exception = Record.Exception(() => _calculator.Divide(a, 0));
    }

    public void DivideByZeroExceptionIsThrown()
    {
        _exception.Should().NotBeNull();
        _exception.Should().BeOfType<DivideByZeroException>();
    }

    public void TwoNumbersSubtracted(int a, int b)
    {
        _calculatorResult = _calculator.Subtract(a, b);
    }

    public void TheNumberShouldEqual(int expected)
    {
        _calculatorResult.Should().Be(expected);
    }
}