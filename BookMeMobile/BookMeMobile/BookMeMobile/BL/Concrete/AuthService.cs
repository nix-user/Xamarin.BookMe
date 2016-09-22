﻿using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.BL.Abstract;
using BookMeMobile.Data;
using BookMeMobile.Enums;
using BookMeMobile.Interface;
using BookMeMobile.Model.Login;
using Xamarin.Forms;

namespace BookMeMobile.BL.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient client;

        public AuthService()
        {
            this.client = new HttpClient();
        }

        public async Task<StatusCode> AuthAsync(LoginModel user)
        {
            try
            {
                var response = await this.SendAuthRequest(user);
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
            await DependencyService.Get<IFileWorker>().SaveTextAsync(token);
        }

        private async Task<HttpResponseMessage> SendAuthRequest(LoginModel user)
        {
            var uri = new Uri(RestURl.GetToken);
            var requestСontent = this.GetLineRequest(user.Login, user.Password);
            var content = new StringContent(requestСontent, Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await this.client.PostAsync(uri, content);
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