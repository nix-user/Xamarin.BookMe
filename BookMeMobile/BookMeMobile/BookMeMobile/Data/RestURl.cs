﻿using System;
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
        private const string ApiURL = "http://10.10.41.76/api/";

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
                return RoomURl + "available?From={0}&To={1}&HasPolycom={2}&IsLarge={3}";
            }
        }

        public static string GetCurrentRoomReservation
        {
            get { return RoomURl + "/reservations?From={0}&To={1}&roomId={2}"; }
        }

        public static string GetUserReservation
        {
            get { return BookURl + "{0}"; }
        }
    }
}