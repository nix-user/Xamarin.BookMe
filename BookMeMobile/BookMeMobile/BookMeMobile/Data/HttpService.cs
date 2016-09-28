using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.BL.Abstract;
using BookMeMobile.Data.Abstract;
using BookMeMobile.Enums;
using BookMeMobile.Interface;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;
using BookMeMobile.Resources;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BookMeMobile.Data
{
    public class HttpService : IHttpService
    {
        private const string AuthorizationHeaderName = "Authorization";

        private readonly IDependencyService dependencyService;
        private readonly IHttpHandler httpHandler;

        public HttpService(IDependencyService dependencyService, IHttpHandler httpHandler)
        {
            this.dependencyService = dependencyService;
            this.httpHandler = httpHandler;
            string token = Task.Run(async () => await this.dependencyService.Get<IFileWorker>().LoadTextAsync(FileResources.FileName)).Result;
            if (token != null)
            {
                this.httpHandler.RequestHeaders.Add(AuthorizationHeaderName, new AuthenticationHeaderValue("bearer", token).ToString());
            }
        }

        public async Task<BaseOperationResult<T>> Get<T>(string route)
        {
            try
            {
                var response = await this.httpHandler.GetAsync(route);
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

        public async Task<BaseOperationResult> Post<TContent>(string route, TContent content)
        {
            string jsonFormat = "application/json";

            var json = JsonConvert.SerializeObject(content);
            var jsonContent = new StringContent(json, Encoding.UTF8, jsonFormat);
            try
            {
                var response = await this.httpHandler.PostAsync(route, jsonContent);
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

        public async Task<BaseOperationResult> Delete(string route)
        {
            try
            {
                var uri = new Uri(route);
                var response = await this.httpHandler.DeleteAsync(route);
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