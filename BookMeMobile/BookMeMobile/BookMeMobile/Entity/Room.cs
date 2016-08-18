using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMeMobile.Entity
{
    public class Room
    {
        public Room()
        {
            Bookings=new List<Booking>();
        }
        public int Id { get; set; }

        public int Number { get; set; }

        public bool IsBig { get; set; }

        public bool IsHasPolykom { get; set; }

        public List<Booking> Bookings { get; set; }
    }
}
