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
                return ApiURL + "Reservation/{0}";
            }
        }

        public static string RoomURl
        {
            get
            {
                return ApiURL + "Room/{0}";
            }
        }

        public static async Task<bool> IsConnected()
        {
            try
            {
                var client = new HttpClient();
                client.Timeout = new TimeSpan(0, 0, 0, 3);
                var uri = new Uri(string.Format(RestURl.BookURl, string.Empty));
                await client.GetAsync(uri);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}