using System.Threading.Tasks;
using BookMeMobile.BL.Abstract;
using BookMeMobile.Data;
using BookMeMobile.Entity;
using BookMeMobile.Enums;

namespace BookMeMobile.BL.Concrete
{
    public class AccountService : IAccountService
    {
        public async Task<StatusCode> GetToken(User user)
        {
            AccountController account = new AccountController();
            return await account.GetTokenKey(user);
        }
    }
}