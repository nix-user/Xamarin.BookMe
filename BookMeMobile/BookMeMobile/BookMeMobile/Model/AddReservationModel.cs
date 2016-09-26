using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Views.Animations;
using Javax.Xml.Datatype;

namespace BookMeMobile.Model
{
    public class AddReservationModel
    {
        public AddReservationModel(SelectModel filterParameters, RoomViewModel roomModel)
        {
            this.From = filterParameters.From;
            this.To = filterParameters.To;
            this.ResourceId = roomModel.Id;
            this.IsRecursive = false;
            this.IsLarge = filterParameters.IsLarge;
            this.HasPolycom = filterParameters.HasPolycom;
            this.NumberRoom = roomModel.NumberRoom;
        }

        public string NumberRoom { get; set; }

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