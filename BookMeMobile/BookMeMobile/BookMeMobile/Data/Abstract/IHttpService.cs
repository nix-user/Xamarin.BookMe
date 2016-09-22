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
        Task<BaseOperationResult<T>> Get<T>(string root);

        Task<BaseOperationResult> Post<TContent>(string root, TContent content);

        Task<BaseOperationResult> Delete(string root);
    }
}