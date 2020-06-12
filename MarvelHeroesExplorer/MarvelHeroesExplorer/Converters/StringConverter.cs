using System;
using Windows.UI.Xaml.Data;

namespace MarvelHeroesExplorer.Converters
{
    public class StringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime dateTime)
            {
                return dateTime.ToString((string)parameter);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is string stringValue)
            {
                return DateTime.Parse(stringValue);
            }
            return null;
        }
    }

}
