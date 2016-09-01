using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;

namespace BookMeMobile.Data
{
    class RestURl
    {
        private readonly string ApiURL = "http://localhost:52594/api/";

        public string BookURl
        {
            get
            {
                return this.ApiURL + "Booking/{0}";
            }
        }

        public string RoomURl
        {
            get
            {
                return this.ApiURL + "Room/{0}";
            }
        }

    }
}
