using System;
using BookMeMobile.Render;
using BookMeMobile.Touch.Render;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TimePicker24Hour), typeof(TimePicker24HRenderer))]

namespace BookMeMobile.Touch.Render
{
    public class TimePicker24HRenderer : TimePickerRenderer
    {
        private TimePicker24Hour timePickerSize;

        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);
            var timePicker = (UIDatePicker)Control.InputView;
            timePicker.Locale = new NSLocale("no_nb");
            this.timePickerSize = (TimePicker24Hour)Element;
            this.SetFontSize(this.timePickerSize);
            timePicker.MinuteInterval = 15;
        }

        private void SetFontSize(TimePicker24Hour timePicker)
        {
            this.Control.Font = UIFont.FromName("BrandonGrotesque-Light", 40);
        }
    }
}