using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using Newtonsoft.Json;

namespace Weather_Info_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //a object use to send reques and receive response
        private readonly HttpClient _httpClient = new HttpClient();
        //api key that used for requesting weather information
        private const string ApiKey = "11a033043b103c6225493e97f891c853";
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Button click event that get the user input as cityname and send url request using cityname and api key to get weather response for input city
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CheckWeatherButton_Click(object sender, RoutedEventArgs e)
        {
            //Get city name from textbox
            string cityName = CityTextBox.Text;

            //Makesure it not empty
            if (string.IsNullOrEmpty(cityName))
            {
                MessageBox.Show("Please enter a city to proceed checking.");
                return;
            }

            //Requesting url for gathering weather information
            string requestURL = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={ApiKey}&units=metric";

            //Try catch to avoid run in exception
            try
            {
                //Requesting from the openweathermap url and await here due to the response might take some time 
                HttpResponseMessage response = await _httpClient.GetAsync(requestURL);

                //Checking the response is success
                response.EnsureSuccessStatusCode();

                //Read response as string and store in the variable
                string jsonResponse = await response.Content.ReadAsStringAsync();

                //Use the deserialize function to convert the response json format to the object of defined class
                WeatherInfo weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(jsonResponse);

                //Use this object as parameter and send to UpdateUI for updating the information
                UpdateUI(weatherInfo);
            }

            //Deal with the http request exception
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error getting weather info: {ex.Message}");
            }
            //Deal with other type of exception
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error occur during request: {ex.Message}");
            }
        }

        /// <summary>
        /// Update the user interface with the returned weather information from the url
        /// </summary>
        /// <param name="weatherInfo"></param>
        private void UpdateUI(WeatherInfo weatherInfo)
        {
            //Make sure the object is not null
            if (weatherInfo != null)
            {
                //Show the information in the textbox
                CityNameTextBlock.Text = weatherInfo.CityName;
                TemperatureTextBlock.Text = $"{weatherInfo.Main.Temperature}";
                DescriptionTextBlock.Text = weatherInfo.Weather.FirstOrDefault()?.Description ?? "N/A";//If the weather is null, assign N/A as default value
                HumidityTextBlock.Text = $"Humidity: {weatherInfo.Main.Humidity}";
                WindSpeedTextBlock.Text = $"Wind Speed: {weatherInfo.Wind.Speed}";
            }
        }

        /// <summary>
        /// Textbox event that triggers when a enter key is press down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CityTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            //When enter key is pressing down, call checkWeather button event
            if (e.Key == Key.Enter)
            {
                CheckWeatherButton_Click(sender, e);
            }
        }
    }
}