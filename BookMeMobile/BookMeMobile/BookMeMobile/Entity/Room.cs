using System.Collections.Generic;

namespace BookMeMobile.Entity
{
    public class Room
    {
        public Room()
        {
            this.Bookings = new List<ReservationModel>();
        }

        public int Id { get; set; }

        public string Number { get; set; }

        public bool IsBig { get; set; }

        public bool IsHasPolykom { get; set; }

        public List<ReservationModel> Bookings { get; set; }
    }
}