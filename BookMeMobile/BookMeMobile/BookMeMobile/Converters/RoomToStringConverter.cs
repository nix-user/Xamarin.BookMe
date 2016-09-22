using System;
using System.Globalization;
using BookMeMobile.Entity;
using Xamarin.Forms;

namespace BookMeMobile.Converters
{
    /// <summary>
    /// This class provide logic of converting Room values
    /// Convert back doesn't implemented yet
    /// </summary>
    public class RoomToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Room)value).Number;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}