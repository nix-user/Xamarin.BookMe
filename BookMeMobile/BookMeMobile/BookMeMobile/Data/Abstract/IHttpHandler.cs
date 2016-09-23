using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BookMeMobile.Data.Abstract
{
    public interface IHttpHandler
    {
        HttpHeaders RequestHeaders { get; }

        Task<HttpResponseMessage> GetAsync(string url);

        Task<HttpResponseMessage> PostAsync(string url, HttpContent content);

        Task<HttpResponseMessage> DeleteAsync(string url);
    }
}