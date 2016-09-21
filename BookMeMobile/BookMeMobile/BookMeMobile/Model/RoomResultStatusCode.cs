using System.Collections.Generic;
using BookMeMobile.Entity;
using BookMeMobile.Enums;

namespace BookMeMobile.Model
{
    public class RoomResultStatusCode
    {
        public IEnumerable<Room> LIstRoomResults { get; set; }

        public StatusCode StatusCode { get; set; }
    }
}