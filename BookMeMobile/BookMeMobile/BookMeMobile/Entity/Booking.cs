﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMeMobile.Entity
{
    public class Booking
    {
        public int Id { get; set; }

        public TimeSpan From { get; set; }

        public TimeSpan To { get; set; }

        public DateTime Date { get; set; }

        public Room Room { get; set; }

        public User WhoBook { get; set; }
    }
}