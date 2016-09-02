using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMeMobile.Entity
{
    public class MyReservationViewResult
    {
        public int Id { get; set; }

        public string Room { get; set; }

        public TimeSpan From { get; set; }

        public TimeSpan To { get; set; }

        public DateTime Date { get; set; }

        public bool IsBig { get; set; }

        public bool IsHasPolykom { get; set; }

        public bool? InRange { get; set; }

        public bool IsReservation { get; set; }

        public bool IsRecursive { get; set; }
    }
}