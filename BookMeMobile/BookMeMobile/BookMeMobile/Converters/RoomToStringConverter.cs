using System;
using System.Globalization;
using BookMeMobile.Entity;
using Xamarin.Forms;

namespace BookMeMobile.Converters
{
    public class RoomToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Room)value).Number;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}