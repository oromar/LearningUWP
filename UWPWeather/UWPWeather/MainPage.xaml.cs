using System;
using UWPWeather.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPWeather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var weather = await WeatherService.Instance.GetWeatherAsync();
            ImageView.UriSource = new Uri(weather.Icon);
            MainTextBlock.Text = weather.Main;
            DescriptionTextBlock.Text = weather.Description;
            CityTextBlock.Text = weather.City;
            TemperatureTextBlock.Text = weather.Temperature.ToString("0");
            MainGrid.Visibility = Visibility.Visible;
        }
    }
}
