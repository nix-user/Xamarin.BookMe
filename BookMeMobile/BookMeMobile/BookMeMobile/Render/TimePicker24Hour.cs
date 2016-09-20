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
        private TimeSpan timeStep = TimeSpan.FromMinutes(15);

        public TimePicker24Hour() : base()
        {
            this.Time = this.RoundTime(DateTime.Now).TimeOfDay;
        }

        public DateTime RoundTime(DateTime dt)
        {
            var delta = dt.Ticks % this.timeStep.Ticks;
            bool roundUp = delta > this.timeStep.Ticks / 2;
            var offset = roundUp ? this.timeStep.Ticks : 0;

            return new DateTime(dt.Ticks + offset - delta, dt.Kind);
        }
    }
}