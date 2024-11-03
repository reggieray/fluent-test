using System.Net.Http.Json;
using Microsoft.Extensions.Logging;

namespace FluentTesting.Sample.Weather;

public class WeatherService(ILogger<WeatherService> logger, HttpClient httpClient)
{
    public async Task<WeatherForecast[]?> GetWeatherForecastsAsync()
    {
        var response = await httpClient.GetAsync("http://localhost/api/weatherforecast");
        
        logger.LogInformation("{responseCode} for {urlPath}", response.StatusCode, "/weatherforecast");
        
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<WeatherForecast[]>();
    }
}