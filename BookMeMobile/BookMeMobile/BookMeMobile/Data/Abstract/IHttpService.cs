using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.OperationResults;

namespace BookMeMobile.Data.Abstract
{
    public interface IHttpService
    {
        Task<BaseOperationResult<T>> Get<T>(string route);

        Task<BaseOperationResult> Post<TContent>(string route, TContent content);

        Task<BaseOperationResult> Delete(string route);

        Task<BaseOperationResult> Put<TContent>(string route, TContent content);
    }
}