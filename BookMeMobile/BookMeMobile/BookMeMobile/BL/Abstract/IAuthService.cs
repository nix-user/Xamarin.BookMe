using System.Threading.Tasks;
using BookMeMobile.Enums;
using BookMeMobile.Model.Login;

namespace BookMeMobile.BL.Abstract
{
    public interface IAuthService
    {
        Task<StatusCode> AuthAsync(LoginModel user);
    }
}