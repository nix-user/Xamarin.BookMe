using System.Collections.Generic;
using BookMeMobile.Enums;

namespace BookMeMobile.Model
{
    public class RoomsStatusModel
    {
        public IEnumerable<RoomResult> Rooms { get; set; }

        public StatusCode StatusCode { get; set; }
    }
}