using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Data.Abstract;

namespace BookMeMobile.Data.Concrete
{
    public class HttpClientHandler : IHttpHandler
    {
        private HttpClient httpClient = new HttpClient();

        public HttpRequestHeaders RequestHeaders
        {
            get { return this.httpClient.DefaultRequestHeaders; }
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await this.httpClient.GetAsync(url);
        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            return await this.httpClient.PostAsync(url, content);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            return await this.httpClient.DeleteAsync(url);
        }
    }
}