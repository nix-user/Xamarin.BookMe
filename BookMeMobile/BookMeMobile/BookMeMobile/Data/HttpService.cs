using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Enums;
using BookMeMobile.Interface;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;
using BookMeMobile.Resources;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BookMeMobile.Data
{
    public class HttpService
    {
        private readonly HttpClient httpClient = new HttpClient();

        public HttpService()
        {
            string token = DependencyService.Get<IFileWorker>().LoadTextAsync(FileResources.FileName).Result;
            if (token != null)
            {
                this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
        }

        public async Task<BaseOperationResult<T>> Get<T>(string root)
        {
            var uri = new Uri(root);
            try
            {
                var response = await this.httpClient.GetAsync(uri);
                return await this.CreateOperationResultFromResponse<T>(response);
            }
            catch (Exception)
            {
                return new BaseOperationResult<T>()
                {
                    Status = StatusCode.ConnectionProblem
                };
            }
        }

        public async Task<BaseOperationResult> Post<TContent>(string root, TContent content)
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
                return new BaseOperationResult()
                {
                    Status = StatusCode.ConnectionProblem
                };
            }
        }

        public async Task<BaseOperationResult> Delete(string root)
        {
            try
            {
                var uri = new Uri(root);
                var response = await this.httpClient.DeleteAsync(uri);
                return await this.CreateOperationResultFromResponse(response);
            }
            catch (Exception)
            {
                return new BaseOperationResult()
                {
                    Status = StatusCode.ConnectionProblem
                };
            }
        }

        private async Task<BaseOperationResult> CreateOperationResultFromResponse(HttpResponseMessage response)
        {
            var contentResponse = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<BaseResponseModel>(contentResponse);
            return new BaseOperationResult
            {
                Status = responseModel.IsOperationSuccessful ? StatusCode.Ok : StatusCode.Error
            };
        }

        private async Task<BaseOperationResult<T>> CreateOperationResultFromResponse<T>(HttpResponseMessage response)
        {
            var operationResult = await this.CreateOperationResultFromResponse(response);
            if (operationResult.Status == StatusCode.Ok)
            {
                var contentResponse = await response.Content.ReadAsStringAsync();
                return new BaseOperationResult<T>() { Status = StatusCode.Ok, Result = JsonConvert.DeserializeObject<ResponseModel<T>>(contentResponse).Result };
            }

            return new BaseOperationResult<T>() { Status = operationResult.Status };
        }
    }
}