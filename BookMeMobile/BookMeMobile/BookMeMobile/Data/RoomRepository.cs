using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using BookMeMobile.Entity;
using BookMeMobile.Interface;
using BookMeMobile.Model;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BookMeMobile.Data
{
    public class RoomRepository
    {
        private HttpClient client;

        public RoomRepository()
        {
            this.client = new HttpClient();
            string token = DependencyService.Get<IFileWork>().LoadTextAsync().Result;
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
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
            var uri = new Uri(string.Format(RestURl.RoomURl + id));
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
            var uri = new Uri(string.Format(RestURl.RoomURl + room.Id));
            var json = JsonConvert.SerializeObject(room);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await this.client.PostAsync(uri, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<ResponceModelStatusCode<IEnumerable<Room>>> GetEmptyRoom(RoomFilterParameters filter)
        {
            var uri = new Uri(RestURl.GetEmptyRoom);
            var json = JsonConvert.SerializeObject(filter);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await this.client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                return await this.ResponceSuccess<IEnumerable<Room>>(response);
            }
            else
            {
                return this.ResponceFailed<IEnumerable<Room>>(response);
            }
        }

        public async Task<ResponceModelStatusCode<IEnumerable<ReservationModel>>> GetCurrentRoomReservation(RoomReservationsRequestModel reservationsModel)
        {
            try
            {
                var uri = new Uri(RestURl.GetCurrentRoomReservation);
                var json = JsonConvert.SerializeObject(reservationsModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await this.client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    return await this.ResponceSuccess<IEnumerable<ReservationModel>>(response);
                }
                else
                {
                    return this.ResponceFailed<IEnumerable<ReservationModel>>(response);
                }
            }
            catch (WebException e)
            {
                return new ResponceModelStatusCode<IEnumerable<ReservationModel>>()
                {
                    Result = null,
                    StatusCode = StatusCode.Error
                };
            }
        }

        public async Task<ResponceModelStatusCode<T>> ResponceSuccess<T>(HttpResponseMessage response) where T : class
        {
            var contentResponce = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseModel<T>>(contentResponce);
            if (result.IsOperationSuccessful)
            {
                return new ResponceModelStatusCode<T>()
                {
                    Result = (T)result.Result,
                    StatusCode = StatusCode.Ok
                };
            }
            else
            {
                return new ResponceModelStatusCode<T>()
                {
                    Result = null,
                    StatusCode = StatusCode.Error
                };
            }
        }

        public ResponceModelStatusCode<T> ResponceFailed<T>(HttpResponseMessage response) where T : class
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return new ResponceModelStatusCode<T>()
                {
                    Result = null,
                    StatusCode = StatusCode.NoAuthorize
                };
            }
            else
            {
                return new ResponceModelStatusCode<T>()
                {
                    Result = null,
                    StatusCode = StatusCode.Error
                };
            }
        }
    }
}