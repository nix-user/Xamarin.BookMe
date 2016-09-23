using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.BL.Abstract;
using BookMeMobile.Data;
using BookMeMobile.Data.Abstract;
using BookMeMobile.Enums;
using BookMeMobile.Interface;
using BookMeMobile.Model.Login;
using Xamarin.Forms;

namespace BookMeMobile.BL.Concrete
{
    /// <summary>
    /// This class performs an authentication logic
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IDependencyService dependencyService;
        private readonly IHttpHandler httpHandler;

        public AuthService(IDependencyService dependencyService, IHttpHandler httpHandler)
        {
            this.dependencyService = dependencyService;
            this.httpHandler = httpHandler;
        }

        /// <summary>
        /// Authenticate method
        /// </summary>
        /// <param name="loginModel">Model with user credentials</param>
        /// <returns>Status code of operation</returns>
        public async Task<StatusCode> AuthAsync(LoginModel loginModel)
        {
            try
            {
                var response = await this.SendAuthRequest(loginModel);

                if (response == null)
                {
                    return StatusCode.Error;
                }

                if (response.IsSuccessStatusCode)
                {
                    await this.SaveToken(response);
                    return StatusCode.Ok;
                }

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    return StatusCode.NoAuthorize;
                }

                return StatusCode.Error;
            }
            catch (WebException)
            {
                return StatusCode.ConnectionProblem;
            }
        }

        private async Task SaveToken(HttpResponseMessage response)
        {
            var contentResponce = await response.Content.ReadAsStringAsync();
            var token = this.ParseResponse(contentResponce);
            await this.dependencyService.Get<IFileWorker>().SaveTextAsync(token);
        }

        private async Task<HttpResponseMessage> SendAuthRequest(LoginModel user)
        {
            var requestСontent = this.GetLineRequest(user.Login, user.Password);
            var content = new StringContent(requestСontent, Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await this.httpHandler.PostAsync(RestURl.GetToken, content);
            return response;
        }

        private string GetLineRequest(string login, string password)
        {
            return $"grant_type=password&username={login}&password={password}";
        }

        private string ParseResponse(string contentResponse)
        {
            var getMassive = contentResponse.Split('"');
            return getMassive[3];
        }
    }
}