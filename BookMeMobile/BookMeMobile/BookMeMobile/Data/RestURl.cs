using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;

namespace BookMeMobile.Data
{
    public static class RestURl
    {
        private const string ApiURL = "http://10.10.41.172/api/";

        public static string BookURl
        {
            get
            {
                return ApiURL + "Book/{0}";
            }
        }

        public static string RoomURl
        {
            get
            {
                return ApiURL + "Room/{0}";
            }
        }
    }
}