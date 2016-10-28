using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Provider;
using Xamarin.Forms;

namespace BookMeMobile.Binding
{
    /// <summary>
    /// A converter to inverse a boolean value from XAML code
    /// </summary>
    public class InverseBoolConverter : IValueConverter
    {
        /// <summary>
        /// Convert method
        /// </summary>
        /// <param name="value">A boolean value</param>
        /// <returns>Inverted value</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        /// <summary>
        /// Convert back method
        /// </summary>
        /// <param name="value">A boolean value</param>
        /// <returns>Inverted value</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}