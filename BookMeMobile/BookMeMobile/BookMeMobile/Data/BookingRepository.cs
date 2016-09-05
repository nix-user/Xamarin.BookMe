using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;
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

        public async Task<ReservationModel> GetReservation(int id)
        {
            var uri = new Uri(string.Format(this.restUri, id));
            var response = this.client.GetAsync(uri);
            if (response.Result.IsSuccessStatusCode)
            {
                var content = await response.Result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ReservationModel>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> RemoveReservation(int id)
        {
            var uri = new Uri(string.Format(this.restUri, id));
            var response = await this.client.DeleteAsync(uri);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddReservation(ReservationModel reservation)
        {
            var uri = new Uri(string.Format(this.restUri, string.Empty));
            var json = JsonConvert.SerializeObject(reservation);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await this.client.PostAsync(uri, content);
            return response.IsSuccessStatusCode;
        }
    }
}