using System;
using System.ComponentModel;
using Android.App;
using Android.Runtime;
using Android.Widget;
using BookMeMobile.Droid.Render;
using BookMeMobile.Render;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AndroidTimePicker = Android.Widget.TimePicker;
using TimePicker = Xamarin.Forms.TimePicker;

[assembly: ExportRenderer(typeof(TimePicker24Hour), typeof(MyTimePickerRenderer))]

namespace BookMeMobile.Droid.Render
{
    public class MyTimePickerRenderer : ViewRenderer<TimePicker, EditText>, TimePickerDialog.IOnTimeSetListener, IJavaObject, IDisposable
    {
        private TimePickerDialog dialog = null;
        private TimeSpan currentTime;

        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);
            this.SetNativeControl(new EditText(Forms.Context));
            this.Control.Click += this.Control_Click;
            this.currentTime = e.NewElement.Time;
            this.Control.Text = this.currentTime.ToString(@"hh\:mm");
            this.Control.KeyListener = null;
            this.Control.FocusChange += this.Control_FocusChange;
            this.Control.TextSize = 45;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            this.Control.Text = ((TimePicker)sender).Time.ToString(@"hh\:mm");
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
                this.dialog = new TimePickerDialog(Forms.Context, this, this.currentTime.Hours, this.currentTime.Minutes, true);
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