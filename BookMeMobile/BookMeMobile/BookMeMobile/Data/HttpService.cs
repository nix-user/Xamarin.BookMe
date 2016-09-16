using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;
using Newtonsoft.Json;

namespace BookMeMobile.Data
{
    internal class HttpService
    {
        private HttpClient httpClient = new HttpClient();

        public async Task<OperationResult<T>> Get<T>(string root)
        {
            var uri = new Uri(string.Format(root, string.Empty));
            try
            {
                var response = await this.httpClient.GetAsync(uri);
                return await this.CreateOperationResultFromResult<T>(response);
            }
            catch (WebException)
            {
                return new OperationResult<T>()
                {
                    Status = StatusCode.NoInternet
                };
            }
        }

        public async Task<OperationResult<TResult>> Post<TContent, TResult>(string root, TContent content) //todo: add code to account for unsuccessful operation.result
        {
            string jsonFormat = "application/json";

            var uri = new Uri(root);
            var json = JsonConvert.SerializeObject(content);
            var jsonContent = new StringContent(json, Encoding.UTF8, jsonFormat);
            try
            {
                var response = await this.httpClient.PostAsync(uri, jsonContent);
                return await this.CreateOperationResultFromResult<TResult>(response);
            }
            catch (WebException)
            {
                return new OperationResult<TResult>()
                {
                    Status = StatusCode.NoInternet
                };
            }
        }

        private async Task<OperationResult<T>> CreateOperationResultFromResult<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var contentResponse = await response.Content.ReadAsStringAsync();
                var operationResult = new OperationResult<T>();
                if (response.IsSuccessStatusCode)
                {
                    operationResult.Status = StatusCode.Ok;
                    operationResult.Result =
                        JsonConvert.DeserializeObject<ResponseModel<T>>(contentResponse).Result;
                }
                else
                {
                    operationResult.Status = StatusCode.Error;
                }

                return operationResult;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return new OperationResult<T>()
                {
                    Status = StatusCode.NoAuthorize
                };
            }
            else
            {
                return new OperationResult<T>()
                {
                    Status = StatusCode.Error
                };
            }

            return null;
        }
    }
}