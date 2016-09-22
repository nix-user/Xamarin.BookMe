using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Views.Animations;
using Javax.Xml.Datatype;

namespace BookMeMobile.Model
{
    public class ReservationModelToAdd
    {
        public ReservationModelToAdd(RoomFilterParameters filterParameters, int idRoom)
        {
            this.From = filterParameters.From;
            this.To = filterParameters.To;
            this.ResourceId = idRoom;
            this.IsRecursive = false;
        }

        public string Title { get; set; }

        public DateTime Date => this.Date.Date;

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int? ResourceId { get; set; }

        public TimeSpan Duration => this.To - this.From;

        public bool IsRecursive { get; set; }

        public bool IsLarge { get; set; }

        public bool HasPolycom { get; set; }
    }
}