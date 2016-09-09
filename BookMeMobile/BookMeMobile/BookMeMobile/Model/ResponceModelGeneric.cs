using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMeMobile.Model
{
    public class ResponseModel<T> : ResponseModel
    {
        public T Result { get; set; }
    }
}