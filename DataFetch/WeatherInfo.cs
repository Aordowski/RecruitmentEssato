using System.Net.Http;
using Newtonsoft.Json;

namespace ZadanieRekrutacyjne.DataFetch
{
    public class WeatherInfo
    {
        [JsonProperty("latitude")]
        public float Latitude { get; set; }

        [JsonProperty("longitude")]
        public float Longitude { get; set; }

        [JsonProperty("current")]
        public CurrentWeather Current { get; set; }

        [JsonProperty("hourly")]
        public Hourly Hourly { get; set; }
    }

    public class CurrentWeather
    {
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("temperature_2m")]
        public float Temperature2m { get; set; }

        [JsonProperty("is_day")]
        public int IsDay { get; set; }

        [JsonProperty("weather_code")]
        public int WeatherCode { get; set; }
    }

    public class Hourly
    {
        [JsonProperty("time")]
        public List<string> Time { get; set; }

        [JsonProperty("temperature_2m")]
        public List<float> Temperature2m { get; set; }

        [JsonProperty("weather_code")]
        public List<int> WeatherCode { get; set; }
    }

    public class WeatherService
    {
        public async Task<WeatherInfo> GetWeatherDataAsync()
        {
            using var client = new HttpClient();
            string url = "https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&current=temperature_2m,relative_humidity_2m,is_day,weather_code";

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WeatherInfo>(json);
        }

        public async Task<WeatherInfo> RunGetDataAsync()
        {
            return await GetWeatherDataAsync();
        }
        public async Task<WeatherInfo> RunGetData3DayAsync()
        {
            return await GetWeatherData3DayAsync();
        }
        public async Task<WeatherInfo> GetWeatherData3DayAsync()
        {
            using var client = new HttpClient();
            string url = "https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m,weather_code&forecast_days=3";

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WeatherInfo>(json);
        }

    }

}
