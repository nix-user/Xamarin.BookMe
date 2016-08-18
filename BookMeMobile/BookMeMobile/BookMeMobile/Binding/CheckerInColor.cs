﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace BookMeMobile.Binding
{
    public class CheckerInColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? inRange = (bool?)value;
            if (inRange != null)
            {
                if (inRange == true)
                {
                    return Color.Green;
                }
                else
                {
                    return Color.Silver;
                }
            }
            else
            {
                return Color.Default;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
