﻿using System;
using System.Diagnostics.Tracing;
using BookMeMobile.Render;
using BookMeMobile.WinPhone.Render;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Xamarin.Forms.Platform.WinRT;
using TimePicker = Xamarin.Forms.TimePicker;

[assembly: ExportRenderer(typeof(TimePicker24Hour), typeof(TimePicker24HRenderer))]

namespace BookMeMobile.WinPhone.Render
{
    public class TimePicker24HRenderer : TimePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null)
            {
               Control.ClockIdentifier = "24HourClock";
                Control.MinuteIncrement = 15;
            }
        }
    }
}