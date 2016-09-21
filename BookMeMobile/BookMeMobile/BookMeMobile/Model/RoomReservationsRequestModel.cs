using System;

namespace BookMeMobile.Model
{
    public class RoomReservationsRequestModel
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int RoomId { get; set; }
    }
}