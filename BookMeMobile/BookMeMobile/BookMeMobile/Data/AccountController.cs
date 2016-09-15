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
        private HttpClient client;

        public AccountController()
        {
            this.client = new HttpClient();
        }

        public async Task<ResponseModel<string>> GetTockenKey(User user)
        {
            try
            {
                var uri = new Uri(RestURl.GetTocken);
                var contentRequeest = this.GetLineRequest(user.Login, user.Password);
                var content = new StringContent(contentRequeest, Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = await this.client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var contentResponce = await response.Content.ReadAsStringAsync();
                    var roomResult = JsonConvert.DeserializeObject<string>(contentResponce);
                    await DependencyService.Get<IFileWork>().SaveTextAsync(roomResult);
                    return new ResponseModel<string>()
                    {
                        Result = roomResult,
                        IsOperationSuccessful = true
                    };
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                    }
                    return new ResponseModel<string>()
                    {
                        Result = null,
                        IsOperationSuccessful = false
                    };
                }
            }
            catch (Exception e)
            {
                var ec = e;
                return null;
            }
        }

        private string GetLineRequest(string login, string password)
        {
            return string.Format("grant_type=password&username={0}&password={1}", login, password);
        }
    }
}