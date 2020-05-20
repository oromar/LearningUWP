using Exercise4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Services.Maps;
using Windows.UI.Xaml.Data;

namespace Exercise4.Converters
{
    public class EntryDateConverter : IValueConverter
    {
        private const string DATE_TIME_FORMAT = "dd/MM HH:mm";
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var dateTime = value as DateTime?;
            if (dateTime.HasValue)
                return dateTime.Value.ToString(DATE_TIME_FORMAT);
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var dtf = new DateTimeFormat(DATE_TIME_FORMAT);
            return DateTime.Parse(value as string, dtf.FormatProvider);
        }
    }
}
