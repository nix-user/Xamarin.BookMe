using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMeMobile.Data
{
    public class BaseRepository
    {
        protected readonly HttpService HttpService = new HttpService();
    }
}