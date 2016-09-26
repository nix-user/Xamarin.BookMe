using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMeMobile.Infrastructure.Concrete
{
    internal class NavigationServiceException : Exception
    {
        public NavigationServiceException(string message) : base(message)
        {
        }
    }
}