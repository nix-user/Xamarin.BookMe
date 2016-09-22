using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;
using BookMeMobile.Enums;

namespace BookMeMobile.BL.Abstract
{
    public interface IAccountService
    {
        Task<StatusCode> GetToken(User user);
    }
}