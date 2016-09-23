using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BookMeMobile.Binding
{
    /// <summary>
    /// A converter to check if a string is null or empty
    /// </summary>
    public class IsStringNullOrEmptyConverter : IValueConverter
    {
        /// <summary>
        /// Convert method
        /// </summary>
        /// <param name="value">String value</param>
        /// <returns>True if string is not null or empty</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = (string)value;
            return !string.IsNullOrEmpty(stringValue);
        }

        /// <summary>
        /// Method is not supported
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}