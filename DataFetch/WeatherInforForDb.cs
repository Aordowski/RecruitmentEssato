namespace ZadanieRekrutacyjne.DataFetch
{
    class WeatherInforForDb
    {
        public List<DateTime> Time { get; set; }

        public List<float> Temperature2m { get; set; }

        public List<bool> is_Day { get; set; }

        public List<string> WeatherType { get; set; } //From the WeatherCode

        public WeatherInforForDb(string dateTimeFromApi, float temperatureFromApi, int isDayFromApi, int weatherCodeFromApi)
        {

            //  foreach (string dateString in dateTimeFromApi)
            // 
            DateTime dt = DateTime.ParseExact(dateTimeFromApi, "yyyy-MM-ddTHH:mm", null);
            Time.Add(dt);
            // }
            // foreach (float temperature2m in temperatureFromApi)
            //  {
            Temperature2m.Add(temperatureFromApi);
            //  }
            //  foreach (int isItDay in isDayFromApi)
            // {
            if (isDayFromApi == 1)
            {
                is_Day.Add(true);
            }
            else if (isDayFromApi == 0)
            {
                is_Day.Add(false);
            }


            // }
            //   foreach (int code in weatherCodeFromApi) //i know its a huge block, but i dont think it can be done diffrently (avoiding wall of if/else if)

            switch (weatherCodeFromApi)
            {
                case 0:
                    WeatherType.Add("Clear Sky");
                    break;
                case 1:
                    WeatherType.Add("Mainly clear, partly cloudy, and overcast");
                    break;
                case 2:
                    WeatherType.Add("Mainly clear, partly cloudy, and overcast");
                    break;
                case 3:
                    WeatherType.Add("Mainly clear, partly cloudy, and overcast");
                    break;
                case 45:
                    WeatherType.Add("Fog and depositing rime fog");
                    break;
                case 48:
                    WeatherType.Add("Fog and depositing rime fog");
                    break;
                case 51:
                    WeatherType.Add("Drizzle: Light, moderate, and dense intensity");
                    break;
                case 53:
                    WeatherType.Add("Drizzle: Light, moderate, and dense intensity");
                    break;
                case 55:
                    WeatherType.Add("Drizzle: Light, moderate, and dense intensity");
                    break;
                case 56:
                    WeatherType.Add("Freezing Drizzle: Light and dense intensity");
                    break;
                case 57:
                    WeatherType.Add("Freezing Drizzle: Light and dense intensity");
                    break;
                case 61:
                    WeatherType.Add("Rain: Slight, moderate and heavy intensity");
                    break;
                case 63:
                    WeatherType.Add("Rain: Slight, moderate and heavy intensity");
                    break;
                case 65:
                    WeatherType.Add("Rain: Slight, moderate and heavy intensity");
                    break;
                case 66:
                    WeatherType.Add("Freezing Rain: Light and heavy intensity");
                    break;
                case 67:
                    WeatherType.Add("Freezing Rain: Light and heavy intensity");
                    break;
                case 71:
                    WeatherType.Add("Snow fall: Slight, moderate, and heavy intensity");
                    break;
                case 73:
                    WeatherType.Add("Snow fall: Slight, moderate, and heavy intensity");
                    break;
                case 75:
                    WeatherType.Add("Snow fall: Slight, moderate, and heavy intensity");
                    break;
                case 77:
                    WeatherType.Add("Snow grains");
                    break;
                case 80:
                    WeatherType.Add("Rain showers: Slight, moderate, and violent");
                    break;
                case 81:
                    WeatherType.Add("Rain showers: Slight, moderate, and violent");
                    break;
                case 82:
                    WeatherType.Add("Rain showers: Slight, moderate, and violent");
                    break;
                case 85:
                    WeatherType.Add("Snow showers slight and heavy");
                    break;
                case 86:
                    WeatherType.Add("Snow showers slight and heavy");
                    break;
                case 95:
                    WeatherType.Add("Thunderstorm: Slight or moderate");
                    break;
                case 96:
                    WeatherType.Add("TThunderstorm with slight and heavy hail");
                    break;
                case 99:
                    WeatherType.Add("Thunderstorm with slight and heavy hail");
                    break;
                default:
                    WeatherType.Add("Undefined weather code");
                    break;
            }



        }
    }
    public class WeatherRecord //single record that goes inside the Db
    {

        public int Id { get; set; }
        public DateTime Time { get; set; }
        public float Temperature2m { get; set; }
        public  bool? IsDay { get; set; }
        public string WeatherType { get; set; }


        public WeatherRecord(string dateTimeFromApi, float Temperature2mApi, int isDayApi,int weatherCodeFromApi)
        {
            Time = DateTime.ParseExact(dateTimeFromApi, "yyyy-MM-ddTHH:mm", null);
          
            Temperature2m = Temperature2mApi;

            if (isDayApi == 1)
            {
                IsDay = true;
            }
            else if (isDayApi == 0)
            {
                IsDay = false;
            }
            else
            {
                IsDay = null;
            }
            switch (weatherCodeFromApi)
            {
                case 0:
                    WeatherType = "Clear Sky";
                    break;
                case 1:
                    WeatherType = "Mainly clear, partly cloudy, and overcast";
                    break;
                case 2:
                    WeatherType = "Mainly clear, partly cloudy, and overcast";
                    break;
                case 3:
                    WeatherType = "Mainly clear, partly cloudy, and overcast";
                    break;
                case 45:
                    WeatherType = "Fog and depositing rime fog";
                    break;
                case 48:
                    WeatherType = "Fog and depositing rime fog";
                    break;
                case 51:
                    WeatherType = "Drizzle: Light, moderate, and dense intensity";
                    break;
                case 53:
                    WeatherType = "Drizzle: Light, moderate, and dense intensity";
                    break;
                case 55:
                    WeatherType = "Drizzle: Light, moderate, and dense intensity";
                    break;
                case 56:
                    WeatherType = "Freezing Drizzle: Light and dense intensity";
                    break;
                case 57:
                    WeatherType = "Freezing Drizzle: Light and dense intensity";
                    break;
                case 61:
                    WeatherType = "Rain: Slight, moderate and heavy intensity";
                    break;
                case 63:
                    WeatherType = "Rain: Slight, moderate and heavy intensity";
                    break;
                case 65:
                    WeatherType = "Rain: Slight, moderate and heavy intensity";
                    break;
                case 66:
                    WeatherType = "Freezing Rain: Light and heavy intensity";
                    break;
                case 67:
                    WeatherType = "Freezing Rain: Light and heavy intensity";
                    break;
                case 71:
                    WeatherType = "Snow fall: Slight, moderate, and heavy intensity";
                    break;
                case 73:
                    WeatherType = "Snow fall: Slight, moderate, and heavy intensity";
                    break;
                case 75:
                    WeatherType = "Snow fall: Slight, moderate, and heavy intensity";
                    break;
                case 77:
                    WeatherType = "Snow grains";
                    break;
                case 80:
                    WeatherType = "Rain showers: Slight, moderate, and violent";
                    break;
                case 81:
                    WeatherType = "Rain showers: Slight, moderate, and violent";
                    break;
                case 82:
                    WeatherType = "Rain showers: Slight, moderate, and violent";
                    break;
                case 85:
                    WeatherType = "Snow showers slight and heavy";
                    break;
                case 86:
                    WeatherType = "Snow showers slight and heavy";
                    break;
                case 95:
                    WeatherType = "Thunderstorm: Slight or moderate";
                    break;
                case 96:
                    WeatherType = "Thunderstorm with slight and heavy hail";
                    break;
                case 99:
                    WeatherType = "Thunderstorm with slight and heavy hail";
                    break;
                default:
                    WeatherType = "Undefined weather code";
                    break;
            }

        }
        public WeatherRecord(string dateTimeFromApi, float Temperature2mApi, int weatherCodeFromApi)
        {
            Time = DateTime.ParseExact(dateTimeFromApi, "yyyy-MM-ddTHH:mm", null);

            Temperature2m = Temperature2mApi;

            
            
                IsDay = null;
            
            switch (weatherCodeFromApi)
            {
                case 0:
                    WeatherType = "Clear Sky";
                    break;
                case 1:
                    WeatherType = "Mainly clear, partly cloudy, and overcast";
                    break;
                case 2:
                    WeatherType = "Mainly clear, partly cloudy, and overcast";
                    break;
                case 3:
                    WeatherType = "Mainly clear, partly cloudy, and overcast";
                    break;
                case 45:
                    WeatherType = "Fog and depositing rime fog";
                    break;
                case 48:
                    WeatherType = "Fog and depositing rime fog";
                    break;
                case 51:
                    WeatherType = "Drizzle: Light, moderate, and dense intensity";
                    break;
                case 53:
                    WeatherType = "Drizzle: Light, moderate, and dense intensity";
                    break;
                case 55:
                    WeatherType = "Drizzle: Light, moderate, and dense intensity";
                    break;
                case 56:
                    WeatherType = "Freezing Drizzle: Light and dense intensity";
                    break;
                case 57:
                    WeatherType = "Freezing Drizzle: Light and dense intensity";
                    break;
                case 61:
                    WeatherType = "Rain: Slight, moderate and heavy intensity";
                    break;
                case 63:
                    WeatherType = "Rain: Slight, moderate and heavy intensity";
                    break;
                case 65:
                    WeatherType = "Rain: Slight, moderate and heavy intensity";
                    break;
                case 66:
                    WeatherType = "Freezing Rain: Light and heavy intensity";
                    break;
                case 67:
                    WeatherType = "Freezing Rain: Light and heavy intensity";
                    break;
                case 71:
                    WeatherType = "Snow fall: Slight, moderate, and heavy intensity";
                    break;
                case 73:
                    WeatherType = "Snow fall: Slight, moderate, and heavy intensity";
                    break;
                case 75:
                    WeatherType = "Snow fall: Slight, moderate, and heavy intensity";
                    break;
                case 77:
                    WeatherType = "Snow grains";
                    break;
                case 80:
                    WeatherType = "Rain showers: Slight, moderate, and violent";
                    break;
                case 81:
                    WeatherType = "Rain showers: Slight, moderate, and violent";
                    break;
                case 82:
                    WeatherType = "Rain showers: Slight, moderate, and violent";
                    break;
                case 85:
                    WeatherType = "Snow showers slight and heavy";
                    break;
                case 86:
                    WeatherType = "Snow showers slight and heavy";
                    break;
                case 95:
                    WeatherType = "Thunderstorm: Slight or moderate";
                    break;
                case 96:
                    WeatherType = "Thunderstorm with slight and heavy hail";
                    break;
                case 99:
                    WeatherType = "Thunderstorm with slight and heavy hail";
                    break;
                default:
                    WeatherType = "Undefined weather code";
                    break;
            }

        }

    }
}
