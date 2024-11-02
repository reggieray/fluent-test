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
            .Given("A calculator")
            .When(x => x.TwoNumbersAreAdded(a, b))
            .Then(x => x.TheNumberShouldEqual(expected))
            .Test();
    }
}

public class CalculatorTestSteps
{
    private readonly Calculator.Calculator _calculator = new();
    private double _addResult;

    public void TwoNumbersAreAdded(int a, int b)
    {
        _addResult = _calculator.Add(a, b);
    }

    public void TheNumberShouldEqual(int expected)
    {
        _addResult.Should().Be(expected);
    }
}