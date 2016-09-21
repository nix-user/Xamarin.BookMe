using System;
using System.Globalization;
using Xamarin.Forms;

namespace BookMeMobile.Converters
{
    public class BoolToTextconverter : IValueConverter
    {
        private readonly string trueValue = "Да";

        private readonly string falseValue = "Да";

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