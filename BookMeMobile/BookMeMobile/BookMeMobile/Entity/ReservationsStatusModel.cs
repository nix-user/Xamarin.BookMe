using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMeMobile.Entity
{
    public class ReservationsStatusModel
    {
        public List<MyReservationViewResult> ReservationModels { get; set; }

        public StatusCode StatusCode { get; set; }
    }
}