using System;
using Android.App;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using BookMeMobile.Droid.Render;
using BookMeMobile.Render;
using Java.Lang;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AndroidTimePicker = Android.Widget.TimePicker;
using String = System.String;
using TextAlignment = Android.Views.TextAlignment;
using TimePicker = Xamarin.Forms.TimePicker;

[assembly: ExportRenderer(typeof(TimePicker24Hour), typeof(MyTimePickerRenderer))]

namespace BookMeMobile.Droid.Render
{
    public class MyTimePickerRenderer : ViewRenderer<TimePicker, EditText>, TimePickerDialog.IOnTimeSetListener, IJavaObject, IDisposable
    {
        private TimePickerDialog dialog = null;

        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);
            this.SetNativeControl(new EditText(Forms.Context));
            this.Control.Click += this.Control_Click;
            TimeSpan currentTime = e.NewElement.Time;
            this.Control.Text = $"{currentTime.Hours}:{currentTime.Minutes}";
            this.Control.KeyListener = null;
            this.Control.FocusChange += this.Control_FocusChange;
            this.Control.TextSize = 45;
        }

        private void Control_FocusChange(object sender, FocusChangeEventArgs e)
        {
            if (e.HasFocus)
            {
                this.ShowTimePicker();
            }
        }

        private void Control_Click(object sender, EventArgs e)
        {
            this.ShowTimePicker();
        }

        private void ShowTimePicker()
        {
            if (this.dialog == null)
            {
                this.dialog = new TimePickerDialog(Forms.Context, this, DateTime.Now.Hour, DateTime.Now.Minute, true);
            }

            this.dialog.Show();
        }

        public void OnTimeSet(AndroidTimePicker view, int hourOfDay, int minute)
        {
            var time = new TimeSpan(hourOfDay, minute, 0);
            this.Element.SetValue(TimePicker.TimeProperty, time);

            this.Control.Text = time.ToString(@"hh\:mm");
        }
    }
}