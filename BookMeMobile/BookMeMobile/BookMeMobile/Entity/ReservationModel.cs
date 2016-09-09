using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMeMobile.Entity
{
    public class ReservationModel : IEquatable<ReservationModel>
    {
        public int Id { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public Room Room { get; set; }

        public User Author { get; set; }

        public bool IsRecursive { get; set; }

        public TimeSpan Duration { get; set; }

        public bool Equals(ReservationModel other)
        {
            return this.Id == other.Id;
        }
    }
}