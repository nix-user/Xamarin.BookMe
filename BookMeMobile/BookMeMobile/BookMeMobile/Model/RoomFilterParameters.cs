using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMeMobile.Model
{
    public class RoomFilterParameters
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public bool HasPolycom { get; set; }

        public bool IsLarge { get; set; }
    }
}