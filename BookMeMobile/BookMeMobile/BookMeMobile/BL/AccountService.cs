using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Data;
using BookMeMobile.Entity;
using BookMeMobile.Enums;
using BookMeMobile.Model;
using BookMeMobile.Model.Login;

namespace BookMeMobile.BL
{
    public class AccountService
    {
        public async Task<StatusCode> GetToken(LoginModel user)
        {
            AccountController account = new AccountController();
            return await account.GetTokenKey(user);
        }
    }
}