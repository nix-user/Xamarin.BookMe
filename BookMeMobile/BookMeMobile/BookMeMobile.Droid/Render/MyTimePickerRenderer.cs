using System;
using System.ComponentModel;
using Android.App;
using Android.Runtime;
using Android.Widget;
using BookMeMobile.Droid.Render;
using BookMeMobile.Render;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.App.TimePickerDialog;
using TimePicker = Xamarin.Forms.TimePicker;

[assembly: ExportRenderer(typeof(TimePicker24Hour), typeof(MyTimePickerRenderer))]

namespace BookMeMobile.Droid.Render
{
    public class MyTimePickerRenderer : TimePickerRenderer
    {
        private TimeSpan currentTime;

        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);
            TimePickerDialogRender timePickerDlg = new TimePickerDialogRender(this.Context, new EventHandler<TimeSetEventArgs>(this.UpdateDuration), Element.Time.Hours, Element.Time.Minutes, true);
            var control = new EditText(this.Context);
            control.Focusable = false;
            control.FocusableInTouchMode = false;
            control.Clickable = false;
            control.Click += (sender, ea) => timePickerDlg.Show();
            control.Text = Element.Time.Hours.ToString("00") + ":" + Element.Time.Minutes.ToString("00");
            this.SetNativeControl(control);
        }

        public void UpdateDuration(object sender, TimeSetEventArgs e)
        {
            Element.Time = new TimeSpan(e.HourOfDay, e.Minute, 0);
            Control.Text = Element.Time.Hours.ToString("00") + ":" + Element.Time.Minutes.ToString("00");
        }
    }
}