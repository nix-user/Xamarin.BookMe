using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Data;
using BookMeMobile.Entity;
using BookMeMobile.Model;

namespace BookMeMobile.BL
{
    public class AccountService
    {
        public async Task<StatusCode> GetTocken(User user)
        {
            AccountController account = new AccountController();
            return await account.GetTockenKey(user);
        }
    }
}