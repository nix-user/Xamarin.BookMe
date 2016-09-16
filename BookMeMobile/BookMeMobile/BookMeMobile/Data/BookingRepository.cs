using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;
using BookMeMobile.Interface;
using BookMeMobile.Model;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BookMeMobile.Data
{
    public class ReservationRepository
    {
        private HttpClient client;

        public ReservationRepository()
        {
            this.client = new HttpClient();
            string token = DependencyService.Get<IFileWork>().LoadTextAsync().Result;
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }

        public async Task<IEnumerable<ReservationModel>> GetAll()
        {
            var uri = new Uri(string.Format(RestURl.BookURl, string.Empty));
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
            var uri = new Uri(string.Format(RestURl.BookURl, id));
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
                var uri = new Uri(string.Format(RestURl.BookURl, id));
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

        public async Task<StatusCode> AddReservation(int idRoom, ReservationModel reservation)
        {
            try
            {
                var uri = new Uri(string.Format(RestURl.BookURl, string.Empty));
                var json = JsonConvert.SerializeObject(reservation);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await this.client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var contentResponce = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseModel>(contentResponce);
                    if (result.IsOperationSuccessful)
                    {
                        return StatusCode.Ok;
                    }
                    else
                    {
                        return StatusCode.Error;
                    }
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

        public async Task<ResponceModelStatusCode<IEnumerable<ReservationModel>>> GetUserReservations()
        {
            try
            {
                var uri = new Uri(string.Format(RestURl.GetUserReservation));
                var response = await this.client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return await this.ResponceSuccess<IEnumerable<ReservationModel>>(response);
                }
                else
                {
                    return this.ResponceFailed<IEnumerable<ReservationModel>>(response);
                }
            }
            catch (WebException)
            {
                return new ResponceModelStatusCode<IEnumerable<ReservationModel>>()
                {
                    Result = null,
                    StatusCode = StatusCode.NoInternet
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