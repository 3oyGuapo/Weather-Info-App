using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Weather_Info_App
{
    /// <summary>
    /// Represent main object of api that stores all the information
    /// Stores all other object inside
    /// </summary>
    internal class WeatherInfo
    {
        //Attribute that link the Weather field from api's return json with this property
        [JsonProperty("weather")]
        public WeatherDescription[] Weather { get; set; }

        [JsonProperty("main")]
        public MainInfo Main { get; set; }

        [JsonProperty("wind")]
        public WindInfo Wind { get; set; }

        [JsonProperty("name")]
        public string CityName { get; set; }
    }

    /// <summary>
    /// Represent a specific description for the weather
    /// Address to the weather object array in the json format return by the api
    /// </summary>
    internal class WeatherDescription
    {
        [JsonProperty("description")]
        public string Description { get; set; }
    }

    /// <summary>
    /// Represent the main information about the weather such as temperature and humidity
    /// Address to the main object in the json format return by the api
    /// </summary>
    internal class MainInfo
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }
    }

    /// <summary>
    /// Represent the wind information about the weather
    /// Address to the wind object in the json format return by the api
    /// </summary>
    internal class WindInfo
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }
    }
}
