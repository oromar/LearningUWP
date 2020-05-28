using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace UWPWeather.Services
{
    public class WeatherService
    {
        private const string API_KEY = "API_KEY";
        public static WeatherService Instance { get; } = new WeatherService();

        private WeatherService()
        {
        }

        public async Task<Weather> GetWeatherAsync()
        {
            var coordinate = await LocationService.Instance.GetCoordinateAsync();

            var client = new HttpClient();
            var response =
                await client.GetAsync(
                    $@"http://api.openweathermap.org/data/2.5/weather?lat={coordinate.Point.Position.Latitude}&lon={coordinate.Point.Position.Longitude}&appid={API_KEY}"
                );
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<dynamic>(json);

            return new Weather
            {
                Main = obj.weather[0].main,
                City = obj.name,
                Temperature = obj.main.temp,
                Description = obj.weather[0].description,
                Icon = obj.weather[0].icon
            };
        }
    }
}
