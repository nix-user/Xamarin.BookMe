using BookMeMobile.Entity;
using BookMeMobile.Enums;

namespace BookMeMobile.Model
{
    public class ReservationStatusModel
    {
        public Reservation Reservation { get; set; }

        public StatusCode StatusCode { get; set; }
    }
}