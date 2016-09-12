using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
                return ApiURL + "Reservation/";
            }
        }

        public static string RoomURl
        {
            get
            {
                return ApiURL + "Room/";
            }
        }

        public static string GetEmptyRoom
        {
            get
            {
                return ApiURL + "available";
            }
        }

        public static string GetCurrentRoomReservation
        {
            get { return RoomURl + "/reservations"; }
        }

        public static string GetUserReservation
        {
            get { return BookURl + "{0}"; }
        }
    }
}