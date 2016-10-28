using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMeMobile.Model
{
    public class SelectModel
    {
        private TimeSpan timeStep = TimeSpan.FromMinutes(15);

        public SelectModel()
        {
            this.Date = DateTime.Now;
            this.From = this.RoundTime(DateTime.Now);
            this.To = this.From.AddHours(1);
            this.HasPolycom = false;
            this.IsLarge = false;
        }

        public DateTime Date { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public bool IsLarge { get; set; }

        public bool HasPolycom { get; set; }

        public DateTime RoundTime(DateTime dt)
        {
            return new DateTime(((dt.Ticks + this.timeStep.Ticks - 1) / this.timeStep.Ticks) * this.timeStep.Ticks);
        }
    }
}