using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.BL.Abstract;
using BookMeMobile.Enums;
using BookMeMobile.Model.Login;

namespace BookMeMobile.BL.Concrete.Fake
{
    public class FakeAuthService : IAuthService
    {
        public Task<StatusCode> AuthAsync(LoginModel loginModel)
        {
            return Task.FromResult(StatusCode.Ok);
        }
    }
}