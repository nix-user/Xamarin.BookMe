using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Data;
using BookMeMobile.Entity;
using Newtonsoft.Json;

namespace BookMeMobile.BL
{
    public class BookingRepository
    {
        private string restUri;
        private HttpClient client;

        public BookingRepository()
        {
            restUri = new RestURl().BookURl;
            this.client = new HttpClient();
        }

        public async Task<IEnumerable<Booking>> GetAll()
        {
            var uri = new Uri(string.Format(this.restUri, string.Empty));
            var response = await this.client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Booking>>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task<Booking> GetBook(int id)
        {
            var uri = new Uri(string.Format(this.restUri, id));
            var response = await this.client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Booking>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> RemoveBook(int id)
        {
            var uri = new Uri(string.Format(this.restUri, id));
            var response = await this.client.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> AddBooking(Booking book)
        {
            var uri = new Uri(string.Format(this.restUri, book.Id));
            var json = JsonConvert.SerializeObject(book);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await this.client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}