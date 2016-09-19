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
        private const string Adress = "http://10.10.41.76/";
        private const string ApiURL = Adress + "api/";

        public static string BookURl => ApiURL + "Reservation/";

        public static string RoomURl => ApiURL + "room/";

        public static string GetEmptyRoom => RoomURl + "available?From={0}&To={1}&HasPolycom={2}&IsLarge={3}";

        public static string GetCurrentRoomReservation => RoomURl + "/reservations?From={0}&To={1}&roomId={2}";

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