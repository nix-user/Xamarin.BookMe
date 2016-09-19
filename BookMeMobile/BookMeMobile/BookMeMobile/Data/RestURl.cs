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
        private const string Adress = "http://10.10.41.172/";
        private const string ApiURL = Adress + "api/";

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
                return ApiURL + "room/";
            }
        }

        public static string GetEmptyRoom
        {
            get
            {
                return RoomURl + "available";
            }
        }

        public static string GetCurrentRoomReservation
        {
            get { return RoomURl + "/reservations"; }
        }

        public static string GetUserReservation
        {
            get { return BookURl; }
        }

        public static string GetToken
        {
            get { return Adress + "token"; }
        }
    }
}