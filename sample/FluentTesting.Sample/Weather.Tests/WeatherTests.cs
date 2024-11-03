namespace FluentTesting.Sample.Weather.Tests;

public class WeatherTests
{
    private readonly WeatherTestSteps _testSteps = new();

    [Fact]
    public async Task ShouldGetWeatherForecastsAsync()
    {
        await _testSteps
            .Given(x => x.AWeatherApiIsHealthy())
            .When(x => x.GetWeatherForecastsAsyncIsCalled())
            .Then(x => x.TheWeatherForecastsIsReturned())
            .And(x => x.A200OkIsLogged())
            .RunAsync();
    }
    
    [Fact]
    public async Task ShouldThrowOnGetWeatherForecastsAsyncError()
    {
        await _testSteps
            .Given(x => x.AWeatherApiIsUnHealthy())
            .When(x => x.GetWeatherForecastsAsyncIsRequestedAndErrors())
            .Then(x => x.AHttpRequestExceptionIsThrown())
            .And(x => x.A500InternalServerErrorIsLogged())
            .RunAsync();
    }
}