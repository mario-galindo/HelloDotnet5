using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HelloDotnet5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherClient _client;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherClient client)
        {
            _logger = logger;
            _client = client;
        }

        [HttpGet]
        [Route("{city}")]
        public async Task<WeatherForecast> Get(string city)
        {
            var forecast = await _client.GetCurrentWeatherAsync(city);

            return new WeatherForecast
            {
                Summary = forecast.Weather[0].description,
                TemperatureC = (int)forecast.main.temp,
                Date = DateTimeOffset.FromUnixTimeSeconds(forecast.dt).DateTime

            };
        }
    }
}
