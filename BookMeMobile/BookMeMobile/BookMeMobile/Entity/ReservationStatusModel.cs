using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMeMobile.Entity
{
    public class ReservationStatusModel
    {
        public ReservationModel Reservation { get; set; }

        public StatusCode StatusCode { get; set; }
    }
}