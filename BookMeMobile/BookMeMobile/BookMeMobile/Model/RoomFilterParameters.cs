using System;

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