using System;
using System.Globalization;
using Xamarin.Forms;

namespace BookMeMobile.Converters
{
    /// <summary>
    /// This class provide logic of converting boolean values
    /// </summary>
    public class BoolToTextconverter : IValueConverter
    {
        private readonly string trueValue = "Да";
        private readonly string falseValue = "Нет";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? this.trueValue : this.falseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().Equals(this.trueValue);
        }
    }
}