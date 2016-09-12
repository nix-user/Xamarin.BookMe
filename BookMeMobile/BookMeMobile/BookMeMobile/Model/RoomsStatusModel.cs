using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;

namespace BookMeMobile.Model
{
    public class RoomsStatusModel
    {
        public IEnumerable<RoomResult> Rooms { get; set; }

        public StatusCode StatusCode { get; set; }
    }
}