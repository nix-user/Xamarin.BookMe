using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMeMobile.Model
{
    public class RoomReservationsRequestModel
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int RoomId { get; set; }
    }
}