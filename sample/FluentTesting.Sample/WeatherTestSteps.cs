using System.Net;
using System.Text.Json;
using FluentAssertions;
using FluentTesting.Sample.Weather;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;

namespace FluentTesting.Sample;

public class WeatherTestSteps
{
    private readonly Mock<ILogger<WeatherService>> _mockWeatherLogger = new();
    private readonly Mock<HttpMessageHandler> _mockMessageHandler = new();
    
    private readonly WeatherService _weatherService;
    private WeatherForecast[]? _weatherForecasts;
    private Exception? _exception;

    public WeatherTestSteps()
    {
        var httpClient = new HttpClient(_mockMessageHandler.Object);
        _weatherService = new WeatherService(_mockWeatherLogger.Object, httpClient);
    }
    
    public void AWeatherApiIsHealthy()
    {
        var weatherForecasts = new WeatherForecast[]
        {
            new() { Date = DateTime.Now, TemperatureC = 14, Summary = "Mild" },
            new() { Date = DateTime.Now.AddDays(1), TemperatureC = 30, Summary = "Hot" }
        };
        
        var response = new HttpResponseMessage(HttpStatusCode.OK)
            { Content = new StringContent(JsonSerializer.Serialize(weatherForecasts), System.Text.Encoding.UTF8, "application/json") };
        
        _mockMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);
    }
    
    public void AWeatherApiIsUnHealthy()
    {
        var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        
        _mockMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);
    }

    public async Task GetWeatherForecastsAsyncIsCalled()
    {
        _weatherForecasts = await _weatherService.GetWeatherForecastsAsync();
    }
    
    public async Task GetWeatherForecastsAsyncIsRequestedAndErrors()
    {
        _exception =  await Record.ExceptionAsync(() => _weatherService.GetWeatherForecastsAsync());
    }

    public void AHttpRequestExceptionIsThrown()
    {
        _exception.Should().NotBeNull();
        _exception.Should().BeOfType<HttpRequestException>();
        _exception!.Message.Should().Be("Response status code does not indicate success: 500 (Internal Server Error).");
    }

    public void TheWeatherForecastsIsReturned()
    {
        _weatherForecasts.Should().NotBeNullOrEmpty();
        _weatherForecasts.Should().HaveCount(2);
    }

    public void A200OkIsLogged()
    {
        _mockWeatherLogger.VerifyLogging("OK for /weatherforecast", LogLevel.Information);
    }
    
    public void A500InternalServerErrorIsLogged()
    {
        _mockWeatherLogger.VerifyLogging("InternalServerError for /weatherforecast", LogLevel.Information);
    }
}