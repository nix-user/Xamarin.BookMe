using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;
using BookMeMobile.Model;
using Newtonsoft.Json;

namespace BookMeMobile.Data
{
    public class ReservationRepository
    {
        private string restUri;
        private HttpClient client;

        public ReservationRepository()
        {
            this.restUri = RestURl.BookURl;
            this.client = new HttpClient();
            this.client.Timeout = new TimeSpan(0, 0, 4);
        }

        public async Task<IEnumerable<ReservationModel>> GetAll()
        {
            var uri = new Uri(string.Format(this.restUri, string.Empty));
            var response = this.client.GetAsync(uri);
            if (response.Result.IsSuccessStatusCode)
            {
                var content = await response.Result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<ReservationModel>>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task<ReservationStatusModel> GetReservation(int id)
        {
            var uri = new Uri(string.Format(this.restUri, id));
            var response = this.client.GetAsync(uri);
            if (response.Result.IsSuccessStatusCode)
            {
                var content = await response.Result.Content.ReadAsStringAsync();
                return new ReservationStatusModel()
                {
                    Reservation = JsonConvert.DeserializeObject<ReservationModel>(content),
                    StatusCode = StatusCode.Ok
                };
            }
            else
            {
                return new ReservationStatusModel()
                {
                    Reservation = null,
                    StatusCode = StatusCode.Ok
                };
            }
        }

        public async Task<StatusCode> RemoveReservation(int id)
        {
            try
            {
                var uri = new Uri(string.Format(this.restUri, id));
                var response = await this.client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return StatusCode.Ok;
                }
                else
                {
                    return StatusCode.Error;
                }
            }
            catch (Exception)
            {
                return StatusCode.NoInternet;
            }
        }

        public async Task<StatusCode> AddReservation(int idRoom,ReservationModel reservation)
        {
            try
            {
                var uri = new Uri(string.Format(this.restUri, string.Empty));
                var json = JsonConvert.SerializeObject(reservation);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await this.client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    return StatusCode.Ok;
                }
                else
                {
                    return StatusCode.Error;
                }
            }
            catch (Exception)
            {
                return StatusCode.NoInternet;
            }
        }

        public async Task<ResponseModel<IEnumerable<ReservationModel>>> GetUserReservations(string login)
        {
            var uri = new Uri(string.Format(this.restUri, login));
            var response = await this.client.GetAsync(uri);
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ResponseModel<IEnumerable<ReservationModel>>>(content);
            }
            else
            {
                return new ResponseModel<IEnumerable<ReservationModel>>
                {
                    Result = null,
                    IsOperationSuccessful = false
                };
            }
        }
    }
}