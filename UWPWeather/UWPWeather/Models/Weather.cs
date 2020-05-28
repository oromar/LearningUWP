namespace UWPWeather
{
    public class Weather
    {
        private double _temperature;
        private string _icon;

        public string City { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon
        {
            get
            {
                return $"http://openweathermap.org/img/wn/{_icon}@2x.png";
            }
            set
            {
                _icon = value;
            }
        }
        public double Temperature
        {
            get
            {
                return _temperature - 273.15;
            }
            set 
            {
                _temperature = value;
            }
        }
    }
}
