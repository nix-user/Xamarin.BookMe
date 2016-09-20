using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BookMeMobile.Render
{
    public class TimePicker24Hour : TimePicker
    {
        public TimePicker24Hour() : base()
        {
            this.Time = this.RoundTime(DateTime.Now.TimeOfDay);
        }

        protected TimeSpan RoundTime(TimeSpan time)
        {
            int minute = time.Minutes;
            if (minute <= 7)
            {
                minute = 0;
            }

            if (minute >= 8 && minute <= 22)
            {
                minute = 15;
            }

            if (minute >= 23 && minute <= 37)
            {
                minute = 30;
            }

            if (minute >= 38 && minute <= 52)
            {
                minute = 45;
            }

            if (minute >= 53)
            {
                minute = 0;
            }

            return new TimeSpan(DateTime.Now.Hour, minute, 0);
        }
    }
}