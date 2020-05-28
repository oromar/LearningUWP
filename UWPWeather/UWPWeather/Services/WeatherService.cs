using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace UWPWeather.Services
{
    public class WeatherService
    {
        public const string API_KEY = "52d5979f0c3a68f322ba0060707c801f";
        public static WeatherService Instance { get; } = new WeatherService();

        private WeatherService()
        {
        }

        public async Task<Weather> GetWeatherAsync(BasicGeoposition position)
        {
            var client = new HttpClient();
            var response =
                await client.GetAsync(
                    $@"http://api.openweathermap.org/data/2.5/weather?lat={position.Latitude}&lon={position.Longitude}&appid={API_KEY}"
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
