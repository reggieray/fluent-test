using FluentAssertions;

namespace FluentTesting.Sample.PrimeService.Tests;

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
    
    [Theory]
    [InlineData(4)]
    [InlineData(6)]
    [InlineData(8)]
    [InlineData(9)]
    public async Task ShouldReturnFalseIfNotPrimeNumber(int value)
    {
        await this
            .Given(x => x.APrimeService())
            .When(x => x.IsPrimeIsCalledFor(value))
            .Then(x => x.FalseIsReturned())
            .RunAsync();
    }

    private void FalseIsReturned()
    {
        _primeResult.Should().BeFalse();
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