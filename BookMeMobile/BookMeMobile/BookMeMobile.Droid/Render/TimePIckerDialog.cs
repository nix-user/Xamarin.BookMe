using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BookMeMobile.Droid.Render
{
    public class TimePickerDialogRender : TimePickerDialog
    {
        public const int TimePickerInterval = 15;

        public TimePickerDialogRender(Context context,
            EventHandler<TimeSetEventArgs> callBack,
            int hourOfDay,
            int minute,
            bool is24HourView) :
            base(context,
               2,
                (sender, e) =>
            {
                callBack(sender,
                    new TimeSetEventArgs(e.HourOfDay, e.Minute * TimePickerInterval));
            },
                hourOfDay,
                  minute / TimePickerInterval,
                  is24HourView)
        {
        }

        public override void SetView(View view)
        {
            this.SetupMinutePicker(view);
            base.SetView(view);
        }

        private void SetupMinutePicker(View view)
        {
            var numberPicker = this.FindMinuteNumberPicker(view as ViewGroup);
            if (numberPicker != null)
            {
                numberPicker.MinValue = 0;
                numberPicker.MaxValue = 3;
                string[] range = new string[] { "00", "15", "30", "45" };
                numberPicker.SetDisplayedValues(range);
            }
        }

        private NumberPicker FindMinuteNumberPicker(ViewGroup viewGroup)
        {
            for (var i = 0; i < viewGroup.ChildCount; i++)
            {
                var child = viewGroup.GetChildAt(i);
                var numberPicker = child as NumberPicker;
                if (numberPicker != null)
                {
                    if (numberPicker.MaxValue == 59)
                    {
                        return numberPicker;
                    }
                }

                var childViewGroup = child as ViewGroup;
                if (childViewGroup != null)
                {
                    var childResult = this.FindMinuteNumberPicker(childViewGroup);
                    if (childResult != null)
                    {
                        return childResult;
                    }
                }
            }

            return null;
        }
    }
}