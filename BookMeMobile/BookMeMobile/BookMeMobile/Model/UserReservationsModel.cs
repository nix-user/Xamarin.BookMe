using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;

namespace BookMeMobile.Model
{
    public class UserReservationsModel
    {
        public IEnumerable<Reservation> TodayReservations { get; set; }

        public IEnumerable<Reservation> AllReservations { get; set; }
    }
}