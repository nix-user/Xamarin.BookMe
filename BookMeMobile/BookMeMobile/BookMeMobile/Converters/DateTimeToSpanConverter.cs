using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BookMeMobile.Converters
{
    public class DateTimeToSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dt = (DateTime)value;
            return dt.TimeOfDay;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var currentTimeSpan = (TimeSpan)value;
            DateTime currentDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, currentTimeSpan.Hours, currentTimeSpan.Minutes, currentTimeSpan.Seconds);
            return currentDateTime;
        }
    }
}