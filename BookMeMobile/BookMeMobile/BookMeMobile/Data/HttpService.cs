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
using BookMeMobile.OperationResults;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BookMeMobile.Data
{
    public class HttpService
    {
        private readonly HttpClient httpClient = new HttpClient();

        public HttpService()
        {
            string token = DependencyService.Get<IFileWork>().LoadTextAsync().Result;
            if (token != null)
            {
                this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
        }

        public async Task<OperationResult<T>> Get<T>(string root)
        {
            var uri = new Uri(root);
            try
            {
                var response = await this.httpClient.GetAsync(uri);
                return await this.CreateOperationResultFromResponse<T>(response);
            }
            catch (Exception)
            {
                return new OperationResult<T>()
                {
                    Status = StatusCode.NoInternet
                };
            }
        }

        public async Task<OperationResult> Post<TContent>(string root, TContent content)
        {
            string jsonFormat = "application/json";

            var uri = new Uri(root);
            var json = JsonConvert.SerializeObject(content);
            var jsonContent = new StringContent(json, Encoding.UTF8, jsonFormat);
            try
            {
                var response = await this.httpClient.PostAsync(uri, jsonContent);
                return await this.CreateOperationResultFromResponse(response);
            }
            catch (Exception)
            {
                return new OperationResult()
                {
                    Status = StatusCode.NoInternet
                };
            }
        }

        public async Task<OperationResult> Delete(string root)
        {
            try
            {
                var uri = new Uri(root);
                var response = await this.httpClient.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return new OperationResult() { Status = StatusCode.Ok };
                }

                return new OperationResult() { Status = StatusCode.Error };
            }
            catch (Exception)
            {
                return new OperationResult() { Status = StatusCode.NoInternet };
            }
        }

        private async Task<OperationResult> CreateOperationResultFromResponse(HttpResponseMessage response)
        {
            var contentResponse = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<ResponseModel>(contentResponse);
            return new OperationResult
            {
                Status = responseModel.IsOperationSuccessful ? StatusCode.Ok : StatusCode.Error
            };
        }

        private async Task<OperationResult<T>> CreateOperationResultFromResponse<T>(HttpResponseMessage response)
        {
            var operationResult = await this.CreateOperationResultFromResponse(response);
            if (operationResult.Status == StatusCode.Ok)
            {
                var contentResponse = await response.Content.ReadAsStringAsync();
                return new OperationResult<T>() { Status = StatusCode.Ok, Result = JsonConvert.DeserializeObject<ResponseModel<T>>(contentResponse).Result };
            }

            return new OperationResult<T>() { Status = operationResult.Status };
        }
    }
}