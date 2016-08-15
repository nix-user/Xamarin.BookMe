using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Entity
{
    public class MyBookViewResult
    {
        public int Room { get; set; }

        public TimeSpan From { get; set; }

        public TimeSpan To { get; set; }

        public DateTime Date { get; set; }

        public bool IsBig { get; set; }

        public bool IsHasPolykom { get; set; }
    }
}
