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
        private readonly HttpClient _httpClient = new HttpClient();
        private const string ApiKey = "11a033043b103c6225493e97f891c853";
        public MainWindow()
        {
            InitializeComponent();
        }


        private async void CheckWeatherButton_Click(object sender, RoutedEventArgs e)
        {
            string cityName = CityTextBox.Text;

            if (string.IsNullOrEmpty(cityName))
            {
                MessageBox.Show("Please enter a city to proceed checking.");
                return;
            }

            string requestURL = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={ApiKey}&units=metric";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(requestURL);

                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();

                WeatherInfo weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(jsonResponse);

                UpdateUI(weatherInfo);
            }

            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error getting weather info: {ex.Message}");
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error occur during request: {ex.Message}");
            }
        }

        private void UpdateUI(WeatherInfo weatherInfo)
        {
            if (weatherInfo != null)
            {
                CityNameTextBlock.Text = weatherInfo.CityName;
                TemperatureTextBlock.Text = $"{weatherInfo.Main.Temperature}";
                DescriptionTextBlock.Text = weatherInfo.Weather.FirstOrDefault()?.Description ?? "N/A";
                HumidityTextBlock.Text = $"Humidity: {weatherInfo.Main.Humidity}";
                WindSpeedTextBlock.Text = $"Wind Speed: {weatherInfo.Wind.Speed}";
            }
        }
    }
}