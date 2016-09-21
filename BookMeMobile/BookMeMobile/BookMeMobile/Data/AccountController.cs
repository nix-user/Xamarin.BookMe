using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;
using BookMeMobile.Enums;
using BookMeMobile.Interface;
using BookMeMobile.Model;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BookMeMobile.Data
{
    public class AccountController
    {
        private HttpClient client;

        public AccountController()
        {
            this.client = new HttpClient();
        }

        public async Task<StatusCode> GetTokenKey(User user)
        {
            try
            {
                var uri = new Uri(RestURl.GetToken);
                var contentRequeest = this.GetLineRequest(user.Login, user.Password);
                var content = new StringContent(contentRequeest, Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = await this.client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var contentResponce = await response.Content.ReadAsStringAsync();
                    var token = this.ParseResponse(contentResponce);
                    await DependencyService.Get<IFileWorker>().SaveTextAsync(token);
                    return StatusCode.Ok;
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        return StatusCode.NoAuthorize;
                    }
                    else
                    {
                        return StatusCode.Error;
                    }
                }
            }
            catch (WebException e)
            {
                return StatusCode.NoInternet;
            }
        }

        private string GetLineRequest(string login, string password)
        {
            return string.Format("grant_type=password&username={0}&password={1}", login, password);
        }

        private string ParseResponse(string contentResponse)
        {
            var getMassive = contentResponse.Split('"');
            return getMassive[3];
        }
    }
}