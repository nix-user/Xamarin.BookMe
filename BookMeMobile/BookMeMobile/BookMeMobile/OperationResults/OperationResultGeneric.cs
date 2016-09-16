using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMeMobile.OperationResults
{
    public class OperationResult<T> : OperationResult
    {
        public T Result { get; set; }
    }
}