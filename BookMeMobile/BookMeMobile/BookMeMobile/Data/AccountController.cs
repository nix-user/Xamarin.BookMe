using System;
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
    public class AccountController
    {
        private HttpService client;

        public AccountController()
        {
            this.client = new HttpService();
        }

        public async Task<StatusCode> GetTokenKey(User user)
        {
            var contentRequeest = this.GetLineRequest(user.Login, user.Password);
            var contentType = "application/x-www-form-urlencoded";
            var result = await this.client.Post<string, string>(RestURl.GetToken, contentRequeest, contentType);
            if (result.Status == StatusCode.Ok)
            {
                var token = this.ParseResponse(result.Result);
                await DependencyService.Get<IFileWork>().SaveTextAsync(token);
            }

            return result.Status;
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