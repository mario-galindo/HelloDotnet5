using System.Net.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace HelloDotnet5
{
    public class WeatherClient
    {
        private readonly HttpClient _httpClient;
        private readonly ServiceSettings _serviceSettings;

        public WeatherClient(HttpClient httpClient, IOptions<ServiceSettings> options)
        {
            _httpClient = httpClient;
            _serviceSettings = options.Value;
        }

        public record Weather(string description);
        public record Main(decimal temp);
        public record Forecast(Weather[] Weather, Main main, long dt);

        public async Task<Forecast> GetCurrentWeatherAsync(string city)
        {
            var forecast = await _httpClient.GetFromJsonAsync<Forecast>($"https://{_serviceSettings.OpenWeatherHost}/data/2.5/weather?q={city}&appid={_serviceSettings.ApiKey}&units=metric");
            return forecast;
        }
    }
}