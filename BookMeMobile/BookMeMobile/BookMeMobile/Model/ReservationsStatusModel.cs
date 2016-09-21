using System.Collections.Generic;
using BookMeMobile.Entity;
using BookMeMobile.Enums;

namespace BookMeMobile.Model
{
    public class ReservationsStatusModel
    {
        public List<Reservation> ReservationModels { get; set; }

        public StatusCode StatusCode { get; set; }
    }
}