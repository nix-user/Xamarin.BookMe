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
    internal class HttpService
    {
        private HttpClient httpClient = new HttpClient();

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

        public async Task<OperationResult<TResult>> Post<TContent, TResult>(string root, TContent content, string jsonFormat = "application/json")
        {
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

        public async Task<OperationResult> Delete(string root, int id)
        {
            try
            {
                var uri = new Uri(string.Format(root, id));
                var response = await this.httpClient.DeleteAsync(uri);
                OperationResult result = new OperationResult();
                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    ResponseModel responseModel = JsonConvert.DeserializeObject<ResponseModel>(contentResponse);
                    if (responseModel.IsOperationSuccessful)
                    {
                        result.Status = StatusCode.Ok;
                    }
                    else
                    {
                        result.Status = StatusCode.Error;
                    }
                }
                else
                {
                    result.Status = StatusCode.Error;
                }

                return result;
            }
            catch (WebException)
            {
                return new OperationResult()
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
                ResponseModel<T> responseModel = JsonConvert.DeserializeObject<ResponseModel<T>>(contentResponse);
                if (responseModel.IsOperationSuccessful)
                {
                    operationResult.Status = StatusCode.Ok;
                    operationResult.Result = responseModel.Result;
                }
                else
                {
                    operationResult.Status = StatusCode.Error;
                }

                return operationResult;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.BadRequest)
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
        }
    }
}