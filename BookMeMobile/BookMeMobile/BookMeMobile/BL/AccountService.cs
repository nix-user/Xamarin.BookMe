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
        public async Task<TokenStatusCode> GetTocken(User user)
        {
            try
            {
                AccountController account = new AccountController();
                var response = await account.GetTockenKey(user);
                if (response.IsOperationSuccessful)
                {
                    return new TokenStatusCode()
                    {
                        StatusCode = StatusCode.Ok,
                        Token = response.Result
                    };
                }
                else
                {
                    return new TokenStatusCode()
                    {
                        StatusCode = StatusCode.Error,
                        Token = null
                    };
                }
            }
            catch (WebException e)
            {
                return new TokenStatusCode()
                {
                    StatusCode = StatusCode.NoInternet,
                    Token = null
                };
            }
        }
    }
}