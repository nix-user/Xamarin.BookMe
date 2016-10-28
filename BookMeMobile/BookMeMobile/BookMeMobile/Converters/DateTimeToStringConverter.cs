using System;
using System.Globalization;
using Xamarin.Forms;

namespace BookMeMobile.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime)value).ToString("ddd dd MMMM  yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToDateTime(value);
        }
    }
}