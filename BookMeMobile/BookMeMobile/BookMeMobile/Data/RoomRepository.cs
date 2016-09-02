﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Data;
using BookMeMobile.Entity;
using Newtonsoft.Json;

namespace BookMeMobile.BL
{
    public class RoomRepository
    {
        private string restUri;
        private HttpClient client;

        public RoomRepository()
        {
            this.restUri = RestURl.RoomURl;
            this.client = new HttpClient();
        }

        public async Task<IEnumerable<Room>> GetAllRoom()
        {
            var uri = new Uri(string.Format(this.restUri, string.Empty));
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
            var uri = new Uri(string.Format(this.restUri, id));
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
            var uri = new Uri(string.Format(this.restUri, room.Id));
            var json = JsonConvert.SerializeObject(room);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await this.client.PostAsync(uri, content);
            return response.IsSuccessStatusCode;
        }
    }
}