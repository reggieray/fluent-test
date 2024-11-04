namespace FluentTesting.Sample.Calculator.Tests;

public class CalculatorTests
{
    private readonly CalculatorTestSteps _testSteps = new();
    
    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(4, 5, 9)]
    [InlineData(22, 33, 55)]
    public async Task ShouldAdd(int a, int b, int expected)
    {
        await _testSteps
            .Given(x => x.ACalculator())
            .When(x => x.TwoNumbersAreAddedAsync(a, b)) // demonstrates async and no async usage 
            .Then(x => x.TheNumberShouldEqual(expected))
            .RunAsync();
    }
    
    [Theory]
    [InlineData(1, 2, 2)]
    [InlineData(4, 5, 20)]
    [InlineData(22, 33, 726)]
    public async Task ShouldMultiply(int a, int b, int expected)
    {
        await _testSteps
            .Given(x => x.ACalculator())
            .When(x => x.TwoNumbersMultiplied(a, b))
            .Then(x => x.TheNumberShouldEqual(expected))
            .RunAsync();
    }
    
    [Theory]
    [InlineData(2, 1, 2)]
    [InlineData(10, 2, 5)]
    [InlineData(50, 5, 10)]
    public async Task ShouldDivide(int a, int b, int expected)
    {
        await _testSteps
            .Given(x => x.ACalculator())
            .When(x => x.TwoNumbersDivided(a, b))
            .Then(x => x.TheNumberShouldEqual(expected))
            .RunAsync();
    }
    
    [Fact]
    public async Task ShouldThrowDivideByZeroException()
    {
        await _testSteps
            .Given(x => x.ACalculator())
            .When(x => x.DivideNumberByZero(80))
            .Then(x => x.DivideByZeroExceptionIsThrown())
            .RunAsync();
    }
    
    [Theory]
    [InlineData(2, 1, 1)]
    [InlineData(10, 2, 8)]
    [InlineData(50, 5, 45)]
    public async Task ShouldSubtract(int a, int b, int expected)
    {
        await _testSteps
            .Given(x => x.ACalculatorAsync()) // demonstrates async and no async usage 
            .When(x => x.TwoNumbersSubtracted(a, b))
            .Then(x => x.TheNumberShouldEqual(expected))
            .RunAsync();
    }
}