using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;
using Newtonsoft.Json;
using Org.Apache.Http.Protocol;

namespace BookMeMobile.BL
{
    public class RoomRepository
    {
        private static List<Room> rooms = new List<Room>()
        {
              new Room()
            {
                IsBig = false,
                IsHasPolykom = false,
                Number = "304D",
                Id = 6,
                Bookings = new List<Booking>()
            },
            new Room()
            {
                IsBig = false,
                IsHasPolykom = false,
                Number = "505E",
                Id = 5,
                Bookings = new List<Booking>()
            },
            new Room()
            {
                IsBig = false,
                IsHasPolykom = false,
                Number = "403D",
                Id = 1,
                Bookings = new List<Booking>()
            },
            new Room()
            {
                IsBig = true,
                IsHasPolykom = false,
                Number = "202K",
                Id = 2,
                Bookings = new List<Booking>()
            },
            new Room()
            {
                IsBig = true,
                IsHasPolykom = false,
                Number = "323P",
                Id = 3,
                Bookings = new List<Booking>()
            },
            new Room()
            {
                IsBig = false,
                IsHasPolykom = true,
                Number = "678T",
                Id = 4,
                Bookings = new List<Booking>()
            }
        };

        private string restUri = "{0}";
        private HttpClient client;

        public RoomRepository()
        {
            this.client = new HttpClient();
        }

        public async Task<IEnumerable<Room>> GetAllRoom()
        {
            var uri = new Uri(string.Format(this.restUri, string.Empty));
            var response = await this.client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Room>>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task<Room> GetRoom(int id)
        {
            var uri = new Uri(string.Format(this.restUri, id));
            var response = await this.client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Room>(content);
            }
            else
            {
                return null;
            }
        }

        public async void AddRoom(Room room)
        {
            var uri = new Uri(string.Format(this.restUri, room.Id));
            var json = JsonConvert.SerializeObject(room);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await this.client.PostAsync(uri, content);
        }
    }
}