using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using BookMeMobile.Entity;
using BookMeMobile.Model;
using Newtonsoft.Json;

namespace BookMeMobile.Data
{
    public class RoomRepository
    {
        private HttpClient client;

        public RoomRepository()
        {
            this.client = new HttpClient();
            this.client.Timeout = new TimeSpan(0, 0, 5);
        }

        public async Task<IEnumerable<Room>> GetAllRoom()
        {
            var uri = new Uri(string.Format(RestURl.RoomURl, string.Empty));
            var response = this.client.GetAsync(uri);
            if (response.Result.IsSuccessStatusCode)
            {
                var content = await response.Result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Room>>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task<Room> GetRoom(int id)
        {
            var uri = new Uri(string.Format(RestURl.RoomURl, id));
            var response = this.client.GetAsync(uri);
            if (response.Result.IsSuccessStatusCode)
            {
                var content = await response.Result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Room>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> AddRoom(Room room)
        {
            var uri = new Uri(string.Format(RestURl.RoomURl, room.Id));
            var json = JsonConvert.SerializeObject(room);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await this.client.PostAsync(uri, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<RoomResult>> GetEmptyRoom(RoomFilterParameters filter)
        {
            var uri = new Uri(RestURl.GetEmptyRoom);
            var json = JsonConvert.SerializeObject(filter);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await this.client.PostAsync(uri, content);
            var contentResponce = await response.Content.ReadAsStringAsync();
            var roomResult = JsonConvert.DeserializeObject<List<Room>>(contentResponce);
            return roomResult.Select(item => new RoomResult
            {
                Id = item.Id,
                Number = item.Number
            });
        }
    }
}