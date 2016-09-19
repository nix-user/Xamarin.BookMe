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
    public class HttpService
    {
        private readonly HttpClient httpClient = new HttpClient();

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

        private bool DidInteralServerErrorHappen(HttpResponseMessage message)
        {
            return message.StatusCode >= HttpStatusCode.InternalServerError;
        }

        private async Task<OperationResult> CreateOperationResultFromResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return new OperationResult() { Status = StatusCode.Ok };
            }

            return new OperationResult() { Status = StatusCode.Error };
        }

        private async Task<OperationResult<T>> CreateOperationResultFromResponse<T>(HttpResponseMessage response)
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

            if (this.DidInteralServerErrorHappen(response))
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