using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Exercise4.Converters
{
    public class ValueToForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var doubleValue = value as double?;
            if (doubleValue.HasValue)
                return doubleValue.Value < 0 ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.White);
            return new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return double.Parse(value as string ?? "0");
        }
    }
}
