using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Weather_Info_App
{
    internal class WeatherInfo
    {
        [JsonProperty("Weather")]
        public WeatherDescription[] Weather { get; set; }

        [JsonProperty("main")]
        public MainInfo Main { get; set; }

        [JsonProperty("wind")]
        public WindInfo Wind { get; set; }

        [JsonProperty("name")]
        public string CityName { get; set; }
    }

    public class WeatherDescription
    {
        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class MainInfo
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }
    }

    public class WindInfo
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }
    }
}
