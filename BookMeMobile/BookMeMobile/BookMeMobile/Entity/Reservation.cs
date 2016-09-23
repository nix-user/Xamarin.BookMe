using System;

namespace BookMeMobile.Entity
{
    public class Reservation
    {
        public string Title { get; set; }

        public int Id { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public Room Room { get; set; }

        public string Author { get; set; }

        public bool IsRecursive { get; set; }

        public int? ResourceId { get; set; }

        public TimeSpan Duration { get; set; }

        public string TextPeriod { get; set; }

        public string TextRule { get; set; }
    }
}