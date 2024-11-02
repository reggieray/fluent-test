using FluentAssertions;

namespace FluentTesting.Sample;

public class CalculatorTests
{
    private readonly CalculatorTestSteps _calculatorTestSteps = new();
    
    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(4, 5, 9)]
    [InlineData(22, 33, 55)]
    public void ShouldAdd(int a, int b, int expected)
    {
        _calculatorTestSteps
            .Given(x => x.ANewCalculator())
            .When(x => x.TwoNumbersAreAdded(a, b))
            .Then(x => x.TheNumberShouldEqual(expected))
            .Test();
    }
    
    [Theory]
    [InlineData(1, 2, 2)]
    [InlineData(4, 5, 20)]
    [InlineData(22, 33, 726)]
    public void ShouldMultiply(int a, int b, int expected)
    {
        _calculatorTestSteps
            .Given(x => x.ANewCalculator())
            .When(x => x.TwoNumbersMultiplied(a, b))
            .Then(x => x.TheNumberShouldEqual(expected))
            .Test();
    }
    
    [Theory]
    [InlineData(2, 1, 2)]
    [InlineData(10, 2, 5)]
    [InlineData(50, 5, 10)]
    public void ShouldDivide(int a, int b, int expected)
    {
        _calculatorTestSteps
            .Given(x => x.ANewCalculator())
            .When(x => x.TwoNumbersDivided(a, b))
            .Then(x => x.TheNumberShouldEqual(expected))
            .Test();
    }
    
    [Fact]
    public void ShouldThrowDivideByZeroException()
    {
        _calculatorTestSteps
            .Given(x => x.ANewCalculator())
            .When(x => x.DivideNumberByZero(80))
            .Then(x => x.DivideByZeroExceptionIsThrown())
            .Test();
    }
    
    [Theory]
    [InlineData(2, 1, 1)]
    [InlineData(10, 2, 8)]
    [InlineData(50, 5, 45)]
    public void ShouldSubtract(int a, int b, int expected)
    {
        _calculatorTestSteps
            .Given(x => x.ANewCalculator())
            .When(x => x.TwoNumbersSubtracted(a, b))
            .Then(x => x.TheNumberShouldEqual(expected))
            .Test();
    }
}

public class CalculatorTestSteps
{
    private Calculator.Calculator _calculator = new();
    private double _calculatorResult;
    private Exception? _exception;

    public void ANewCalculator()
    {
        _calculator = new();
        _calculatorResult = 0;
    }

    public void TwoNumbersAreAdded(int a, int b)
    {
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