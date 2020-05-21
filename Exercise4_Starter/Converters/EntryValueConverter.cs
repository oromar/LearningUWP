using System;
using Windows.UI.Xaml.Data;

namespace Exercise4.Converters
{
    public class EntryValueConverter: IValueConverter
    {
        private const string VALUE_FORMAT = "0.00";
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var doubleValue = value as double?;
            if (doubleValue.HasValue)
                return doubleValue.Value.ToString(VALUE_FORMAT);
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return double.Parse(value as string ?? "0");
        }
    }
}
